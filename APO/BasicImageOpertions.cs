using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO
{
    // Manipulate method (bitmap)
    public class ImageEventArgs : EventArgs
    {
        public Bitmap bmap { get; set; }

    }

    public class BasicImageOpertions
    {
        public Bitmap bmp { get; set; }

        private BitmapData bitmapData;
        
        int bytesPerPixel;
        int heightInPixels;
        int widthInBytes;

        private Bitmap SecondaryBitmap;

        public BasicImageOpertions(Bitmap bitmap)
        {
            if (bitmap == null) return;

            this.bmp = bitmap;
            SecondaryBitmap = (Bitmap)bmp.Clone();

            this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

            this.bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
            this.heightInPixels = bitmapData.Height;
            this.widthInBytes = bitmapData.Width * bytesPerPixel;

            bmp.UnlockBits(bitmapData);
        }


        public void ToGreyScale()
        {
            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            int avg = (currentLine[x] + currentLine[x + 1] + currentLine[x + 2]) / 3;

                            currentLine[x] = (byte)avg;
                            currentLine[x + 1] = (byte)avg;
                            currentLine[x + 2] = (byte)avg;
                            //GreyScale
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                
            }
        }

        public void Negative()
        {
            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLine[x] = (byte)(255 - currentLine[x]);
                            currentLine[x + 1] = (byte)(255 - currentLine[x + 1]);
                            currentLine[x + 2] = (byte)(255 - currentLine[x + 2]);
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void Reduction(int numOfLevels)
        {
            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            int tmp = (currentLine[x] + currentLine[x + 1] + currentLine[x + 2]) / 3;

                            tmp = ((int)(tmp / (256 / numOfLevels))) * (255 / (numOfLevels - 1));

                            currentLine[x] = (byte)tmp;
                            currentLine[x + 1] = (byte)tmp;
                            currentLine[x + 2] = (byte)tmp;
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void Threshold(int value)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            if (currentLine[x] <= value)
                            {
                                currentLine[x] = 0;
                                currentLine[x + 1] = 0;
                                currentLine[x + 2] = 0;
                            }
                            else
                            {
                                currentLine[x] = 255;
                                currentLine[x + 1] = 255;
                                currentLine[x + 2] = 255;
                            }
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void ThresholdWithRetention(int value)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            if (currentLine[x] <= value)
                            {
                                currentLine[x] = 0;
                                currentLine[x + 1] = 0;
                                currentLine[x + 2] = 0;
                            }                            
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void Stretching(int value1, int value2)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            if (currentLine[x] > value1 && currentLine[x] <= value2)
                            {
                                Console.Write("Dupaaaa");

                                currentLine[x] = (byte)((currentLine[x] - value1) * ((255) / (value2 - value1)));
                                currentLine[x + 1] = (byte)((currentLine[x + 1] - value1) * ((255) / (value2 - value1)));
                                currentLine[x + 2] = (byte)((currentLine[x + 2] - value1) * ((255) / (value2 - value1)));
                            }
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }

        }

       
        public void Brightness(int value)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            double val = currentLine[x] + value * (256 / 256.0);

                            if (val < 0) val = 0;
                            else if (val > 255) val = 255;
                            
                           currentLine[x] = (byte)val;
                           currentLine[x + 1] = (byte)val;
                           currentLine[x + 2] = (byte)val;
                            
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void ADD(Bitmap argument)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                BitmapData bitmapDataSecImage = BitmapToBitmapData(argument);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel1 = (byte*)bitmapData.Scan0; // Point to first pixel
                    byte* PtrFirstPixel2 = (byte*)bitmapDataSecImage.Scan0;

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineFirst = PtrFirstPixel1 + (y * bitmapData.Stride);
                        byte* currentLineSec = PtrFirstPixel2 + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLineFirst[x] += currentLineSec[x];
                            currentLineFirst[x + 1] += currentLineSec[x + 1];
                            currentLineFirst[x + 2] += currentLineSec[x + 2];
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                    argument.UnlockBits(bitmapDataSecImage);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }


        public void SUB(Bitmap argument)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                BitmapData bitmapDataSecImage = BitmapToBitmapData(argument);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel1 = (byte*)bitmapData.Scan0; // Point to first pixel
                    byte* PtrFirstPixel2 = (byte*)bitmapDataSecImage.Scan0;

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineFirst = PtrFirstPixel1 + (y * bitmapData.Stride);
                        byte* currentLineSec = PtrFirstPixel2 + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLineFirst[x] -= currentLineSec[x];
                            currentLineFirst[x + 1] -= currentLineSec[x + 1];
                            currentLineFirst[x + 2] -= currentLineSec[x + 2];
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                    argument.UnlockBits(bitmapDataSecImage);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }
        
        public void DIFF(Bitmap imgToAdd)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                BitmapData bitmapDataSecImage = BitmapToBitmapData(imgToAdd);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel1 = (byte*)bitmapData.Scan0; // Point to first pixel
                    byte* PtrFirstPixel2 = (byte*)bitmapDataSecImage.Scan0;

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineFirst = PtrFirstPixel1 + (y * bitmapData.Stride);
                        byte* currentLineSec = PtrFirstPixel2 + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLineFirst[x] = (byte)Math.Abs(currentLineFirst[x] - currentLineSec[x]);
                            currentLineFirst[x + 1] = (byte)Math.Abs(currentLineFirst[x + 1] - currentLineSec[x + 1]);
                            currentLineFirst[x + 2] = (byte)Math.Abs(currentLineFirst[x + 2] - currentLineSec[x + 2]);
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                    imgToAdd.UnlockBits(bitmapDataSecImage);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void OR(Bitmap imgToAdd)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                BitmapData bitmapDataSecImage = BitmapToBitmapData(imgToAdd);
                
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel1 = (byte*)bitmapData.Scan0; // Point to first pixel
                    byte* PtrFirstPixel2 = (byte*)bitmapDataSecImage.Scan0;

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineFirst = PtrFirstPixel1 + (y * bitmapData.Stride);
                        byte* currentLineSec = PtrFirstPixel2 + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLineFirst[x] = (byte)(currentLineFirst[x] | currentLineSec[x]);
                            currentLineFirst[x + 1] = (byte)(currentLineFirst[x + 1] | currentLineSec[x + 1]);
                            currentLineFirst[x + 2] = (byte)(currentLineFirst[x + 2] | currentLineSec[x + 2]);
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                    imgToAdd.UnlockBits(bitmapDataSecImage);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void AND(Bitmap imgToAdd)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                BitmapData bitmapDataSecImage = BasicImageOpertions.BitmapToBitmapData(imgToAdd);
                
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel1 = (byte*)bitmapData.Scan0; // Point to first pixel
                    byte* PtrFirstPixel2 = (byte*)bitmapDataSecImage.Scan0;

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineFirst = PtrFirstPixel1 + (y * bitmapData.Stride);
                        byte* currentLineSec = PtrFirstPixel2 + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLineFirst[x] = (byte)(currentLineFirst[x] & currentLineSec[x]);
                            currentLineFirst[x + 1] = (byte)(currentLineFirst[x + 1] & currentLineSec[x + 1]);
                            currentLineFirst[x + 2] = (byte)(currentLineFirst[x + 2] & currentLineSec[x + 2]);
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                    imgToAdd.UnlockBits(bitmapDataSecImage);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public void XOR(Bitmap imgToAdd)
        {
            this.ToGreyScale();

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                BitmapData bitmapDataSecImage = BasicImageOpertions.BitmapToBitmapData(imgToAdd);
                
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel1 = (byte*)bitmapData.Scan0; // Point to first pixel
                    byte* PtrFirstPixel2 = (byte*)bitmapDataSecImage.Scan0;
                    
                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineFirst = PtrFirstPixel1 + (y * bitmapData.Stride);
                        byte* currentLineSec = PtrFirstPixel2 + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            currentLineFirst[x] = (byte)(currentLineFirst[x] ^ currentLineSec[x]);
                            currentLineFirst[x + 1] = (byte)(currentLineFirst[x + 1] ^ currentLineSec[x + 1]);
                            currentLineFirst[x + 2] = (byte)(currentLineFirst[x + 2] ^ currentLineSec[x + 2]);
                        }
                    }
                    bmp.UnlockBits(bitmapData);
                    imgToAdd.UnlockBits(bitmapDataSecImage);
                }
                OnImageFinished(bmp);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
        }

        public static BitmapData BitmapToBitmapData(Bitmap bitmap)
        {
            BitmapData bitmapData = new BitmapData();
            if (bitmap != null)
            {                
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                return bitmapData;
            }

            return bitmapData;
        }

        public BitmapData CloneBitmapData()
        {
            BitmapData SecondaryBitmapData = new BitmapData();
            if (bmp != null)
            {
                SecondaryBitmapData = SecondaryBitmap.LockBits(new Rectangle(0, 0, SecondaryBitmap.Width, SecondaryBitmap.Height), ImageLockMode.ReadWrite, SecondaryBitmap.PixelFormat);
                return SecondaryBitmapData;
            }

            return SecondaryBitmapData;
        }

        // Define the EventHandler Delegate
        public event EventHandler<ImageEventArgs> ImageFinished;

        // Add event Method
        protected virtual void OnImageFinished(Bitmap bmap)
        {
            ImageFinished?.Invoke(this, new ImageEventArgs() { bmap = bmp });
        }

    }
}
