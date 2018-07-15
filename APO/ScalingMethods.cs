using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO
{
    class ScalingMethods
    {
        private Bitmap bitmap;

        public ScalingMethods(Bitmap bitmap)
        {
            this.bitmap = (Bitmap)bitmap.Clone();

            FindMax();
            FindMin();
        }

        private int min = 255;
        private int max = 0;

        public int Scale(int value, int type)
        {
            switch(type)
            {
                case 1:
                    return ProportionalScaling(value);

                case 2:
                    return TriValueScaling(value);

                case 3:
                    return CutDownScaling(value);

                default:
                    return value;
            }
        }

        private int ProportionalScaling(int value)
        {
            int result = ((value - min) / (max - min)) * 255;
            return (result);
        }

        private int TriValueScaling(int value)
        {
            if (value > 255)
            {
                return 255;
            }

            if (value < 0)
            {
                return 0;
            }

            return 128;
        }

        private int CutDownScaling(int value)
        {
            if (value > 255)
            {
                return 255;
            }

            if (value < 0)
            {
                return 0;
            }

            return value;
        }

        private void FindMin()
        {
            try
            {
                BitmapData bitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;


                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            if (currentLine[x] < min)
                            {
                                min = currentLine[x];
                            }
                        }
                    }
                    bitmap.UnlockBits(bitmapData);
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        private void FindMax()
        {
            try
            {
                BitmapData bitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;


                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            if (currentLine[x] > max)
                            {
                                max = currentLine[x];
                            }
                        }
                    }
                    bitmap.UnlockBits(bitmapData);
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }
    }
}
