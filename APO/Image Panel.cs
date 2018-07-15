using APO.K_means_Segmentation;
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
    public partial class Image_Panel : Form
    {
        ImageHistogram hist;

        Bitmap imageForReset;
        Bitmap secondaryImage;
        BasicImageOpertions basicImageOpertions;

        ImageSegmentation ImageSeg;


        public Bitmap originalImage{ get; set; }
        public string imageName { get; set; }

        public Image_Panel(Bitmap image, string imageName)
        {
            InitializeComponent();

            this.chHisto.Width = this.Width - panelImage.Width;

            imageForReset = (Bitmap)image.Clone();

            originalImage = image;
            this.imageName = imageName;

            this.Text = imageName;

            secondaryImage = (Bitmap)image.Clone();

            this.pctbImage.Image = image;
            this.pctbImage.Show();

            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
            
            hist.CreateChart(chHisto);
            this.pctbHistogram.Show();

            basicImageOpertions = new BasicImageOpertions((Bitmap)pctbImage.Image);

            basicImageOpertions.ImageFinished += OnImageFinished;
        }

        public void SetImageAndHist(Bitmap image)
        {
            originalImage = image;

            secondaryImage = (Bitmap)image.Clone();

            this.pctbImage.Image = null;

            this.pctbImage.Image = image;
            this.pctbImage.Show();

            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
                        
            hist.CreateChart(chHisto);
            this.pctbHistogram.Show();

            basicImageOpertions = new BasicImageOpertions((Bitmap)pctbImage.Image);

            basicImageOpertions.ImageFinished += OnImageFinished;

            this.Refresh();
        }

        public void SetImage(Bitmap image)
        {
            originalImage = image;

            secondaryImage = (Bitmap)image.Clone();

            this.pctbImage.Image = image;
            this.pctbImage.Show();
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {

            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
            hist.StretchHistogram();
            this.pctbImage.Show();

            
            this.pctbHistogram.Show();
            hist.CreateChart(chHisto);
        }

        private void averageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
            hist.EqulizeHist(1);
            this.pctbImage.Show();


            this.pctbHistogram.Show();
            hist.CreateChart(chHisto);            
        }

        private void randomValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
            hist.EqulizeHist(2);
            this.pctbImage.Show();


            this.pctbHistogram.Show();
            hist.CreateChart(chHisto);
        }

        private void neighbourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
            hist.EqulizeHist(3);
            this.pctbImage.Show();


            this.pctbHistogram.Show();
            hist.CreateChart(chHisto);
        }

        private void own1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);
            hist.EqulizeHist(4);
            this.pctbImage.Show();


            this.pctbHistogram.Show();
            hist.CreateChart(chHisto);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManipulation.SaveFile((Bitmap)pctbImage.Image);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pctbImage.Image = (Bitmap)imageForReset.Clone();
            originalImage = (Bitmap)imageForReset.Clone();
            
            
            hist = new ImageHistogram((Bitmap)pctbImage.Image, this);

            hist.CreateChart(chHisto);
            this.pctbHistogram.Show();
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void chHisto_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chHisto.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around the point)
                        if (Math.Abs(pos.X - pointXPixel) < 5 &&
                            Math.Abs(pos.Y - pointYPixel) < 5)
                        {
                            tooltip.Show("Value =" + prop.XValue + ", Amount =" + prop.YValues[0], this.chHisto,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        private void DisplayImage(Bitmap bitmap)
        {
            this.pctbImage.Image = bitmap;
        }

        public void RemakeHistogram()
        {
            hist.CreateChart(chHisto);
        }

        private void OnImageFinished(object sender, ImageEventArgs e)
        {
            DisplayImage(e.bmap);            
            hist.CreateChart(this.chHisto);
        }

        private void toGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            SetImageAndHist((Bitmap)pctbImage.Image);
        }

        private void negacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basicImageOpertions.Negative();
        }

        private void progowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeAfterPanel beforeAfterPanel = new BeforeAfterPanel((Bitmap)this.pctbImage.Image, BeforeAfterPanel.Operacje.Progowanie, this);
            beforeAfterPanel.Show();
        }

        private void jasnoscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeAfterPanel beforeAfterPanel = new BeforeAfterPanel((Bitmap)this.pctbImage.Image, BeforeAfterPanel.Operacje.Jasnosc, this);
            beforeAfterPanel.Show();
        }

        private void DrawHistoOnChange(object sender, AsyncCompletedEventArgs e)
        {
            hist.CreateChart(chHisto);
        }
        
        private void progowanieZZachowaniemPoziomówSzarościToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeAfterPanel beforeAfterPanel = new BeforeAfterPanel((Bitmap)this.pctbImage.Image, BeforeAfterPanel.Operacje.ProgowanieZZachowaniem, this);
            beforeAfterPanel.Show();
        }

        private void rozciaganieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeAfterPanel beforeAfterPanel = new BeforeAfterPanel((Bitmap)this.pctbImage.Image, BeforeAfterPanel.Operacje.Rozciaganie, this);
            beforeAfterPanel.Show();
        }

        private void redukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeAfterPanel beforeAfterPanel = new BeforeAfterPanel((Bitmap)this.pctbImage.Image, BeforeAfterPanel.Operacje.Redukcja, this);
            beforeAfterPanel.Show();
        }

        private void uOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UOPPanel UOP = new UOPPanel((Bitmap)this.pctbImage.Image, this);
            UOP.Show();
        }

        private void dwuargumentoweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TwoArgsPanel twoArgOps = new TwoArgsPanel(MdiParent.MdiChildren);
            twoArgOps.Show();
        }

        private void filtracjaLiniowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperationsForm kernel = new KernelOpperationsForm((Bitmap)this.pctbImage.Image, this);
            kernel.Show();
        }

        private void operacjaMedianowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            MedianBlurPanel median = new MedianBlurPanel((Bitmap)this.pctbImage.Image, this);
            median.Show();
        }

        private void dwumaskoweWyostrzanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            TwoSharpeningMasks sharpeningMasks = new TwoSharpeningMasks((Bitmap)this.pctbImage.Image, this);
            sharpeningMasks.Show();
        }

        private void filtracjaLogicznaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            LogicalFiltrationForm logicalFiltration = new LogicalFiltrationForm((Bitmap)this.pctbImage.Image, this);
            logicalFiltration.Show();
        }

        private void Dialation4_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            SetImageAndHist(kernelOpperations.Dilatation(0, 1, 4));
        }

        private void Dilation8_Click(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            SetImageAndHist(kernelOpperations.Dilatation(0, 1, 8));
        }

        private void Erosion4(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            SetImageAndHist(kernelOpperations.Erosion(0, 1, 4));
        }

        private void Erosion8(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            SetImageAndHist(kernelOpperations.Erosion(0, 1, 8));
        }

        private void Closure4(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            this.pctbImage.Image = kernelOpperations.Erosion(0, 1, 4);

            kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);

            this.pctbImage.Image = kernelOpperations.Dilatation(0, 1, 4);
            SetImageAndHist((Bitmap)this.pctbImage.Image);
        }

        private void Closure8(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            this.pctbImage.Image = kernelOpperations.Erosion(0, 1, 8);

            kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);

            this.pctbImage.Image = kernelOpperations.Dilatation(0, 1, 8);
            SetImageAndHist((Bitmap)this.pctbImage.Image);
        }

        private void Opening4(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            this.pctbImage.Image = kernelOpperations.Dilatation(0, 1, 4);

            kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);

            this.pctbImage.Image = kernelOpperations.Erosion(0, 1, 4);
            SetImageAndHist((Bitmap)this.pctbImage.Image);
        }

        private void Opening8(object sender, EventArgs e)
        {
            basicImageOpertions.ToGreyScale();
            KernelOpperations kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);
            this.pctbImage.Image = kernelOpperations.Dilatation(0, 1, 8);

            kernelOpperations = new KernelOpperations((Bitmap)this.pctbImage.Image);

            this.pctbImage.Image = kernelOpperations.Erosion(0, 1, 8);
            SetImageAndHist((Bitmap)this.pctbImage.Image);
        }

        private void szkieletyzacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[][] t = KernelOpperations.Image2Bool(pctbImage.Image);
            t = KernelOpperations.ZhangSuenThinning(t);
            SetImageAndHist((Bitmap)KernelOpperations.Bool2Image(t));
        }

        private void kśrednichToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageSeg = new ImageSegmentation();

            Bitmap image = ImageSeg.Compute((Bitmap)this.pctbImage.Image);

            ImageSeg = null;

            SetImageAndHist(image);
        }

        private void rLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaRLE();

            SetImageAndHist(ic.bitmap);
        }

        private void rEADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaREAD();

            SetImageAndHist(ic.bitmap);
        }

        private void huffmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaHuffman();

            SetImageAndHist(ic.bitmap);
        }

        private void lZWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaLZW();

            SetImageAndHist(ic.bitmap);
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaBlokowa(4);

            SetImageAndHist(ic.bitmap);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaBlokowa(8);

            SetImageAndHist(ic.bitmap);        
        }

        private void x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaBlokowa(16);

            SetImageAndHist(ic.bitmap);
        }

        private void x32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaBlokowa(32);

            SetImageAndHist(ic.bitmap);
        }

        private void różnicowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageCompression ic = new ImageCompression((Bitmap)this.pctbImage.Image);

            ic.kompresjaRoznicowa();

            SetImageAndHist(ic.bitmap);
        }
    }
}
    

