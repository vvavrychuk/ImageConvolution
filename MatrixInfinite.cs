using System;

namespace Convolution
{
    abstract class MatrixInfinite
    {
        protected Matrix values;

        public MatrixInfinite(Matrix values)
        {
            this.values = values;
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

        public MatrixInfiniteMod(Matrix values) : base(values)
        {
        }

        public override double this[int i, int j]
        {
            get
            {
                return values[ModFloor(i, values.RowCount), ModFloor(j, values.ColumnCount)];
            }
        }
    }

    class MatrixInfiniteZero : MatrixInfinite
    {
        public MatrixInfiniteZero(Matrix values) : base(values)
        {
        }

        public override double this[int i, int j]
        {
            get
            {
                if ((i >= 0) && (i < values.RowCount) && (j >= 0) && (j < values.ColumnCount))
                {
                    return values[i, j];
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
