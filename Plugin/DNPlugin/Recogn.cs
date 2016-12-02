using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Documents;
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
            var lire = new Dictionary<String, Single>();
            foreach (var dictionary in result)
            {
                foreach (var f in dictionary.Keys)
                {
                    if (!lire.ContainsKey(f))
                        lire.Add(f, 0);
                    lire[f] += dictionary[f] / result.Length;
                }
            }
            return lire["pos"] > lire["neg"];
        }
    }
}
