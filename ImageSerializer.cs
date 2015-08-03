using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolution
{
    class ImageSerializer
    {
        public static Matrix ReadImage(string path)
        {
            var bitmap = new Bitmap(path);
            var bitmap2 = new Matrix(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap2[i, j] = (bitmap.GetPixel(i, j).R + bitmap.GetPixel(i, j).G + bitmap.GetPixel(i, j).B) / 255.0 / 3.0;
                }
            }
            return bitmap2;
        }

        public static void SaveImage(Matrix bitmap, string path)
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
    }
}
