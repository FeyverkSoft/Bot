﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfExecutor.Extensions.Localization
{
    /// <summary>
    /// Расширение разметки, которое возвращает локализованную строку по ключу или привязке
    /// </summary>
    [ContentProperty(nameof(ArgumentBindings))]
    public class LocalizationExtension : MarkupExtension
    {
        private Collection<BindingBase> _arguments;

        public LocalizationExtension()
        {
        }

        public LocalizationExtension(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Ключ локализованной строки
        /// </summary>
        public String Key { get; set; }

        /// <summary>
        /// Привязка для ключа локализованной строки
        /// </summary>
        public Binding KeyBinding { get; set; }

        /// <summary>
        /// Игнорировать, и не пытаться переводить, а просто вызывать обновление поля
        /// </summary>
        public Boolean IgnoreLoc { get; set; }

        /// <summary>
        /// Форматирование ключа
        /// </summary>
        public String KeyFormat { get; set; }

        /// <summary>
        /// Аргументы форматируемой локализованный строки
        /// </summary>
        public IEnumerable<object> Arguments { get; set; }

        /// <summary>
        /// Привязки аргументов форматируемой локализованный строки
        /// </summary>
        public Collection<BindingBase> ArgumentBindings
        {
            get { return _arguments ?? (_arguments = new Collection<BindingBase>()); }
            set { _arguments = value; }
        }

        /// <summary>
        /// Форматирование полученной строки
        /// </summary>
        public string ValueFormat { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null && KeyBinding == null)
                throw new ArgumentException($"Необходимо задать {nameof(Key)} или {nameof(KeyBinding)}");

            if (Key != null && KeyBinding != null)
                throw new ArgumentException($"Нельзя одновременно задать {nameof(Key)} и {nameof(KeyBinding)}");

            if (Arguments != null && ArgumentBindings.Any())
                throw new ArgumentException($"Нельзя одновременно задать {nameof(Arguments)} и {nameof(ArgumentBindings)}");

            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target.TargetObject.GetType().FullName == "System.Windows.SharedDp")
                return this;

            if (!String.IsNullOrEmpty(Key) && !String.IsNullOrEmpty(KeyFormat))
                Key = String.Format(KeyFormat, Key);


            // Если заданы привязка ключа или список привязок аргументов,
            // то используем BindingLocalizationListener
            if (KeyBinding != null || ArgumentBindings.Any())
            {
                var listener = new CultureChangedListener();

                // Создаем привязку для слушателя
                var listenerBinding = new Binding
                {
                    Source = listener,
                    Path = new PropertyPath(nameof(CultureChangedListener.CurrentCulture)),
                    Mode = BindingMode.OneWay
                };

                var keyBinding = KeyBinding ?? new Binding { Source = Key };

                var multiBinding = new MultiBinding
                {
                    Converter = new BindingLocalizationConverter(KeyFormat, ValueFormat, IgnoreLoc),
                    ConverterParameter = Arguments,
                    Bindings = { listenerBinding, keyBinding }
                };

                // Добавляем все переданные привязки аргументов
                foreach (var binding in ArgumentBindings)
                    multiBinding.Bindings.Add(binding);

                var value = multiBinding.ProvideValue(serviceProvider);
                return value;
            }

            // Если задан ключ, то используем KeyLocalizationListener
            if (!String.IsNullOrEmpty(Key))
            {
                if (IgnoreLoc)
                    return String.Format(ValueFormat, Key);

                var listener = new KeyLocalizationListener(Key, Arguments?.ToArray(), ValueFormat);

                // Если локализация навешана на DependencyProperty объекта DependencyObject
                if ((target.TargetObject is DependencyObject && target.TargetProperty is DependencyProperty) ||
                    target.TargetObject is Setter)
                {
                    var binding = new Binding(nameof(KeyLocalizationListener.Value))
                    {
                        Source = listener,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    return binding.ProvideValue(serviceProvider);
                }

                // Если локализация навешана на Binding, то возвращаем слушателя
                var targetBinding = target.TargetObject as Binding;
                var prop = target.TargetProperty as PropertyInfo;
                if (targetBinding != null && target.TargetProperty != null && prop != null && prop.Name == "Source")
                {
                    targetBinding.Path = new PropertyPath(nameof(KeyLocalizationListener.Value));
                    return listener;
                }

                // Иначе возвращаем локализованную строку
                return listener.Value;
            }

            return null;
        }
    }
}
