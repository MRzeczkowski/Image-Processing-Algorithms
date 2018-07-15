using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace APO
{
    public partial class UOPPanel : Form
    {
        private ImageHistogram hist;
        private Bitmap originalImage;
        private Bitmap secondaryImage;

        private Image_Panel caller;

        private int[] LUT;
        public Krzywa linia;
        private Graphics grafika;
        private BasicImageOpertions BasicImageOpertions;


        public UOPPanel(Bitmap image, Image_Panel caller)
        {
            InitializeComponent();

            this.caller = caller;

            originalImage = image;

            BasicImageOpertions = new BasicImageOpertions(originalImage);

            BasicImageOpertions.ToGreyScale();

            secondaryImage = (Bitmap)originalImage.Clone();

            this.pctbImage.Image = secondaryImage;

            hist = new ImageHistogram((Bitmap)pctbImage.Image);

            hist.CreateChart(this.chHisto);

            this.pctbUOP.Image = new Bitmap(256, 256);

            grafika = Graphics.FromImage(pctbUOP.Image);

            LUT = new int[256];
            linia = new Krzywa();
            RysujLinie();
        }

        private void RysujLinie()
        {   
            pctbUOP.Refresh();
            linia.TworzTablice();        //Konwertuje liste na tablice skladajaca sie z punktow - dopisac sortowanie po X            
            grafika.DrawCurve(linia.Pen, linia.TSkladoweKrzywej, linia.Tension);
            for (int i = 0; i < linia.LSkladoweKrzywej.Count; i++)
                grafika.DrawRectangle(linia.Pen, linia.LSkladoweKrzywej[i].X - 3, linia.LSkladoweKrzywej[i].Y - 3, 7, 7);
            pctbUOP.Refresh();
            TworzTabliceLUT(linia.TSkladoweKrzywej);
            /*for (int i = 0; i < 256; i++)
                histogram.Edit(LUT[i], i);
            histogram_Modified(histogram);*/
        }

        public void TworzTabliceLUT(Point[] tablicaZrodlowa)
        {
            int liczbaPunktow = tablicaZrodlowa.Length;
            int poczatek = 0;
            float b, c, e, f;
            float A, B;
            b = tablicaZrodlowa[0].X;
            c = tablicaZrodlowa[0].Y;

            for (int i = 1; i < liczbaPunktow; i++)
            {
                e = tablicaZrodlowa[i].X;
                f = tablicaZrodlowa[i].Y;

                //MessageBox.Show(tablicaZrodlowa[i].X.ToString() + " " + tablicaZrodlowa[i].Y.ToString());
                if ((e - b) != 0)
                {
                    B = (c * e - b * f) / (e - b);
                    A = (f - c) / (e - b);

                    for (; poczatek <= tablicaZrodlowa[i].X; poczatek++)
                    {
                        LUT[poczatek] = (int)(A * poczatek + B);
                    }
                }
                b = e;
                c = f;
            }
        }

        private void pctbUOP_MouseDown(object sender, MouseEventArgs e)
        {
            grafika.Clear(Color.White);
            if (e.Button == MouseButtons.Left)
            {
                Point punkt = e.Location;

                for (int i = 0; i < linia.LSkladoweKrzywej.Count; i++)
                    if ((punkt.X <= linia.LSkladoweKrzywej[i].X + 5 && punkt.X >= linia.LSkladoweKrzywej[i].X - 5) &&   //sprawdza czy klikniety punkt nalezy do punktu na linii
                        (punkt.Y <= linia.LSkladoweKrzywej[i].Y + 5 && punkt.Y >= linia.LSkladoweKrzywej[i].Y - 5))
                    {
                        RysujLinie();
                        grafika.DrawRectangle(new Pen(Color.Navy, 2), linia.LSkladoweKrzywej[i].X - 3, linia.LSkladoweKrzywej[i].Y - 3, 7, 7); //zaznacza klikniety punkt na czerwono
                        linia.Zaznaczony = i;
                        pctbImage.Refresh();
                        return;
                    }
                punkt.X--; //zmniejsza o jeden by zniwelowac roznice (panel rozmiar 1-256 a tablica 0-255)
                punkt.Y--;
                linia.LSkladoweKrzywej.Add(punkt);  //dodaje klikniety punkt do listy
                RysujLinie();
            }
            else //jesli prawy przycisk myszy to usuwamy punkt
            {
                if (linia.Zaznaczony > 0 && linia.LSkladoweKrzywej.Count > 2 && linia.Zaznaczony != linia.LSkladoweKrzywej.Count - 1)
                {                    
                    linia.LSkladoweKrzywej.RemoveAt(linia.Zaznaczony);
                    grafika.Clear(Color.White);
                    RysujLinie();
                }
            }

            //ChangeImage(secondaryImage);
            //originalImage = (Bitmap)secondaryImage.Clone();
            secondaryImage = (Bitmap)originalImage.Clone();
            pctbImage.Image = ChangeImage(secondaryImage);
            hist = new ImageHistogram((Bitmap)pctbImage.Image);
            hist.CreateChart(chHisto);

            pctbImage.Refresh();
        }

        private Bitmap ChangeImage(Bitmap bitmap)
        {
            BitmapData bitmapData = BitmapToBitmapData(bitmap);

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
                        //for (int z = 0; z < 256; ++z)
                        {                            
                            currentLine[x] = (byte)(255 - LUT[currentLine[x]]);
                            currentLine[x + 1] = (byte)(255 - LUT[currentLine[x + 1]]);
                            currentLine[x + 2] = (byte)(255 - LUT[currentLine[x + 2]]);
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);
            }

            return bitmap;
        }

        private BitmapData BitmapToBitmapData(Bitmap bmp)
        {
            BitmapData bitmapData = new BitmapData();
            if (bmp != null)
            {
                bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                return bitmapData;
            }

            return bitmapData;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            originalImage = ChangeImage(originalImage);
            caller.SetImageAndHist(originalImage);
            this.Dispose();
        }

        private void pctbUOP_MouseMove(object sender, MouseEventArgs e)
        {
            this.lbXValue.Text = e.Location.X.ToString();
            this.lbYValue.Text = (255 - e.Location.Y).ToString();
        }
    }
}
