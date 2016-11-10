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

            var result = perceptron.Recognize(imgPreProcess(img, perceptron.X, perceptron.Y));

            return false;
        }

        private List<float[]> imgPreProcess(Bitmap img, Int32 x, Int32 y)
        {
            img = ImgHelpers.ResizeImg(img, x, y);
            var imgArrray = new Color[x, y];
            var result = new List<float[]>();
            float[]
                r = new float[x * y],
                g = new float[x * y],
                b = new float[x * y];
            var z = 0;
            for (var i = 0; i < img.Width; i++)
                for (var j = 0; j < img.Height; j++)
                {
                    var bitmapColor = img.GetPixel(i, j);
                    imgArrray[i, j] = bitmapColor;
                    r[z] = bitmapColor.R;
                    g[z] = bitmapColor.G;
                    b[z] = bitmapColor.B;
                    z++;
                }
            var gs = imgArrray.GrayScale();
            result.Add(r);
            result.Add(g);
            result.Add(b);
            result.Add(gs.ToLine());
            result.Add(gs.Chunked(6).ToLine());
            result.Add(gs.Thresholds(6).ToLine());
            return result;
        }
    }
}
