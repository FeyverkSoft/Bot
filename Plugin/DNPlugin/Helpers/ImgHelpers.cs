using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImgComparer.Helpers
{
    public static class ImgHelpers
    {
        public static Byte[,] Dev(Byte[,] image1, Byte[,] image2)
        {
            var img = new Byte[image1.GetLength(0), image1.GetLength(1)];
            for (var i = 0; i < img.GetLength(0); i++)
                for (var j = 0; j < img.GetLength(1); j++)
                {
                    var m = (image1[i, j] - image2[i, j]);
                    img[i, j] = (Byte)(m > 0 ? m : 0);
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

        public static Single[,] GrayScale(this Color[,] imageBitmap)
        {
            var arr = new Single[imageBitmap.GetLength(0), imageBitmap.GetLength(1)];
            for (var x = 0; x < imageBitmap.GetLength(0); x++)
            {
                for (var y = 0; y < imageBitmap.GetLength(1); y++)
                {
                    arr[x, y] = imageBitmap[x, y].R * 0.299f + imageBitmap[x, y].G * 0.587f + imageBitmap[x, y].B * 0.114f;
                }
            }
            return arr;
        }

        public static Single[] ToLine(this Single[,] img)
        {
            var z = 0;
            var arr = new Single[img.GetLength(0)*img.GetLength(1)];
            for (var i = 0; i < 10; i++)
                for (var j = 0; j < 10; j++)
                {
                    arr[z] = img[i, j];
                    z++;
                }
            return arr;
        }

        public static Single[,] Thresholds(this Single[,] img, Int32 p = 15)
        {
            var arr = (Single[,])img.Clone();
            var th = 255;
            while (th > 0)
            {
                for (var i = 0; i < arr.GetLength(0) - 3; i++)
                    for (var j = 0; j < arr.GetLength(1) - 3; j++)
                    {
                        arr[i, j] = (Byte)(arr[i, j] < th &&
                                           arr[i, j] > th - 255 / p
                            ? th : arr[i, j]);
                    }
                th -= 255 / p;
            }
            return arr;
        }

        public static Bitmap ToBitmap(this Single[,] img)
        {
            var imageBitmap = new Bitmap(img.GetLength(0), img.GetLength(1));
            for (var x = 0; x < imageBitmap.Width; x++)
            {
                for (var y = 0; y < imageBitmap.Height; y++)
                {
                    imageBitmap.SetPixel(x, y, Color.FromArgb((Byte)img[x, y], (Byte)img[x, y], (Byte)img[x, y]));
                }
            }
            return imageBitmap;
        }

        public static Single[,] Chunked(this Single[,] img, Int32 m = 2)
        {
            var arr = (Single[,])img.Clone();
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
                            arr[i + ik, j + jk] = (Byte)color;
                }
            return arr;
        }

        public static Single[,] Threshold(this Single[,] img, Int32 p = 15)
        {
            var arr = (Single[,])img.Clone();
            for (var i = 0; i < arr.GetLength(0); i++)
                for (var j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = (arr[i, j] > p ? 255f : arr[i, j] / 2f);
                }
            return arr;
        }


        private static Boolean IsValidPoint(Int32 i, Int32 j, Single[,] map)
        {
            return ((i >= 0) && (i < map.GetLength(0)) &&
                    (j >= 0) && (j < map.GetLength(1)) &&
                    map[i, j] > 128f);
        }

        public static Int32 GetMaxIslandSize(this Single[,] map)
        {
            var mapCopy = (Single[,])map.Clone();
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

        public static List<Single[]> ImgPreProcess(Bitmap img, Int32 x, Int32 y)
        {
            img = ResizeImg(img, x, y);
            var imgArrray = new Color[x, y];
            var result = new List<Single[]>();
            Single[]
                r = new Single[x * y],
                g = new Single[x * y],
                b = new Single[x * y];
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
