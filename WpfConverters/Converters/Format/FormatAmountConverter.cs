using System;
using System.Collections.Generic;
using System.Globalization;

namespace WpfConverters.Converters.Format
{
    public class FormatAmountConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return null;

            if (!(values[0] is IConvertible))
                return null;

            var currencyId = (int?)(values[1] as long?) ?? values[1] as int?;

            decimal amount;
            try
            {
                amount = System.Convert.ToDecimal(values[0]);
            }
            catch
            {
                return null;
            }

            if (currencyId != null)
            {
                var onlyAmount = parameter as bool? ?? false;
                if (onlyAmount == false)
                {
                    if (CurrencyFormats.ContainsKey(currencyId.Value))
                    {
                        var format = CurrencyFormats[currencyId.Value];
                        return string.Format(format, amount);
                    }
                }
                else
                {
                    if (CurrencyPrecisions.ContainsKey(currencyId.Value))
                    {
                        var precision = CurrencyPrecisions[currencyId.Value];
                        return string.Format($@"{{0:#0.{new string('0', precision)}}}", amount);
                    }
                }
            }

            var amountString = FormatNumberConverter.Format(amount);
            return amountString;
        }

        private static readonly IDictionary<int, string> CurrencyFormats = new Dictionary<int, string>
        {
            {156, "{0:#0.00} ¥"}, //CNY
            {398, "{0:#0.00} ₸"}, //KZT
            {498, "{0:#0.00} L"}, //MDL
            {643, "{0:#0.00} ₽"}, //RUB
            {710, "{0:#0.00} R"}, //ZAR
            {840, "{0:#0.00} $"}, //USD
            {944, "{0:#0.00} ₼"}, //AZN
            {963, "{0:#0.00} XTS"}, //XTS
            {972, "{0:#0.00} ЅМ"}, //TJS
            {974, "{0:#0.00} Br"}, //BYR
            {978, "{0:#0.00} €"}, //EUR
            {980, "{0:#0.00} ₴"}, //UAH
            {981, "{0:#0.00} ლ"}, //GEL
            {985, "{0:#0.00} zł"} //PLN
        };

        private static readonly IDictionary<int, int> CurrencyPrecisions = new Dictionary<int, int>
        {
            {156, 2}, //CNY
            {398, 2}, //KZT
            {498, 2}, //MDL
            {643, 2}, //RUB
            {710, 2}, //ZAR
            {840, 2}, //USD
            {944, 2}, //AZN
            {963, 2}, //XTS
            {972, 2}, //TJS
            {974, 2}, //BYR
            {978, 2}, //EUR
            {980, 2}, //UAH
            {981, 2}, //GEL
            {985, 2} //PLN
        };
    }
}
