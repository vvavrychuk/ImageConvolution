using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolution
{
    class MatrixInfinite
    {
        Matrix<double> matrix;

        static int ModFloor(int a, int n)
        {
            return a - n * (int)Math.Floor((double)a / n);
        }

        public MatrixInfinite(Matrix<double> matrix)
        {
            this.matrix = matrix;
        }

        public double this[int i, int j]
        {
            get
            {
                return matrix[ModFloor(i, matrix.RowCount), ModFloor(j, matrix.ColumnCount)];
            }
        }
    }
}
