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

        public static float[,] GrayScale(this Color[,] imageBitmap)
        {
            var arr = new float[imageBitmap.GetLength(0), imageBitmap.GetLength(1)];
            for (var x = 0; x < imageBitmap.GetLength(0); x++)
            {
                for (var y = 0; y < imageBitmap.GetLength(1); y++)
                {
                    arr[x, y] = imageBitmap[x, y].R * 0.299f + imageBitmap[x, y].G * 0.587f + imageBitmap[x, y].B * 0.114f;
                }
            }
            return arr;
        }

        public static float[] ToLine(this float[,] img)
        {
            var z = 0;
            var arr = new float[img.GetLength(0)*img.GetLength(1)];
            for (var i = 0; i < 10; i++)
                for (var j = 0; j < 10; j++)
                {
                    arr[z] = img[i, j];
                    z++;
                }
            return arr;
        }

        public static Bitmap ToBitmap(this float[,] img)
        {
            var imageBitmap = new Bitmap(img.GetLength(0), img.GetLength(1));
            for (var x = 0; x < imageBitmap.Width; x++)
            {
                for (var y = 0; y < imageBitmap.Height; y++)
                {
                    imageBitmap.SetPixel(x, y, Color.FromArgb((byte)img[x, y], (byte)img[x, y], (byte)img[x, y]));
                }
            }
            return imageBitmap;
        }

        public static float[,] Chunked(this float[,] img, Int32 m = 2)
        {
            var arr = (float[,])img.Clone();
            for (var i = 0; i < arr.GetLength(0) - m; i += m)
                for (var j = 0; j < arr.GetLength(1) - m; j += m)
                {
                    var color = 0f;
                    for (var ik = 0; ik < m; ik++)
                        for (var jk = 0; jk < m; jk++)
                            color += arr[i + ik, j + jk];
                    color /= (m * m);
                    for (var ik = 0; ik < m; ik++)
                        for (var jk = 0; jk < m; jk++)
                            arr[i + ik, j + jk] = (byte)color;
                }
            return arr;
        }

        public static float[,] Threshold(this float[,] img, Int32 p = 15)
        {
            var arr = (float[,])img.Clone();
            for (var i = 0; i < arr.GetLength(0); i++)
                for (var j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = (arr[i, j] > p ? 255f : arr[i, j] / 2f);
                }
            return arr;
        }


        private static Boolean IsValidPoint(Int32 i, Int32 j, float[,] map)
        {
            return ((i >= 0) && (i < map.GetLength(0)) &&
                    (j >= 0) && (j < map.GetLength(1)) &&
                    map[i, j] > 128f);
        }

        public static Int32 GetMaxIslandSize(this float[,] map)
        {
            var mapCopy = (float[,])map.Clone();
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
