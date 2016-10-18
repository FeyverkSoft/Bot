using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Вызывает последовательно цепочку конвертеров для одного значения
    /// </summary>
    [ContentProperty(nameof(Collection))]
    public class ConverterChains : BaseValueConverterExtension, IAddChild
    {
        private Collection<object> _collection;

        public Collection<object> Collection
        {
            get { return _collection ?? (_collection = new Collection<object>()); }
            set { _collection = value; }
        }

        /// <summary>
        /// Вызывает последовательно цепочку конвертеров для одного значения
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Collection.Any())
            {
                Debug.WriteLine("Convert: ChainConverters: Empty Converters Collection");
                return value;
            }
            return Collection.OfType<ConverterChain>().Aggregate(value,
                (current, chain) => chain.Converter.Convert(current, chain.TargetType ?? targetType, chain.ConverterParameter, culture));
        }

        /// <summary>
        /// Вызывает последовательно в обратном направлении цепочку конвертеров для одного значения
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Collection.Any())
            {
                Debug.WriteLine("ConvertBack: ChainConverters: Empty Converters Collection");
                return value;
            }
            return Collection.OfType<ConverterChain>().Reverse()
                .Aggregate(value, (current, chain) => chain.Converter.ConvertBack(current, chain.TargetType ?? targetType, chain.ConverterParameter, culture));
        }


        public void AddChild(object value)
        {
            var chain = value as ConverterChain;
            if (chain != null)
                Collection.Add(chain);
            else
                throw new ArgumentNullException(nameof(value));
        }

        public void AddText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if (!string.IsNullOrWhiteSpace(text))
                throw new ArgumentException();
        }
    }

    public class ConverterChain
    {
        public IValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }

        public Type TargetType { get; set; }
    }
}
