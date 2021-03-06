﻿using MathNet.Numerics.LinearAlgebra;
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
        static double Sum(Matrix matrix)
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

        static Matrix Convolution(Tuple<int, int> dimension, MatrixInfinite matrixInfinite, Matrix matrixFinite)
        {
            var output = new Matrix(dimension.Item1, dimension.Item2);
            for (var i1 = matrixFinite.Ranges[0, 0]; i1 < matrixFinite.Ranges[0, 1]; i1++)
            {
                for (var j1 = matrixFinite.Ranges[1, 0]; j1 < matrixFinite.Ranges[1, 1]; j1++)
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

        static Matrix Blur(Matrix input, Matrix pattern)
        {
            var dimension = new Tuple<int, int>(input.RowCount, input.ColumnCount);
            return Convolution(dimension, new MatrixInfiniteZero(input), pattern.Center()) / Sum(pattern);
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
