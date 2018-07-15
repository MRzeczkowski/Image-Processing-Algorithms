using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

namespace APO
{
    public class ImageHistogram
    {
        private Bitmap bitmap;
        private Bitmap Source;

        private Image_Panel caller;

        public int max
        {
            get
            {
                return maxR;
            }
        }

        public int min
        {
            get
            {
                return minR;
            }
        }

        private int maxR = 0;
        private int maxG = 0;
        private int maxB = 0;

        private int minR = 255;
        private int minG = 255;
        private int minB = 255;

        BitmapData bitmapData;

        private Dictionary<int, int> toneHistogram = new Dictionary<int, int>();
        private Dictionary<Color, int> colorHistogram = new Dictionary<Color, int>();

        private int[] Red = new int[256];
        private int[] Green = new int[256];
        private int[] Blue = new int[256];

        public ImageHistogram(Bitmap Source)
        {
            this.Source = Source;
            this.bitmap = (Bitmap)Source.Clone();
        }

        public ImageHistogram(Bitmap Source, Form caller)
        {
            this.Source = Source;
            this.bitmap = (Bitmap)Source.Clone();

            this.caller = (Image_Panel)caller;
        }

        public void CreateChart(Chart chart)
        {
            bool ifGrey = true;

            chart.Series.Clear();

            CreateHistogram();

            for (int i = 0; i < 256; ++i)
            {
                if (Red[i] != Green[i] || Red[i] != Blue[i])
                {
                    ifGrey = false;
                }
            }

            if (ifGrey)
            {
                chart.Series.Add("Grayscale");
                chart.Series["Grayscale"].Color = Color.Gray;

                for (int i = 0; i < 256; ++i)
                {
                    chart.Series["Grayscale"].Points.AddXY(i, Red[i]);
                }
            }
            else
            {
                chart.Series.Add("R");
                chart.Series.Add("G");
                chart.Series.Add("B");

                chart.Series["R"].Color = Color.Red;
                chart.Series["G"].Color = Color.Green;
                chart.Series["B"].Color = Color.Blue;

                for (int i = 0; i < 256; ++i)
                {
                    chart.Series["R"].Points.AddXY(i, Red[i]);
                    chart.Series["G"].Points.AddXY(i, Green[i]);
                    chart.Series["B"].Points.AddXY(i, Blue[i]);
                }
            }
        }

        public void CreateHistogram()
        {
            toneHistogram.Clear();

            for (int i = 0; i < 256; ++i)
            {
                toneHistogram.Add(i, 0);

                Red[i] = 0;
                Green[i] = 0;
                Blue[i] = 0;
            }

            try
            {
                bitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);

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
                            Color col = Color.FromArgb(255, currentLine[x + 2], currentLine[x + 1], currentLine[x]);

                            Blue[currentLine[x]]++;
                            Green[currentLine[x + 1]]++;
                            Red[currentLine[x + 2]]++;


                            if (colorHistogram.ContainsKey(col))
                            {
                                colorHistogram[col] = colorHistogram[col] + 1;
                            }
                            else
                            {
                                colorHistogram.Add(col, 1);
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
            SetMinAndMax();
        }

        private void SetMinAndMax()
        {
            foreach (var key in colorHistogram.Keys)
            {
                if (key.R > maxR)
                {
                    maxR = key.R;
                }

                if (key.R < minR)
                {
                    minR = key.R;
                }

                if (key.G > maxG)
                {
                    maxG = key.G;
                }

                if (key.G < minG)
                {
                    minG = key.G;
                }

                if (key.B > maxB)
                {
                    maxB = key.B;
                }

                if (key.B < minB)
                {
                    minB = key.B;
                }
            }
        }

        public void StretchHistogram()
        {
            CreateHistogram();
            
            int newR;
            int newG;
            int newB;

            int diffR = maxR - minR;
            int diffG = maxG - minG;
            int diffB = maxB - minB;

            try
            {
                bitmapData = BasicImageOpertions.BitmapToBitmapData(bitmap);

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
                            newR = (int)((currentLine[x + 2] - minR) * 255 / diffR);
                            newG = (int)((currentLine[x + 1] - minG) * 255 / diffG);
                            newB = (int)((currentLine[x] - minB) * 255 / diffB);

                            currentLine[x + 2] = (byte)newR;
                            currentLine[x + 1] = (byte)newG;
                            currentLine[x] = (byte)newB;
                        }
                    }
                    bitmap.UnlockBits(bitmapData);
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            caller.SetImageAndHist(bitmap);
        }

        public void EqulizeHist(int method)
        {
            CreateHistogram();
                        
            Bitmap tmp = (Bitmap)bitmap.Clone();

            int Rr = 0;
            int Rg = 0;
            int Rb = 0;

            int HintR = 0;
            int HintG = 0;
            int HintB = 0;

            int HavgR = (int)Red.Average();
            int HavgG = (int)Green.Average();
            int HavgB = (int)Blue.Average();

            int[] leftR = new int[toneHistogram.Count];
            int[] rightR = new int[toneHistogram.Count];
            int[] _newR = new int[toneHistogram.Count];

            int[] leftG = new int[toneHistogram.Count];
            int[] rightG = new int[toneHistogram.Count];
            int[] _newG = new int[toneHistogram.Count];

            int[] leftB = new int[toneHistogram.Count];
            int[] rightB = new int[toneHistogram.Count];
            int[] _newB = new int[toneHistogram.Count];
            
            int z;

            for (z = 0; z < toneHistogram.Count - 1; ++z)
            {
                leftR[z] = Rr;
                HintR += Red[z];

                leftG[z] = Rg;
                HintG += Green[z];

                leftB[z] = Rb;
                HintB += Blue[z];

                while (HintR > HavgR)
                {
                    HintR -= HavgR;
                    Rr++;
                }
                if (Rr > toneHistogram.Count - 1) Rr = toneHistogram.Count - 1;

                while (HintG > HavgG)
                {
                    HintG -= HavgG;
                    Rg++;
                }
                if (Rg > toneHistogram.Count - 1) Rg = toneHistogram.Count - 1;

                while (HintB > HavgB)
                {
                    HintB -= HavgB;
                    Rb++;
                }
                if (Rb > toneHistogram.Count - 1) Rb = toneHistogram.Count - 1;

                rightR[z] = Rr;
                rightG[z] = Rg;
                rightB[z] = Rb;

                switch (method)
                {
                    case 1:
                        _newR[z] = (leftR[z] + rightR[z]) / 2;
                        _newG[z] = (leftG[z] + rightG[z]) / 2;
                        _newB[z] = (leftB[z] + rightB[z]) / 2;
                        break;

                    case 2:
                        _newR[z] = rightR[z] - leftR[z];
                        _newG[z] = rightG[z] - leftG[z];
                        _newB[z] = rightB[z] - leftB[z];
                        break;

                    case 3:
                        _newR[z] = (leftR[z] + rightR[z]) / 2;
                        _newG[z] = (leftG[z] + rightG[z]) / 2;
                        _newB[z] = (leftB[z] + rightB[z]) / 2;
                        break;

                    case 4:
                        _newR[z] = (leftR[z] + rightR[z]) / 2;
                        _newG[z] = (leftG[z] + rightG[z]) / 2;
                        _newB[z] = (leftB[z] + rightB[z]) / 2;
                        break;
                }
            }

            Random random = new Random();

            try
            {
                bitmapData = BasicImageOpertions.BitmapToBitmapData(tmp);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(tmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;


                unsafe // C# doesn't support pointer arithmetic
                {
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0; // Point to first pixel

                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        byte* HelperPointer = PtrFirstPixel + (y * bitmapData.Stride);

                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            byte colorR = 0;
                            byte colorG = 0;
                            byte colorB = 0;

                            byte valR = currentLine[x + 2];
                            byte valG = currentLine[x + 1];
                            byte valB = currentLine[x];

                            if (leftR[valR] == rightR[valR])
                            {
                                colorR = (byte)leftR[valR];
                            }
                            if (leftG[valG] == rightG[valG])
                            {
                                colorG = (byte)leftG[valG];
                            }
                            if (leftB[valB] == rightB[valB])
                            {
                                colorB = (byte)leftB[valB];
                            }
                            else
                            {
                                switch (method)
                                {
                                    case 1: //Average
                                        colorR = (byte)_newR[valR];
                                        colorG = (byte)_newG[valG];
                                        colorB = (byte)_newB[valB];
                                        break;

                                    case 2: //Random
                                        int valueR = random.Next(0, _newR[valR]);
                                        colorR = (byte)(leftR[valR] + valueR);

                                        int valueG = random.Next(0, _newG[valG]);
                                        colorG = (byte)(leftG[valG] + valueG);

                                        int valueB = random.Next(0, _newB[valB]);
                                        colorB = (byte)(leftR[valB] + valueB);

                                        break;

                                    case 3://Neighbour
                                        double averageR = 0;
                                        double averageG = 0;
                                        double averageB = 0;

                                        int count = 0;
                                        foreach (Point offset in new Point[] { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1), new Point(1, 1), new Point(-1, -1), new Point(-1, 1), new Point(1, -1) })
                                        {
                                            if (x/bytesPerPixel + offset.X >= 0 && x/bytesPerPixel + offset.X < bitmap.Width && y + offset.Y >= 0 && y + offset.Y < bitmap.Height)
                                            {
                                                Color col = bitmap.GetPixel(x/bytesPerPixel + offset.X, y + offset.Y);

                                                averageR = col.R;
                                                averageG = col.G;
                                                averageB = col.B;

                                                ++count;
                                            }
                                        }
                                        averageR /= count;
                                        averageG /= count;
                                        averageB /= count;

                                        if (averageR > rightR[valR])
                                            colorR = (byte)rightR[valR];
                                        else if (averageR < leftR[valR])
                                            colorR = (byte)leftR[valR];
                                        else
                                            colorR = (byte)((int)averageR);

                                        if (averageG > rightG[valG])
                                            colorG = (byte)rightG[valG];
                                        else if (averageG < leftG[valG])
                                            colorG = (byte)leftG[valG];
                                        else
                                            colorG = (byte)((int)averageG);

                                        if (averageB > rightR[valB])
                                            colorB = (byte)rightB[valB];
                                        else if (averageB < leftB[valB])
                                            colorB = (byte)leftB[valB];
                                        else
                                            colorB = (byte)((int)averageB);


                                        break;

                                    case 4: //Own
                                        averageR = 0;
                                        averageG = 0;
                                        averageB = 0;

                                        count = 0;
                                        foreach (Point offset in new Point[] { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1), new Point(1, 1), new Point(-1, -1), new Point(-1, 1), new Point(1, -1) })
                                        {
                                            if (x/bytesPerPixel + offset.X >= 0 && x/bytesPerPixel + offset.X < bitmap.Width && y + offset.Y >= 0 && y + offset.Y < bitmap.Height)
                                            {
                                                Color col = bitmap.GetPixel(x / bytesPerPixel + offset.X, y + offset.Y);

                                                averageR = col.R;
                                                averageG = col.G;
                                                averageB = col.B;

                                                ++count;

                                                ++count;
                                            }
                                        }
                                        averageR /= count;
                                        averageG /= count;
                                        averageB /= count;

                                        if (averageR > rightR[valR])
                                            colorR = (byte)rightR[valR];
                                        else if (averageR < leftR[valR])
                                            colorR = (byte)leftR[valR];
                                        else
                                            colorR = (byte)((int)averageR);

                                        if (averageG > rightG[valG])
                                            colorG = (byte)rightG[valG];
                                        else if (averageG < leftG[valG])
                                            colorG = (byte)leftG[valG];
                                        else
                                            colorG = (byte)((int)averageG);

                                        if (averageB > rightB[valB])
                                            colorB = (byte)rightB[valB];
                                        else if (averageB < leftB[valB])
                                            colorB = (byte)leftB[valB];
                                        else
                                            colorB = (byte)((int)averageB);

                                        break;

                                    default:
                                        break;
                                }
                            }

                            currentLine[x + 2] = colorR;
                            currentLine[x + 1] = colorG;
                            currentLine[x] = colorB;
                        }
                    }
                    
                    tmp.UnlockBits(bitmapData);
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please select image first", "Error");
            }
            caller.SetImageAndHist(tmp);
        }
    }
}
