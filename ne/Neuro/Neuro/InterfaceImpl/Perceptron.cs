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
        readonly List<INeuron[]> _neurons; // слои нейронов
        readonly Int32 _neuronCount;
        readonly Int32 _m;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="neuronCount"> число нейронов</param>
        /// <param name="m">число входов каждого нейрона скрытого слоя</param>
        /// <param name="l"></param>
        public Perceptron(Int32 neuronCount, Int32 m, Int32 l = 4)
        {
            _neuronCount = neuronCount;
            _m = m;
            _neurons = new List<INeuron[]>();
            for (var i = 0; i < l; i++)
            {
                var nList = new INeuron[neuronCount];
                for (var j = 0; j < neuronCount; j++)
                {
                    nList[j] = new Neuron(m);
                }
                _neurons.Add(nList);
            }
        }

        /// <summary>
        /// Распознавание образа
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <returns> выходной образ</returns>
        public float[][] Recognize(float[][] x)
        {
            var y = new float[_neurons.Count][];
              Parallel.For(0, y.Length, (i) =>
              {
                  y[i] = new float[_neurons[i].Length];
                  for (var j = 0; j < y[i].Length; j++)
                  {
                      y[i][j] += _neurons[i][j].Transfer(x[i]);
                  }
              });
            return y;
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
                    neuron.InitWeights(max);
                }
        }

        /// <summary>
        /// Обучение перцептрона
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <param name="y">правильный выходной вектор</param>
        public void Teach(float[][] x, float[][] y)
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
                Parallel.For(0, _neurons.Count, (i) =>
                 {
                     for (var j = 0; j < _neurons[i].Length; j++)
                     {
                         var d = y[i][j] - t[i][j];
                         _neurons[i][j].ChangeWeights(v, d, x[i]);
                     }
                 });
            } while (!f);
        }

        /// <summary>
        /// Сравнивание двух векторов
        /// </summary>
        /// <param name="a">первый вектор</param>
        /// <param name="b">второй вектор</param>
        /// <returns>boolean</returns>
        private Boolean VectorEqual(float[] a, float[] b)
        {
            if (a.Length != b.Length) return false;
            return !a.Where((t, i) => Math.Abs(t - b[i]) > 0.001).Any();
        }

        /// <summary>
        ///  число нейронов
        /// </summary>
        public Int32 GetNeuronCount => _neuronCount;

        /// <summary>
        /// число входов каждого нейрона скрытого слоя
        /// </summary>
        public Int32 GetM => _m;
    }
}
