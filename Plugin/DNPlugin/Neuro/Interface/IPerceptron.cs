using System;
using System.Collections.Generic;

namespace ImgComparer.Neuro.Interface
{
    public interface IPerceptron
    {
        /// <summary>
        /// Инициализация начальных весов 
        /// малым случайным значением
        /// </summary>
        /// <param name="max">Максимальное значение начальных весов</param>
        void InitWeights(float max);

        /// <summary>
        /// Обучение перцептрона
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <param name="y">правильный выходной вектор</param>
        void Teach(float[][] x, Dictionary<String, float>[] y);


        /// <summary>
        /// число входов каждого нейрона скрытого слоя
        /// </summary>
        Int32 M { get; }
        /// <summary>
        /// колличество классов
        /// </summary>
        IEnumerable<String> Classes { get; }
        Int32 NeuronCount { get; }

        /// <summary>
        /// Распознавание образа
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <returns> выходной образ</returns>
        Dictionary<String, float>[] Recognize(IList<float[]> x);

        /// <summary>
        /// Число слабосвязанных слоёв
        /// </summary>
        Int32 LCount { get; }

        void Save(String path);
        Int32 X { get; }
        Int32 Y { get; }
    }
}
