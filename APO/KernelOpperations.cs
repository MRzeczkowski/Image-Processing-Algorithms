using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO
{
    class KernelOpperations
    {
        public Bitmap bmp { get; set; }

        private BitmapData bitmapData;
        private BitmapData secondaryBitmapData;

        private int bytesPerPixel;
        private int heightInPixels;
        private int widthInBytes;
        private int widthInPixels;

        private ScalingMethods scale;
        
        private Bitmap SecondaryBitmap;
        
        public KernelOpperations(Bitmap bitmap)
        {
            if (bitmap == null) return;

            this.bmp = (Bitmap)bitmap.Clone();
            SecondaryBitmap = (Bitmap)bmp.Clone();

            this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
            this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

            this.bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
            this.heightInPixels = bitmapData.Height;
            this.widthInBytes = bitmapData.Width * bytesPerPixel;
            this.widthInPixels = bitmapData.Width;

            bmp.UnlockBits(bitmapData);
            SecondaryBitmap.UnlockBits(secondaryBitmapData);


            this.scale = new ScalingMethods(bmp);
        }

        public Bitmap LinearFilter(int[,] mask, int scaling_type, int outlierMethod)
        {
            int size = (int)Math.Sqrt(mask.Length);
            int index = size - size / 2 - 1;
            int divisor = 0;

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    divisor += mask[i, j];
                }
            }

            if (divisor == 0) divisor = 1;

            Point[,] temp = new Point[size, size];

            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);

            if(outlierMethod == 0)
            {
                index = 0;
            }

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = index; y < heightInPixels - index; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = index * bytesPerPixel; x < widthInBytes - index * bytesPerPixel; x += bytesPerPixel)
                        {
                            int newColor = 0;
                            
                            for (int k = 0; k < size; k++)
                            {
                                for (int l = 0; l < size; l++)
                                {
                                   if (x + temp[k, l].X * bytesPerPixel >= 0 && x + temp[k, l].X * bytesPerPixel < widthInBytes && y + temp[k, l].Y >= 0 && y + temp[k, l].Y < heightInPixels)
                                   {    
                                        byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);

                                        newColor += (int)(mask[k, l] * (int)*col);
                                   }
                                }
                            }
                            
                            newColor /= divisor;
                            int finalColor = newColor;
                            
                            finalColor = scale.Scale(newColor, scaling_type);
                                                        
                            currentLineNew[x] = (byte)finalColor;
                            currentLineNew[x + 1] = (byte)finalColor;
                            currentLineNew[x + 2] = (byte)finalColor;
                        }
                    }                    
                }
                bmp.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            return SecondaryBitmap;            
        }

        public Bitmap LogicalFilter(int angle)
        {
            int size = 3;
            int index = size - size / 2 - 1;
                        
            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = index; y < heightInPixels - index; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = index * bytesPerPixel; x < widthInBytes - index * bytesPerPixel; x += bytesPerPixel)
                        {   
                            byte* colCenter = (byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);

                            int newColor = *colCenter;

                            switch (angle)
                            {
                                case 0:
                                    byte* colLeft = (byte*)(PtrFirstPixelFirst + ((y + 0) * bitmapData.Stride) + x + 1 * bytesPerPixel);
                                    byte* colRight = (byte*)(PtrFirstPixelFirst + ((y + 0) * bitmapData.Stride) + x + (-1) * bytesPerPixel);

                                    if(*colLeft == *colRight)
                                    {
                                        newColor = *colRight;
                                    }

                                    break;

                                case 45:
                                    byte* colDownLeft = (byte*)(PtrFirstPixelFirst + ((y + 1) * bitmapData.Stride) + x + (-1) * bytesPerPixel);
                                    byte* colTopRight = (byte*)(PtrFirstPixelFirst + ((y + -1) * bitmapData.Stride) + x + 1 * bytesPerPixel);

                                    if (*colDownLeft == *colTopRight)
                                    {
                                        newColor = *colDownLeft;
                                    }

                                    break;
                                

                                case 90:
                                    byte* colTop = (byte*)(PtrFirstPixelFirst + ((y + -1) * bitmapData.Stride) + x + 0 * bytesPerPixel);
                                    byte* colDown = (byte*)(PtrFirstPixelFirst + ((y + 1) * bitmapData.Stride) + x + 0 * bytesPerPixel);

                                    if (*colTop == *colDown)
                                    {
                                        newColor = *colTop;
                                    }

                                    break;          
                                    
                                case 135:
                                    byte* colTopLeft = (byte*)(PtrFirstPixelFirst + ((y + -1) * bitmapData.Stride) + x + (-1) * bytesPerPixel);
                                    byte* colDownRight = (byte*)(PtrFirstPixelFirst + ((y + 1) * bitmapData.Stride) + x + 1 * bytesPerPixel);

                                    if (*colTopLeft == *colDownRight)
                                    {
                                        newColor = *colTopLeft;
                                    }

                                    break;                                
                            }
                                                        
                            int finalColor = newColor;
                            
                            currentLineNew[x] = (byte)finalColor;
                            currentLineNew[x + 1] = (byte)finalColor;
                            currentLineNew[x + 2] = (byte)finalColor;
                        }
                    }
                }
                bmp.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            return SecondaryBitmap;
        }



        public Bitmap MedianFilter(int m, int n, int outlierMethod)
        {            
            int indexM = m - m / 2 - 1;
            int indexN = n - n / 2 - 1;

            Point[,] temp = new Point[m, n];

            for (int i = -indexM; i <= indexM; i++)
                for (int j = -indexN; j <= indexN; j++)
                    temp[i + indexM, j + indexN] = new Point(i, j);

            byte[] pixels = new byte[m*n];

            if(outlierMethod == 0)
            {
                indexM = 0;
                indexN = 0;
            }
            
            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = indexN; y < heightInPixels - indexN; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = indexM * bytesPerPixel; x < widthInBytes - indexM * bytesPerPixel; x += bytesPerPixel)
                        {
                            int p = 0;

                            for (int k = 0; k < m; k++)
                            {
                                for (int l = 0; l < n; l++)
                                {
                                    if (x + temp[k, l].X * bytesPerPixel >= 0 && x + temp[k, l].X * bytesPerPixel < widthInBytes && y + temp[k, l].Y >= 0 && y + temp[k, l].Y < heightInPixels)
                                    {
                                        byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);
                                        pixels[p] = *col;
                                        ++p;
                                    }
                                }
                            }

                            Array.Sort(pixels);

                            byte finalColor = KernelOpperations.GetMedian(pixels);

                            currentLineNew[x] = (byte)finalColor;
                            currentLineNew[x + 1] = (byte)finalColor;
                            currentLineNew[x + 2] = (byte)finalColor;
                        }
                    }
                }
                bmp.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            return SecondaryBitmap;
        }

        public Bitmap TwoMaskFilter(int[,] maskHor, int[,] maskVer, int scaling_type, int outlierMethod)
        {
            int size = (int)Math.Sqrt(maskHor.Length);
            int index = size - size / 2 - 1;
            int divisorHor = 0;
            int divisorVer = 0;

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    divisorHor += maskHor[i, j];
                    divisorVer += maskVer[i, j];
                }
            }
            
            if(divisorHor == 0)
            {
                divisorHor = 1;
            }
            if (divisorVer == 0)
            {
                divisorVer = 1;
            }

            Point[,] temp = new Point[size, size];

            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);

            if (outlierMethod == 0)
            {
                index = 0;
            }

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = index; y < heightInPixels - index; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = index * bytesPerPixel; x < widthInBytes - index * bytesPerPixel; x += bytesPerPixel)
                        {
                            int newColorHor = 0;
                            int newColorVer = 0;

                            for (int k = 0; k < size; k++)
                            {
                                for (int l = 0; l < size; l++)
                                {
                                    if (x + temp[k, l].X * bytesPerPixel >= 0 && x + temp[k, l].X * bytesPerPixel < widthInBytes && y + temp[k, l].Y >= 0 && y + temp[k, l].Y < heightInPixels)
                                    {
                                        byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);

                                        newColorHor += (int)(maskHor[k, l] * (int)*col);
                                        newColorVer += (int)(maskVer[k, l] * (int)*col);
                                    }
                                }
                            }

                            newColorHor /= divisorHor;
                            newColorVer /= divisorVer;

                            //if (size == 2)
                            //    finalColor = Math.Abs(newColorHor) + Math.Abs(newColorVer);
                            //else
                            //    finalColor = (int)Math.Sqrt((newColorHor * newColorHor) + (newColorVer * newColorVer));

                            int finalColor = scale.Scale(newColorHor + newColorVer, scaling_type);

                            currentLineNew[x] = (byte)finalColor;
                            currentLineNew[x + 1] = (byte)finalColor;
                            currentLineNew[x + 2] = (byte)finalColor;
                        }
                    }
                }
                bmp.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            return SecondaryBitmap;
        }

        public Bitmap Dilatation(int scaling_type, int outlierMethod, int neighbours)
        {
            int size = 3;
            int index = size - size / 2 - 1;
            
            Point[,] temp = new Point[size, size];

            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);

            if (outlierMethod == 0)
            {
                index = 0;
            }

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = index; y < heightInPixels - index; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = index * bytesPerPixel; x < widthInBytes - index * bytesPerPixel; x += bytesPerPixel)
                        {
                            byte* colCenter = (byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);

                            int newColor = *colCenter;

                            for (int k = 0; k < size; k++)
                            {
                                for (int l = 0; l < size; l++)
                                {
                                    if (x + temp[k, l].X * bytesPerPixel >= 0 && x + temp[k, l].X * bytesPerPixel < widthInBytes && y + temp[k, l].Y >= 0 && y + temp[k, l].Y < heightInPixels)
                                    {
                                        if (neighbours == 4 && Math.Abs(temp[k, l].X) != Math.Abs(temp[k, l].Y))
                                        {
                                            byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);

                                            if (newColor <= *col)
                                            {
                                                newColor = *col;
                                            }
                                        }
                                        else
                                        {
                                            byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);

                                            if (newColor <= *col)
                                            {
                                                newColor = *col;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            int finalColor = newColor;

                            finalColor = scale.Scale(newColor, scaling_type);

                            currentLineNew[x] = (byte)finalColor;
                            currentLineNew[x + 1] = (byte)finalColor;
                            currentLineNew[x + 2] = (byte)finalColor;
                        }
                    }
                }
                bmp.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            return SecondaryBitmap;
        }

        public Bitmap Erosion(int scaling_type, int outlierMethod, int neighbours)
        {
            int size = 3;
            int index = size - size / 2 - 1;

            Point[,] temp = new Point[size, size];

            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);
                        

            if (outlierMethod == 0)
            {
                index = 0;
            }

            try
            {
                this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bmp);
                this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = index; y < heightInPixels - index; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = index * bytesPerPixel; x < widthInBytes - index * bytesPerPixel; x += bytesPerPixel)
                        {
                            byte* colCenter = (byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);

                            int newColor = *colCenter;

                            for (int k = 0; k < size; k++)
                            {
                                for (int l = 0; l < size; l++)
                                {
                                    if (x + temp[k, l].X * bytesPerPixel >= 0 && x + temp[k, l].X * bytesPerPixel < widthInBytes && y + temp[k, l].Y >= 0 && y + temp[k, l].Y < heightInPixels)
                                    {
                                        if (neighbours == 4 && Math.Abs(temp[k, l].X) != Math.Abs(temp[k, l].Y))
                                        {
                                            byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);

                                            if (newColor > *col)
                                            {
                                                newColor = *col;
                                            }
                                        }
                                        else
                                        {
                                            byte* col = (byte*)(PtrFirstPixelFirst + ((y + temp[k, l].Y) * bitmapData.Stride) + x + temp[k, l].X * bytesPerPixel);

                                            if (newColor > *col)
                                            {
                                                newColor = *col;
                                            }
                                        }
                                    }
                                }
                            }

                            int finalColor = newColor;

                            finalColor = scale.Scale(newColor, scaling_type);

                            currentLineNew[x] = (byte)finalColor;
                            currentLineNew[x + 1] = (byte)finalColor;
                            currentLineNew[x + 2] = (byte)finalColor;
                        }
                    }
                }
                bmp.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            return SecondaryBitmap;
        }

        public static bool[][] Image2Bool(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bool[][] s = new bool[bmp.Height][];
            for (int y = 0; y < bmp.Height; y++)
            {
                s[y] = new bool[bmp.Width];
                for (int x = 0; x < bmp.Width; x++)
                    s[y][x] = bmp.GetPixel(x, y).GetBrightness() < 0.3;
            }
            return s;

        }

        public static Image Bool2Image(bool[][] s)
        {
            Bitmap bmp = new Bitmap(s[0].Length, s.Length);
            using (Graphics g = Graphics.FromImage(bmp)) g.Clear(Color.White);
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    if (s[y][x]) bmp.SetPixel(x, y, Color.Black);

            return (Bitmap)bmp;
        }

        public static bool[][] ZhangSuenThinning(bool[][] s)
        {
            bool[][] temp = ArrayClone(s);  // make a deep copy to start.. 
            int count = 0;
            do  // the missing iteration
            {
                count = step(1, temp, s);
                temp = ArrayClone(s);      // ..and on each..
                count += step(2, temp, s);
                temp = ArrayClone(s);      // ..call!
            }
            while (count > 0);

            return s;
        }

        static int step(int stepNo, bool[][] temp, bool[][] s)
        {
            int count = 0;

            for (int a = 1; a < temp.Length - 1; a++)
            {
                for (int b = 1; b < temp[0].Length - 1; b++)
                {
                    if (SuenThinningAlg(a, b, temp, stepNo == 2))
                    {
                        // still changes happening?
                        if (s[a][b]) count++;
                        s[a][b] = false;
                    }
                }
            }
            return count;
        }

        static bool SuenThinningAlg(int x, int y, bool[][] s, bool even)
        {
            bool p2 = s[x][y - 1];
            bool p3 = s[x + 1][y - 1];
            bool p4 = s[x + 1][y];
            bool p5 = s[x + 1][y + 1];
            bool p6 = s[x][y + 1];
            bool p7 = s[x - 1][y + 1];
            bool p8 = s[x - 1][y];
            bool p9 = s[x - 1][y - 1];


            int bp1 = NumberOfNonZeroNeighbors(x, y, s);
            if (bp1 >= 2 && bp1 <= 6) //2nd condition
            {
                if (NumberOfZeroToOneTransitionFromP9(x, y, s) == 1)
                {
                    if (even)
                    {
                        if (!((p2 && p4) && p8))
                        {
                            if (!((p2 && p6) && p8))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!((p2 && p4) && p6))
                        {
                            if (!((p4 && p6) && p8))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        static int NumberOfZeroToOneTransitionFromP9(int x, int y, bool[][] s)
        {
            bool p2 = s[x][y - 1];
            bool p3 = s[x + 1][y - 1];
            bool p4 = s[x + 1][y];
            bool p5 = s[x + 1][y + 1];
            bool p6 = s[x][y + 1];
            bool p7 = s[x - 1][y + 1];
            bool p8 = s[x - 1][y];
            bool p9 = s[x - 1][y - 1];

            int A = Convert.ToInt32((!p2 && p3)) + Convert.ToInt32((!p3 && p4)) +
                    Convert.ToInt32((!p4 && p5)) + Convert.ToInt32((!p5 && p6)) +
                    Convert.ToInt32((!p6 && p7)) + Convert.ToInt32((!p7 && p8)) +
                    Convert.ToInt32((!p8 && p9)) + Convert.ToInt32((!p9 && p2));
            return A;
        }
        static int NumberOfNonZeroNeighbors(int x, int y, bool[][] s)
        {
            int count = 0;
            if (s[x - 1][y]) count++;
            if (s[x - 1][y + 1]) count++;
            if (s[x - 1][y - 1]) count++;
            if (s[x][y + 1]) count++;
            if (s[x][y - 1]) count++;
            if (s[x + 1][y]) count++;
            if (s[x + 1][y + 1]) count++;
            if (s[x + 1][y - 1]) count++;
            return count;
        }

        public static T[][] ArrayClone<T>(T[][] A)
        { return A.Select(a => a.ToArray()).ToArray(); }

        public static byte GetMedian(byte[] Value)
        {
            decimal Median = 0;
            int size = Value.Length;
            int mid = size / 2;
            Median = (size % 2 != 0) ? (decimal)Value[mid] : ((decimal)Value[mid] + (decimal)Value[mid + 1]) / 2;
            return Convert.ToByte(Math.Round(Median));
        }
    }
}
