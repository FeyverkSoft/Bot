using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class AdditionConverter : ArithmeticConverter
    {
        protected override Double? Calculate(List<Double> numbers)
        {
            return numbers.Sum();
        }
    }
}
