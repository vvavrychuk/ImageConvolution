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
        static double Sum(Matrix<double> matrix)
        {
            double value = 0;
            for (var i = 0; i < matrix.RowCount; i++)
            {
                for (var j = 0; j < matrix.ColumnCount; j++)
                {
                    value += matrix[i, j];
                }
            }
            return value;
        }

        static Matrix<double> Convolution(Tuple<int, int> dimension, MatrixInfinite matrixInfinite, Matrix<double> matrixFinite)
        {
            var output = Matrix<double>.Build.Dense(dimension.Item1, dimension.Item2);
            for (var i1 = 0; i1 < matrixFinite.RowCount; i1++)
            {
                for (var j1 = 0; j1 < matrixFinite.ColumnCount; j1++)
                {
                    if (matrixFinite[i1, j1] != 0)
                    {
                        for (var i = 0; i < output.RowCount; i++)
                        {
                            for (var j = 0; j < output.ColumnCount; j++)
                            {
                                output[i, j] += matrixFinite[i1, j1] * matrixInfinite[i - i1, j - j1];
                            }
                        }
                    }
                }
            }
            return output;
        }

        static Matrix<double> Blur(Matrix<double> input, Matrix<double> pattern)
        {
            var dimension = new Tuple<int, int>(input.RowCount, input.ColumnCount);
            return Convolution(dimension, new MatrixInfinite(input), pattern) / Sum(pattern);
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
