using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Выволняет операцию деления элемента списка друг на друга
    /// </summary>
    public class DivisionConverter : ArithmeticConverter
    {
        protected override Double? Calculate(List<Double> numbers)
        {
            var result = numbers[0];
            foreach (var number in numbers.Skip(1))
            {
                if (Math.Abs(number) < 0.0000000001d)
                    return null;
                result = result / number;
            }
            return result;
        }
    }
}
