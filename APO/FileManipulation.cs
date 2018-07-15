using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace APO
{
    class FileManipulation
    {
        Bitmap tmpImage;
        
        //Bitmap newImage;

        public string fileName{ get; set; }

        public unsafe Bitmap OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "C:\\Images";
            ofd.Filter = "images| *.jpg; *.png; *.bmp; *.gif;";

            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tmpImage = new Bitmap(Image.FromFile(ofd.FileName));
                fileName = Path.GetFileName(ofd.FileName);
            }
            
            //newImage = new Bitmap(tmpImage.Width, tmpImage.Height, PixelFormat.Format8bppIndexed);

            //ColorPalette pal = newImage.Palette;
            //for (int i = 0; i < pal.Entries.Length; i++)
            //    pal.Entries[i] = Color.FromArgb(i, i, i);
            //newImage.Palette = pal;
            //BitmapData bitmapData = new BitmapData();

            //bitmapData = newImage.LockBits(new Rectangle(0, 0, newImage.Width, newImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            //if (tmpImage != null)
            //{
            //    Byte* pPixel = (Byte*)bitmapData.Scan0;
            //    for (int y = 0; y < tmpImage.Height; y++)
            //    {
            //        for (int x = 0; x < tmpImage.Width; x++)
            //        {
            //            Color clr = tmpImage.GetPixel(x, y);
            //            Byte byPixel = (byte)((30 * clr.R + 59 * clr.G + 11 * clr.B) / 100);
            //            pPixel[x] = byPixel;
            //        }
            //        pPixel += bitmapData.Stride;
            //    }
            //}

            //newImage.UnlockBits(bitmapData);

            return tmpImage;
        }

        

       

        public static void SaveFile(Bitmap bmp)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = "C:\\Images";
            sfd.Filter = "images| *.jpg; *.png; *.bmp; *gif;";

            if (sfd.ShowDialog() == DialogResult.OK && bmp != null)
            {
                bmp.Save(sfd.FileName);
            }
        }
    }
}
