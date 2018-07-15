using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace APO
{
    public partial class BeforeAfterPanel : Form
    {
        public enum Operacje
        {
            Progowanie,
            ProgowanieZZachowaniem,
            Redukcja,
            Jasnosc,
            Kontrast,
            Rozciaganie
        }

        Bitmap Origbitmap;
        Bitmap newBitmap;

        Operacje operacja;

        Image_Panel caller;

        BasicImageOpertions basicImage;

        public BeforeAfterPanel(Bitmap bitmap, Operacje operacja, Image_Panel caller)
        {
            InitializeComponent();

            this.Origbitmap = bitmap;
            newBitmap = (Bitmap)bitmap.Clone();
            
            basicImage = new BasicImageOpertions(this.newBitmap);
            basicImage.ImageFinished += OnImageFinished;

            this.pctbInput.Image = Origbitmap;
            this.operacja = operacja;

            this.caller = caller;

            switch (operacja)
            {
                case Operacje.Progowanie:
                    tbArgument.Minimum = 0;
                    tbArgument.Maximum = 255;
                    tbArgument.Value = 127;
                    this.Text = "Progowanie";
                    basicImage.Threshold(tbArgument.Value);
                    break;

                case Operacje.ProgowanieZZachowaniem:
                    tbArgument.Minimum = 0;
                    tbArgument.Maximum = 255;
                    tbArgument.Value = 127;
                    this.Text = "Progowanie";
                    basicImage.ThresholdWithRetention(tbArgument.Value);
                    break;

                case Operacje.Rozciaganie:
                    this.tbSecondArgument.Enabled = true;
                    this.tbSecondArgument.Visible = true;
                    this.lbValue2.Visible = true;

                    tbArgument.Minimum = 0;
                    tbArgument.Maximum = 255;
                    tbArgument.Value = 127;

                    tbSecondArgument.Minimum = 0;
                    tbSecondArgument.Maximum = 255;
                    tbSecondArgument.Value = 127;

                    this.Text = "Rozciaganie";
                    basicImage.Stretching(tbArgument.Value, tbSecondArgument.Value);
                    break;

                case Operacje.Jasnosc:
                    tbArgument.Minimum = -255;
                    tbArgument.Maximum = 255;
                    tbArgument.Value = 0;
                    this.Text = "Jasność";
                    basicImage.Brightness(tbArgument.Value);
                    break;

                case Operacje.Redukcja:
                    tbArgument.Minimum = 2;
                    tbArgument.Maximum = 255;
                    tbArgument.Value = 127;
                    this.Text = "Redukcja";
                    basicImage.Reduction(tbArgument.Value);
                    break;
            }

            this.lbValue.Text = tbArgument.Value.ToString();
            this.lbValue2.Text = tbSecondArgument.Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.caller.SetImageAndHist(basicImage.bmp);
            this.Close();
        }

        private void DisplayImage(Bitmap bitmap)
        {
            this.pctbEffect.Image = bitmap;
        }

        private void OnImageFinished(object sender, ImageEventArgs e)
        {
            DisplayImage(e.bmap);
        }

        private void CheckCorrectPositon()
        {
            if (tbArgument.Value > tbSecondArgument.Value)
            {
                tbArgument.Value = tbSecondArgument.Value;
            }

            if (tbSecondArgument.Value < tbArgument.Value)
            {
                tbSecondArgument.Value = tbArgument.Value;
            }
        }

        private void tbArgument_MouseUp(object sender, MouseEventArgs e)
        {
            newBitmap = (Bitmap)Origbitmap.Clone();

            basicImage.bmp = this.newBitmap;

            CheckCorrectPositon();

            switch (this.operacja)
            {
                case Operacje.Progowanie:
                    basicImage.Threshold(tbArgument.Value);
                    break;

                case Operacje.ProgowanieZZachowaniem:
                    basicImage.ThresholdWithRetention(tbArgument.Value);
                    break;

                case Operacje.Rozciaganie:
                    basicImage.Stretching(tbArgument.Value, tbSecondArgument.Value);
                    break;

                case Operacje.Jasnosc:
                    basicImage.Brightness(tbArgument.Value);
                    break;

                case Operacje.Redukcja:
                    basicImage.Reduction(tbArgument.Value);
                    break;
            }
        }

        private void tbSecondArgument_MouseUp(object sender, MouseEventArgs e)
        {
            newBitmap = (Bitmap)Origbitmap.Clone();

            basicImage.bmp = this.newBitmap;

            CheckCorrectPositon();

            switch (this.operacja)
            {
                case Operacje.Rozciaganie:
                    basicImage.Stretching(tbArgument.Value, tbSecondArgument.Value);
                    break;
            }
        }

        private void tbArgument_ValueChanged(object sender, EventArgs e)
        {
            this.lbValue.Text = tbArgument.Value.ToString();
        }

        private void tbSecondArgument_ValueChanged(object sender, EventArgs e)
        {
            this.lbValue2.Text = tbSecondArgument.Value.ToString();
        }

       
    }
}
