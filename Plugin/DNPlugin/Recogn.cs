using System;
using System.Collections.Generic;
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

            var result = perceptron.Recognize(ImgHelpers.ImgPreProcess(img, perceptron.X, perceptron.Y));

            return false;
        }
    }
}
