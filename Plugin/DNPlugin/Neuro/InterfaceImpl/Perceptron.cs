using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using ImgComparer.Neuro.Domain;
using ImgComparer.Neuro.Interface;

namespace ImgComparer.Neuro.InterfaceImpl
{

    /// <summary>
    /// Однослойный n-нейронный перцептрон
    /// </summary>
    [Serializable]
    public class Perceptron : IPerceptron
    {
        private List<Dictionary<String, INeuron>> _neurons; // слои нейронов
        private Int32 _neuronCount;
        private Int32 _m;
        private IEnumerable<String> _classes;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="classes"> число нейронов</param>
        /// <param name="l"></param>
        public Perceptron(IEnumerable<String> classes, Int32 x, Int32 y, Int32 l = 4)
        {
            Classes = classes;
            NeuronCount = classes.Count();
            M = x * y;
            X = x;
            Y = y;
            Neurons = new List<Dictionary<String, INeuron>>();
            for (var i = 0; i < l; i++)
            {
                var dict = classes.ToDictionary<string, string, INeuron>(t => t, t => new Neuron(_m));
                Neurons.Add(dict);
            }
        }

        public Perceptron(String file)
        {
            var formatter = new BinaryFormatter();
            SaveInfo saveInfo;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                saveInfo = (SaveInfo)formatter.Deserialize(fs);
            }
            Neurons = saveInfo.Neurons;
            if (Neurons == null)
                throw new NullReferenceException(nameof(Neurons));
            Classes = Neurons.FirstOrDefault()?.Keys.ToArray() ?? new string[0];
            NeuronCount = Neurons.FirstOrDefault()?.Keys.Count ?? 0;
            M = Neurons.FirstOrDefault()?.Values.FirstOrDefault()?.Size ?? 0;
            X = saveInfo.X;
            Y = saveInfo.Y;
        }

        public void Save(String path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, new SaveInfo
                {
                    Neurons = Neurons,
                    X = X,
                    Y = Y
                });
            }
        }

        /// <summary>
        /// Распознавание образа
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <returns> выходной образ</returns>
        public Dictionary<String, float>[] Recognize(IList<float[]> x)
        {
            var yList = new Dictionary<String, float>[Neurons.Count];
            for (var i = 0; i < Neurons.Count; i++)
            {
                yList[i] = Classes.ToDictionary(@class => @class, @class => 0f);
            }
            Parallel.For(0, Neurons.Count, (i) =>
            {
                foreach (var neuron in Neurons[i])
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
            foreach (var t in Neurons)
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
                for (var i = 0; i < Neurons.Count; i++)
                    f &= VectorEqual(t[i], y[i]);

                // подстройка весов каждого нейрона
                Parallel.For(0, Neurons.Count, (layer) =>
                {
                    foreach (var classes in Neurons[layer].Keys)
                    {
                        var d = y[layer][classes] - t[layer][classes];
                        Neurons[layer][classes].ChangeWeights(v, d, x[layer]);
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
            return !a.Any(t => Math.Abs(t.Value - b[t.Key]) > 0.001);
        }


        /// <summary>
        /// Число слабосвязанных слоёв
        /// </summary>
        public Int32 LCount => Neurons.Count;

        /// <summary>
        ///  число нейронов
        /// </summary>
        public Int32 NeuronCount
        {
            get { return _neuronCount; }
            private set { _neuronCount = value; }
        }

        public IEnumerable<String> Classes
        {
            get { return _classes; }
            private set { _classes = value; }
        }

        private List<Dictionary<String, INeuron>> Neurons
        {
            get { return _neurons; }
            set { _neurons = value; }
        }

        public Int32 M
        {
            get { return _m; }
            private set { _m = value; }
        }

        public Int32 X { get; }
        public Int32 Y { get; }
    }
}
