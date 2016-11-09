using System;

namespace Neuro.Domain
{
    /// <summary>
    /// Образ которым обучаем распознавать нейрон класс образа
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Образ представленный в виде целочисленного вектора
        /// </summary>
        public float[][] Data { get; set; }

        /// <summary>
        /// Класс к которому принадлежит образ
        /// </summary>
        public String Class { get; set; }
    }
}
