using System;
using System.Drawing;
using System.IO;
using ImgComparer.Helpers;
using ImgComparer.Neuro.Interface;
using ImgComparer.Neuro.InterfaceImpl;

namespace ImgComparer
{
    internal sealed class Recogn : IRecogn
    {
        public Boolean ImgRecogn(Bitmap img, String semplePath)
        {
            if (!File.Exists(semplePath))
                throw new DirectoryNotFoundException($"{semplePath} not found");

            IPerceptron perceptron = new Perceptron(semplePath);
            perceptron.Recognize(imgPreProcess(img, perceptron.X, perceptron.Y));
            return false;
        }

        private float[][] imgPreProcess(Bitmap img, Int32 x, Int32 y)
        {
            img = ImgHelpers.ResizeImg(img, x, y);
            var result = new float[5][];

            return result;
        }
    }
}
