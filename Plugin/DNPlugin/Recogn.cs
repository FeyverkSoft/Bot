using System;
using System.Drawing;
using System.IO;
using ImgComparer.Neuro.Interface;
using ImgComparer.Neuro.InterfaceImpl;

namespace ImgComparer
{
    internal sealed class Recogn : IRecogn
    {
        public Boolean ImgRecogn(Bitmap img, String semplePath)
        {
           if( !File.Exists(semplePath))
                throw new DirectoryNotFoundException($"{semplePath} not found");

            IPerceptron perceptron = new Perceptron(semplePath);
            perceptron.Recognize(imgPreProcess(img));
            return false;
        }

        private float[][] imgPreProcess(Bitmap img)
        {
            var result = new float[5][];

            return result;
        }
    }
}
