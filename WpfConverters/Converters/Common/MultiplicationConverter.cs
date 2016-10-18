using System.Collections.Generic;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class MultiplicationConverter : ArithmeticConverter
    {
        protected override double? Calculate(List<double> numbers)
        {
            return numbers.Aggregate<double, double>(1, (current, num) => current * num);
        }
    }
}
