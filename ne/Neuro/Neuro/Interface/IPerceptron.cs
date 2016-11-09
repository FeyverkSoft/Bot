﻿using System;

namespace Neuro.Interface
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
        void Teach(float[][] x, float[][] y);

        /// <summary>
        ///  число нейронов
        /// </summary>
        Int32 GetNeuronCount { get; }

        /// <summary>
        /// число входов каждого нейрона скрытого слоя
        /// </summary>
        Int32 GetM { get; }

        /// <summary>
        /// Распознавание образа
        /// </summary>
        /// <param name="x">входной вектор</param>
        /// <returns> выходной образ</returns>
        float[][] Recognize(float[][] x);
    }
}
