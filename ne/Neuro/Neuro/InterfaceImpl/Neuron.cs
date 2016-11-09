using System;
using Neuro.Interface;

namespace Neuro.InterfaceImpl
{
    [Serializable]
    internal class Neuron : INeuron
    {
        private readonly float[] _w; // веса синапсов    
        //private const float S = 50; // порог
        readonly Random _rand = new Random();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="m">число входов</param>
        internal Neuron(Int32 m)
        {
            _w = new float[m];
        }

        /// <summary>
        /// Передаточная функция
        /// </summary>
        /// <param name="x"> входной вектор</param>
        /// <returns>выходное значение нейрона</returns>
        public float Transfer(float[] x)
        {
            return Activator(Adder(x));
        }

        /// <summary>      
        /// Инициализация начальных весов синапсов
        ///  небольшими случайными значениями от 0 до n
        /// </summary>
        /// <param name="n"> от 0 до n</param>
        public void InitWeights(float n)
        {
            for (var i = 0; i < _w.Length; i++)
            {
                _w[i] = _rand.Next((Int32)n);
            }
        }

        /// <summary> 
        /// Модификация весов синапсов для обучения
        /// </summary>
        /// <param name="v"> - скорость обучения</param>
        /// <param name="d"> - разница между выходом нейрона и нужным выходом</param>
        /// <param name="x"> - входной вектор</param>
        public void ChangeWeights(float v, float d, float[] x)
        {
            for (var i = 0; i < _w.Length; i++)
            {
                _w[i] += v * d * x[i];
            }
        }


        /// <summary> 
        /// Сумматор
        /// </summary> 
        /// <param name="x">входной вектор</param> 
        /// <returns> не взвешенная сумма nec (биас не используется)</returns>>
        private float Adder(float[] x)
        {
            float sum = 0;
            for (var i = 0; i < x.Length; i++)
                sum += x[i] * _w[i];
            return sum;
        }

        /// <summary>
        /// Нелинейный преобразователь или функция активации,
        /// в данном случае - жесткая пороговая функция,
        /// имеющая область значений {0;1}
        /// </summary>
        /// <param name="nec">выход сумматора</param>
        /// <returns></returns>

        private float Activator(float nec)
        {
            return (float)(1 / (1 - Math.Pow(Math.E, -nec)));
            //return nec >= S ? 1 : 0;
        }
    }
}