using System;
using System.Collections.Generic;
using System.Linq;
using Neuro.Domain;
using Neuro.Interface;

namespace Neuro.InterfaceImpl
{

    /// <summary>
    ///Учитель
    ///учит перцептрон распознаванию цифр
    /// </summary>
    public class Teacher : ITeacher
    {
        private readonly Dictionary<String, Dictionary<String, float>[]> _dic = new Dictionary<String, Dictionary<String, float>[]>();

        /// <summary>
        /// Персептрон
        /// </summary>
        private readonly IPerceptron _perceptron;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="perceptron">Персептрон</param>
        public Teacher(IPerceptron perceptron)
        {
            _perceptron = perceptron;
        }


        /// <summary>
        ///  Обучение перцептрона
        /// </summary>
        /// <param name="images">Образы для обучения</param>
        /// <param name="n">количество циклов обучения</param>
        public void Teach(ICollection<ImageData> images, Int32 n)
        {
            // инициализация начальных весов
            _perceptron.InitWeights(10);
            var classes = images.Select(x => x.Class).Distinct();
            // получение пиксельных массивов каждого изображения
            // и обучение n раз каждой выборке
            while (n-- > 0)
            {
                foreach (var item in images)
                {
                    var y = GetOutVector(item.Class, classes);
                    _perceptron.Teach(item.Data, y);
                }
            }
        }

        /// <summary>
        ///Генерация правильного выходного вектора
        /// </summary>
        /// <param name="n">цифра, в соответствии с которой 
        ///  нужно построить вектор, другими словами:
        ///  на каком месте должна быть 1, остальные 0</param>
        /// <returns>вектор для входа перцептрона</returns>
        private Dictionary<String, float>[] GetOutVector(String n, IEnumerable<String> classes)
        {
            if (_dic.ContainsKey(n))
                return _dic[n];
            var la = new Dictionary<String, float>[_perceptron.GetM];
            for (var i = 0; i < _perceptron.LCount; i++)
            {
                var dict = classes.ToDictionary<string, string, float>(@class => @class, @class => n == @class ? 1 : 0);
                la[i] = dict;
            }
            _dic.Add(n, la);
            return la;
        }
    }
}
