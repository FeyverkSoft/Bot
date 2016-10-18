using System.Collections.Generic;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class SubtractionConverter : ArithmeticConverter
    {
        protected override double? Calculate(List<double> numbers)
        {
            return numbers.Skip(1).Aggregate(numbers[0], (acc, next) => acc - next);
        }
    }
}
