using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImgComparer.Helpers
{
    public static class ImgHelpers
    {
       public static byte[,] Dev(byte[,] image1, byte[,] image2)
        {
            var img = new byte[image1.GetLength(0), image1.GetLength(1)];
            for (var i = 0; i < img.GetLength(0); i++)
                for (var j = 0; j < img.GetLength(1); j++)
                {
                    var m = (image1[i, j] - image2[i, j]);
                    img[i, j] = (byte)(m > 0 ? m : 0);
                }
            return img;
        }

        public static Bitmap ResizeImg(Bitmap src, Int32 width, Int32 height)
        {
            var newImage = new Bitmap(width, height);
            using (var gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(src, new Rectangle(0, 0, width, height));
            }
            return newImage;
        }

        public static byte[,] GrayScale(this Bitmap imageBitmap)
        {
            var arr = new byte[imageBitmap.Width, imageBitmap.Height];
            for (var x = 0; x < imageBitmap.Width; x++)
            {
                for (var y = 0; y < imageBitmap.Height; y++)
                {
                    var bitmapColor = imageBitmap.GetPixel(x, y);
                    var colorGray = (bitmapColor.R * 0.299 +bitmapColor.G * 0.587 + bitmapColor.B * 0.114);
                    arr[x, y] = (byte)colorGray;
                }
            }
            return arr;
        }

        public static Bitmap ToBitmap(this byte[,] img)
        {
            var imageBitmap = new Bitmap(img.GetLength(0), img.GetLength(1));
            for (var x = 0; x < imageBitmap.Width; x++)
            {
                for (var y = 0; y < imageBitmap.Height; y++)
                {
                    imageBitmap.SetPixel(x, y, Color.FromArgb(img[x, y], img[x, y], img[x, y]));
                }
            }
            return imageBitmap;
        }

        public static void Chunked(this byte[,] img, Int32 m = 2)
        {
            for (var i = 0; i < img.GetLength(0) - m; i += m)
                for (var j = 0; j < img.GetLength(1) - m; j += m)
                {
                    var color = 0;
                    for (var ik = 0; ik < m; ik++)
                        for (var jk = 0; jk < m; jk++)
                            color += img[i + ik, j + jk];
                    color /= (m * m);
                    for (var ik = 0; ik < m; ik++)
                        for (var jk = 0; jk < m; jk++)
                            img[i + ik, j + jk] = (byte)color;
                }
        }

        public static void Threshold(this byte[,] img, Int32 p = 15)
        {
            for (var i = 0; i < img.GetLength(0); i++)
                for (var j = 0; j < img.GetLength(1); j++)
                {
                    img[i, j] = (byte)(img[i, j] > p ? 255 : img[i, j] / 2);
                }
        }


        private static Boolean IsValidPoint(Int32 i, Int32 j, byte[,] map)
        {
            return ((i >= 0) && (i < map.GetLength(0)) &&
                    (j >= 0) && (j < map.GetLength(1)) &&
                    map[i, j] > 128);
        }

        public static Int32 GetMaxIslandSize(this byte[,] map)
        {
            var mapCopy = (byte[,])map.Clone();
            var maxSize = 0;

            var points = new Stack<Point>();

            for (var i = 0; i < mapCopy.GetLength(0); ++i)
                for (var j = 0; j < mapCopy.GetLength(1); ++j)
                {
                    var tmpSize = 0;
                    if (IsValidPoint(i, j, mapCopy))
                    {
                        points.Clear();
                        points.Push(new Point(i, j));
                        mapCopy[i, j] = 0;
                        ++tmpSize;

                        while (points.Count > 0)
                        {
                            var curPoint = points.Pop();
                            for (var eg = -1; eg < 2; eg++)
                                for (var ef = -1; ef < 2; ef++)
                                {
                                    var x = curPoint.X + eg;
                                    var y = curPoint.Y + ef;
                                    if (IsValidPoint(x, y, mapCopy))
                                    {
                                        ++tmpSize;
                                        mapCopy[x, y] = 0;
                                        points.Push(new Point(x, y));
                                    }
                                }
                        }
                    }
                    if (tmpSize > maxSize)
                        maxSize = tmpSize;
                }

            return maxSize;
        }

    }
}
