using MathNet.Numerics.LinearAlgebra;

namespace Convolution
{
    class Matrix
    {
        Matrix<double> values;
        int[,] ranges;

        public Matrix(int rowCount, int columnCount)
        {
            values = Matrix<double>.Build.Dense(rowCount, columnCount);
            ranges = new int[,] { { 0, rowCount }, { 0, columnCount } };
        }

        public Matrix(Matrix<double> values)
        {
            this.values = values;
            ranges = new int[,] { { 0, values.RowCount}, { 0, values.ColumnCount } };
        }

        public Matrix(Matrix<double> values, int[,] ranges)
        {
            this.values = values;
            this.ranges = ranges;
        }

        public double this[int i, int j]
        {
            get
            {
                return values[i - ranges[0, 0], j - ranges[1, 0]];
            }

            set
            {
                values[i - ranges[0, 0], j - ranges[1, 0]] = value;
            }
        }

        public int[,] Ranges
        {
            get
            {
                return ranges;
            }
        }

        public int RowCount
        {
            get
            {
                return ranges[0, 1] - ranges[0, 0];
            }
        }

        public int ColumnCount
        {
            get
            {
                return ranges[1, 1] - ranges[1, 0];
            }
        }

        public Matrix Center()
        {
            var ranges = new int[,]
            {
                { -values.RowCount / 2, values.RowCount / 2 },
                { -values.ColumnCount / 2, values.ColumnCount / 2 }
            };
            return new Matrix(values.Clone(), ranges);
        }

        public static Matrix operator /(Matrix matrix, double c)
        {
            return new Matrix(matrix.values / c, matrix.ranges);
        }
    }
}
