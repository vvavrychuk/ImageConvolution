using MathNet.Numerics.LinearAlgebra;
using System;

namespace Convolution
{
    abstract class MatrixInfinite
    {
        protected Matrix<double> matrix;

        public MatrixInfinite(Matrix<double> matrix)
        {
            this.matrix = matrix;
        }

        public abstract double this[int i, int j]
        {
            get;
        }
    }

    class MatrixInfiniteMod : MatrixInfinite
    {
        static int ModFloor(int a, int n)
        {
            return a - n * (int)Math.Floor((double)a / n);
        }

        public MatrixInfiniteMod(Matrix<double> matrix) : base(matrix)
        {
        }

        public override double this[int i, int j]
        {
            get
            {
                return matrix[ModFloor(i, matrix.RowCount), ModFloor(j, matrix.ColumnCount)];
            }
        }
    }

    class MatrixInfiniteZero : MatrixInfinite
    {
        public MatrixInfiniteZero(Matrix<double> matrix) : base(matrix)
        {
        }

        public override double this[int i, int j]
        {
            get
            {
                if ((i >= 0) && (i < matrix.RowCount) && (j >= 0) && (j < matrix.ColumnCount))
                {
                    return matrix[i, j];
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
