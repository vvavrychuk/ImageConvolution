using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolution
{
    class Program
    {
        static int ModFloor(int a, int n)
        {
            return a - n * (int) Math.Floor((double)a / n);
        }

        static Matrix<double> ReadImageGrayScale(string path)
        {
            var bitmap = new Bitmap(path);
            var bitmap2 = Matrix<double>.Build.Dense(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap2[i, j] = (bitmap.GetPixel(i, j).R + bitmap.GetPixel(i, j).G + bitmap.GetPixel(i, j).B) / 255.0 / 3.0;
                }
            }
            return bitmap2;
        }

        static void SaveImage(Matrix<double> bitmap, string path)
        {
            using (var bitmap2 = new Bitmap(bitmap.RowCount, bitmap.ColumnCount))
            {
                for (int i = 0; i < bitmap2.Width; i++)
                {
                    for (int j = 0; j < bitmap2.Height; j++)
                    {
                        var color = (int)(bitmap[i, j] * 256);
                        bitmap2.SetPixel(i, j, Color.FromArgb(color, color, color));
                    }
                }
                bitmap2.Save(path, ImageFormat.Png);
            }
        }

        static Matrix<double> Circshift(Matrix<double> input, int i, int j)
        {
            var output = Matrix<double>.Build.Dense(input.RowCount, input.ColumnCount);
            for (int n = 0; n < input.RowCount; n++)
            {
                for (int k = 0; k < input.ColumnCount; k++)
                {
                    output[n, k] = input[ModFloor(n - i, input.RowCount), ModFloor(k - j, input.ColumnCount)];
                }
            }
            return output;
        }

        static Matrix<double> Blur(Matrix<double> input, Matrix<double> pattern)
        {
            var output = Matrix<double>.Build.Dense(input.RowCount, input.ColumnCount);
            double total = 0.0;
            for (var i = 0; i < pattern.RowCount; i++)
            {
                for (var j = 0; j < pattern.ColumnCount; j++)
                {
                    if (pattern[i, j] != 0)
                    {
                        output += pattern[i, j] * Circshift(input, i, j);
                        total += pattern[i, j];
                    }
                }
            }
            return output / total;
        }

        static void Main(string[] args)
        {
            var marcie1 = ReadImageGrayScale(@"..\..\marcie1.png");
            var hexagon = ReadImageGrayScale(@"..\..\hexagon.png");
            SaveImage(Blur(marcie1, hexagon), @"..\..\marcie2.png");
        }
    }
}
