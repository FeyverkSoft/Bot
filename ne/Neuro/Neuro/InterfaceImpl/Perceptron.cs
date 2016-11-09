using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neuro.Interface;

namespace Neuro.InterfaceImpl
{

    /// <summary>
    /// Однослойный n-нейронный перцептрон
    /// </summary>
    [Serializable]
    public class Perceptron : IPerceptron
    {
        readonly List<Dictionary<String, INeuron>> _neurons; // слои нейронов
        readonly Int32 _neuronCount;
        readonly Int32 _m;
        readonly String[] _classes;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="classes"> число нейронов</param>
        /// <param name="m">число входов каждого нейрона скрытого слоя</param>
        /// <param name="l"></param>
        public Perceptron(String[] classes, Int32 m, Int32 l = 4)
        {
            _classes = classes;
            _neuronCount = classes.Length;
            _m = m;
            _neurons = new List<Dictionary<String, INeuron>>();
            for (var i = 0; i < l; i++)
            {
                var dict = new Dictionary<String, INeuron>();
                foreach (var t in classes)
                {
                    dict.Add(t, new Neuron(m));
                }
                _neurons.Add(dict);
            }
        }

        /// <summary>
        /// Распознавание образа
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <returns> выходной образ</returns>
        public Dictionary<String, float>[] Recognize(float[][] x)
        {
            var yList = new Dictionary<String, float>[_neurons.Count];
            for (var i = 0; i < _neurons.Count; i++)
            {
                yList[i] = _classes.ToDictionary(@class => @class, @class => 0f);
            }
            Parallel.For(0, _neurons.Count, (i) =>
            {
                foreach (var neuron in _neurons[i])
                {
                    yList[i][neuron.Key] += neuron.Value.Transfer(x[i]);
                }
            });
            return yList;
        }

        /// <summary>
        /// Инициализация начальных весов 
        /// малым случайным значением
        /// </summary>
        /// <param name="max">Максимальное значение начальных весов</param>
        public void InitWeights(float max)
        {
            foreach (var t in _neurons)
                foreach (var neuron in t)
                {
                    neuron.Value.InitWeights(max);
                }
        }

        /// <summary>
        /// Обучение перцептрона
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <param name="y">правильный выходной вектор</param>
        public void Teach(float[][] x, Dictionary<String, float>[] y)
        {
            const float v = 0.5f; // скорость обучения
            Boolean f;
            do
            {
                var t = Recognize(x);
                f = true;
                for (var i = 0; i < _neurons.Count; i++)
                    f &= VectorEqual(t[i], y[i]);

                // подстройка весов каждого нейрона
                Parallel.For(0, _neurons.Count, (layer) =>
                {
                    foreach (var classes in _neurons[layer].Keys)
                    {
                        var d = y[layer][classes] - t[layer][classes];
                        _neurons[layer][classes].ChangeWeights(v, d, x[layer]);
                    }
                });

            } while (!f);
        }

        /// <summary>
        /// Сравнивание двух результатов
        /// </summary>
        /// <param name="a">первый вектор</param>
        /// <param name="b">второй вектор</param>
        /// <returns>boolean</returns>
        private Boolean VectorEqual(Dictionary<String, float> a, Dictionary<String, float> b)
        {
            if (a.Count != b.Count) return false;
            return !a.Where((t, i) => Math.Abs(t.Value - b[t.Key]) > 0.001).Any();
        }

        /// <summary>
        ///  число нейронов
        /// </summary>
        public Int32 GetNeuronCount => _neuronCount;

        /// <summary>
        /// число входов каждого нейрона скрытого слоя
        /// </summary>
        public Int32 GetM => _m;

        /// <summary>
        /// Число слабосвязанных слоёв
        /// </summary>
        public Int32 LCount => _neurons.Count;
    }
}
