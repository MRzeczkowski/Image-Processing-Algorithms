using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO
{
    public class ImageCompression
    {
        public Bitmap bitmap { get; set; }

        private BitmapData bitmapData;
        private BitmapData secondaryBitmapData;

        private int bytesPerPixel;
        private int heightInPixels;
        private int widthInBytes;
        private int widthInPixels;

        private Bitmap SecondaryBitmap;

        public ImageCompression(Bitmap bitmap)
        {
            if (bitmap == null) return;

            this.bitmap = (Bitmap)bitmap.Clone();
            SecondaryBitmap = (Bitmap)bitmap.Clone();

            this.bitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);
            this.secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(SecondaryBitmap);

            this.bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            this.heightInPixels = bitmapData.Height;
            this.widthInBytes = bitmapData.Width * bytesPerPixel;
            this.widthInPixels = bitmapData.Width;

            bitmap.UnlockBits(bitmapData);
            SecondaryBitmap.UnlockBits(secondaryBitmapData);
        }

        public void kompresjaLZW()
        {
            FileStream fs = new FileStream("temp.tiff", FileMode.Create);
            bitmap.Save(fs, ImageFormat.Tiff);
            fs.Close();
            long oSize = bitmap.Width * bitmap.Height * 24 / 8;
            long nSize = new FileInfo("temp.tiff").Length;
            File.Delete("temp.tiff");
            MessageBox.Show("\nRozmiar przed kompresją: " + oSize
                + "\n\nRozmiar po kompresji: " + nSize
                + "\n\nStopień kompresji: " + (float)oSize / nSize, "Kompresja LZW");
        }

        public void kompresjaRLE()
        {
            int licznik = 0;
            int przed;
            int po = 0;
            float stopien;

            int pam;
            int pam2 = -128; // ?

            //bitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);
            //secondaryBitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);

            przed = bitmap.Width * bitmap.Height;
            try
            {
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            if (y >= heightInPixels) break;
                            

                            pam = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);

                            if (pam == pam2)
                            {
                                licznik = 0;

                                while (((pam = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x)) == pam2) && (licznik < 127))
                                {
                                    licznik++;
                                    x++;

                                    if (x >= widthInBytes * bytesPerPixel - 1)
                                    {
                                        x = 0;
                                        y++;
                                        if (y >= heightInPixels) break;
                                    }

                                    pam2 = pam;
                                }

                                po += 2;
                            }
                            else
                            {
                                licznik = 0;
                                while (((pam = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x)) == pam2) && (licznik < 127))
                                {
                                    licznik++;
                                    x++;

                                    if (x >= widthInBytes * bytesPerPixel - 1)
                                    {
                                        x = 0;
                                        y++;
                                        if (y >= heightInPixels) break;
                                    }

                                    pam2 = pam;
                                }
                                po = po + licznik + 1;
                            }

                            pam2 = pam;
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }                       

            stopien = (float)przed / (float)po;

            int bits = 0;
            for (int i = 1; i <= 8; i++)
            {
                int levels = (int)Math.Pow(2, i);
                if (levels < 256) continue;
                bits = i;
                break;
            }
            float div = bits / 8.0f;

            MessageBox.Show("\nRozmiar przed kompresją: " + przed * div
                + "\n\nRozmiar po kompresji: " + po * div
                + "\n\nStopień kompresji: " + stopien, "Kompresja RLE");
        }

        public void kompresjaREAD()
        {
            const int PIXELLEN = 1;
            const int WORDLEN = 1;

            int newColor = 255, lastColor = 0;
            int repeatCount = 0;
            int total = 0;

            int width = bitmap.Width;
            int height = bitmap.Height;
            int fld = width * height;
            int before, after;
            float stopien;

            before = fld * PIXELLEN;

            //Przeglad wierszami
            try
            {
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = 0 * bytesPerPixel; x < widthInBytes; x += bytesPerPixel)
                        {
                            newColor = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);

                            //Jesli ten sam kolor
                            if (newColor == lastColor)
                            {
                                repeatCount++;
                            }
                            else
                            {

                                if (repeatCount > 0)
                                {
                                    total += WORDLEN + PIXELLEN;
                                    repeatCount = 0;
                                }

                                total += WORDLEN + PIXELLEN;
                            }

                            lastColor = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }

            if (repeatCount > 0)
            {
                total += WORDLEN + PIXELLEN;
                repeatCount = 0;
            }

            //Przeglad kolumnami
            try
            {
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = 0 * bytesPerPixel; x < widthInBytes; x += bytesPerPixel)
                        {
                            newColor = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);

                            //Jesli ten sam kolor
                            if (newColor == lastColor)
                            {
                                repeatCount++;
                            }
                            else
                            {
                                if (repeatCount > 0)
                                {
                                    total += WORDLEN + PIXELLEN;
                                    repeatCount = 0;
                                }
                                total += WORDLEN + PIXELLEN;
                            }
                            lastColor = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
                        
            if (repeatCount > 0)
            {
                total += WORDLEN + PIXELLEN;
                repeatCount = 0;
            }

            after = total / 2;
            stopien = (float)(float)before / (float)after;

            int bits = 0;
            for (int i = 1; i <= 8; i++)
            {
                int levels = (int)Math.Pow(2, i);
                if (levels < 256) continue;
                bits = i;
                break;
            }
            float div = bits / 8.0f;

            MessageBox.Show("\nRozmiar przed kompresją: " + before * div
                            + "\n\nRozmiar po kompresji: " + after * div
                            + "\n\nStopień kompresji: " + stopien, "Kompresja READ");
        }

        public void kompresjaHuffman()
        {
            int xx = 2;
            int licznik = 0;
            int przed;
            int po = 0;
            float stopien;
            int moc = 2;
            int ilosc = 0;

            int[] phist = new int[256];
            przed = bitmap.Width * bitmap.Height;
                               

            try
            {
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = 0 * bytesPerPixel; x < widthInBytes; x += bytesPerPixel)
                        {
                                    int color = *(byte*)(PtrFirstPixelFirst + (y * bitmapData.Stride) + x);
                                    phist[color] += 1;
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }


            for (int i = 0; i < 256; i++)
            {
                if (phist[i] != 0)
                {
                    licznik++;
                    po += (licznik * phist[i]);

                    if (licznik >= Math.Pow((double)xx, (double)moc))
                    {
                        licznik = 0;
                        moc++;
                    }
                    ilosc++;
                }

                if (licznik == 1)
                {
                    po = przed + 1;
                }
            }

            po /= 8;
            po += (ilosc * 2);

            stopien = (float)(float)przed / (float)po;

            int bits = 0;
            for (int i = 1; i <= 8; i++)
            {
                int levels = (int)Math.Pow(2, i);
                if (levels < 256) continue;
                bits = i;
                break;
            }
            float div = bits / 8.0f;

            MessageBox.Show("\nRozmiar przed kompresją: " + przed * div
                + "\n\nRozmiar po kompresji: " + po * div
                + "\n\nStopień kompresji: " + stopien, "Kompresja Huffman");
        }

        public Bitmap kompresjaBlokowa(int size)
        {
            int color = 0;
            float avg;
            float avgUP;
            float avgDOWN;
            int countUP;
            int countDOWN;
            int countAVG;
            int sizeAfter = 0;
            Bitmap bmp = (Bitmap)bitmap.Clone();

            try
            {
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = 0; y < heightInPixels; y+=size)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = 0 * bytesPerPixel; x < widthInBytes; x += bytesPerPixel*size)
                        {
                            avg = 0;
                            countAVG = 0;
                            for (int yy = 0; yy < size; ++yy)
                            {
                                for (int xx = 0; xx < size; ++xx)
                                {
                                    if (x + xx < widthInBytes && y + yy < heightInPixels)
                                    {
                                        color = *(byte*)(PtrFirstPixelFirst + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel);

                                        avg += color;
                                        ++countAVG;
                                    }
                                }
                            }
                            avg /= countAVG;
                            avg = (int)avg;

                            avgUP = 0;
                            avgDOWN = 0;
                            countDOWN = 0;
                            countUP = 0;
                            for (int yy = 0; yy < size; ++yy)
                            {
                                for (int xx = 0; xx < size; ++xx)
                                {
                                    if (x + xx < widthInBytes && y + yy < heightInPixels)
                                    {
                                        color = *(byte*)(PtrFirstPixelFirst + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel);
                                        if (color >= avg) { avgUP += color; ++countUP; }
                                        else { avgDOWN += color; ++countDOWN; }
                                    }
                                }
                            }
                            avgUP /= countUP;
                            avgDOWN /= countDOWN;
                            avgUP = (int)avgUP;
                            avgDOWN = (int)avgDOWN;

                            for (int yy = 0; yy < size; ++yy)
                                for (int xx = 0; xx < size; ++xx)
                                    if (x + xx < widthInBytes && y + yy < heightInPixels)
                                        if (*(byte*)(PtrFirstPixelFirst + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel) >= avg)
                                        {
                                            (PtrFirstPixelSecondary + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel)[x] = (byte)avgUP;
                                            (PtrFirstPixelSecondary + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel)[x + 1] = (byte)avgUP;
                                            (PtrFirstPixelSecondary + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel)[x + 2] = (byte)avgUP;                                            
                                        }
                                        else
                                        {                                            
                                            (PtrFirstPixelSecondary + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel)[x] = (byte)avgDOWN;
                                            (PtrFirstPixelSecondary + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel)[x + 1] = (byte)avgDOWN;
                                            (PtrFirstPixelSecondary + ((y + yy) * bitmapData.Stride) + x + xx * bytesPerPixel)[x + 2] = (byte)avgDOWN;
                                        } 

                            sizeAfter += countAVG + 16;
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }

            bitmap = SecondaryBitmap;
            

            int sizeBefore = bitmap.Size.Width * bitmap.Size.Height * 8;

            int bits = 0;
            for (int i = 1; i <= 8; i++)
            {
                int levels = (int)Math.Pow(2, i);
                if (levels < 256) continue;
                bits = i;
                break;
            }
            float div = bits / 8.0f;

            MessageBox.Show("\nRozmiar przed kompresją: " + sizeBefore * div
                            + "\n\nRozmiar po kompresji: " + sizeAfter * div
                            + "\n\nStopień kompresji: " + (float)sizeBefore / (float)sizeAfter, "Kompresja blokowa");

            return bmp;
        }

        public void kompresjaRoznicowa()
        {
            const int PIXELLEN = 1;
            const int WORDLEN = 1;

            const int block_size = 4;

            int width = bitmap.Width;
            int height = bitmap.Height;
            int fld = width * height;

            int before_size = fld * PIXELLEN;
            int after = 0;

            int i, j, I, J, K, ml, mu;
            float m;
            int[,] b = new int[block_size, block_size];

            try
            {
                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixelFirst = (byte*)bitmapData.Scan0; // Point to first pixel of first image
                    byte* PtrFirstPixelSecondary = (byte*)secondaryBitmapData.Scan0; // Point to first pixel of helper image

                    for (int y = 0; y < heightInPixels - block_size; y+= block_size)
                    {
                        byte* currentLineOld = PtrFirstPixelFirst + (y * bitmapData.Stride);
                        byte* currentLineNew = PtrFirstPixelSecondary + (y * secondaryBitmapData.Stride);

                        for (int x = 0 * bytesPerPixel; x < widthInBytes - block_size * bytesPerPixel; x += bytesPerPixel * block_size)
                        {
                            
                            m = 0;
                            mu = 0;
                            ml = 0; K = 0;

                            for (I = 0; I < block_size; I++)
                                for (J = 0; J < block_size; J++)
                                    m = m + *(byte*)(PtrFirstPixelFirst + ((y + J) * bitmapData.Stride) + x + I);

                            m /= block_size;

                            for (I = 0; I < block_size; I++)
                            {
                                for (J = 0; J < block_size; J++)
                                {
                                    int color = *(byte*)(PtrFirstPixelFirst + ((y + J) * bitmapData.Stride) + x + I);
                                    if (color > m)
                                    {
                                        mu = mu + color;
                                        b[I, J] = 1;
                                        K++;
                                    }
                                    else
                                    {
                                        ml = ml + color;
                                        b[I, J] = 0;
                                    }
                                }
                            }

                            if (K == 0) K = 1;
                            mu = mu / K;
                            ml = ml / (block_size * block_size - K);

                            after += K * PIXELLEN + WORDLEN;
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
                SecondaryBitmap.UnlockBits(secondaryBitmapData);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            
            float degree = (float)(float)before_size / (float)after;

            int bits = 0;
            for (int k = 1; k <= 8; k++)
            {
                int levels = (int)Math.Pow(2, k);
                if (levels < 256) continue;
                bits = k;
                break;
            }
            float div = bits / 8.0f;

            MessageBox.Show("\nRozmiar przed kompresją: " + before_size * div
                + "\n\nRozmiar po kompresji: " + after * div
                + "\n\nStopień kompresji: " + degree, "Kompresja blokowa");
        }
    }
}
