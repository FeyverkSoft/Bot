using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfExecutor.Extensions.Localization
{
    public class XmlLocalizationExtension : MarkupExtension
    {
        /// <summary>
        /// Привязка локализованного значения в виде XML
        /// </summary>
        public Binding Xml { get; set; }

        public Binding LocalizedStrings { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Xml == null && LocalizedStrings == null)
                throw new ArgumentException($"Необходимо задать {nameof(Xml)} или {nameof(LocalizedStrings)}");

            if (Xml != null && LocalizedStrings != null)
                throw new ArgumentException($"Нельзя одновременно задать {nameof(Xml)} и {nameof(LocalizedStrings)}");

            // Если задан XML
            if (Xml != null)
            {
                var listener = new CultureChangedListener();

                // Создаем привязку для слушателя
                var listenerBinding = new Binding
                {
                    Source = listener,
                    Path = new PropertyPath(nameof(CultureChangedListener.CurrentCulture)),
                    Mode = BindingMode.OneWay
                };

                var multiBinding = new MultiBinding
                {
                    Converter = new XmlLocalizationConverter(),
                    Bindings = { listenerBinding, Xml },
                    Mode = BindingMode.OneWay
                };

                var value = multiBinding.ProvideValue(serviceProvider);
                return value;
            }

            // Если задан LocalizedStrings
            if (LocalizedStrings != null)
            {
                var listener = new CultureChangedListener();

                // Создаем привязку для слушателя
                var listenerBinding = new Binding
                {
                    Source = listener,
                    Path = new PropertyPath(nameof(CultureChangedListener.CurrentCulture)),
                    Mode = BindingMode.OneWay
                };

                var multiBinding = new MultiBinding
                {
                    Converter = new LocalizedStringsConverter(),
                    Bindings = { listenerBinding, LocalizedStrings },
                    Mode = BindingMode.OneWay
                };

                var value = multiBinding.ProvideValue(serviceProvider);
                return value;
            }

            return null;
        }
    }
}
