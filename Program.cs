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
            var marcie1 = ImageSerializer.ReadImage(@"..\..\marcie1.png");
            var hexagon = ImageSerializer.ReadImage(@"..\..\hexagon.png");
            var marcie2 = Blur(marcie1, hexagon);
            ImageSerializer.SaveImage(marcie2, @"..\..\marcie2.png");
        }
    }
}
