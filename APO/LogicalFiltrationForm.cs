using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO
{
    public partial class LogicalFiltrationForm : Form
    {
        Bitmap orignalImage;
        Bitmap output;
        Image_Panel caller;

        KernelOpperations KernelOpperations;

        public LogicalFiltrationForm(Bitmap inputImage, Form caller)
        {
            InitializeComponent();

            this.orignalImage = (Bitmap)inputImage.Clone();
            this.output = (Bitmap)inputImage.Clone();

            this.caller = (Image_Panel)caller;

            this.pctbInput.Image = orignalImage;
            this.pctbOutput.Image = orignalImage;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            caller.SetImageAndHist(output);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbAngle_SelectedIndexChanged(object sender, EventArgs e)
        {
            KernelOpperations = new KernelOpperations((Bitmap)pctbInput.Image.Clone());

            int angle = Int32.Parse(cmbAngle.SelectedItem.ToString());

            output = KernelOpperations.LogicalFilter(angle);

            this.pctbOutput.Image = output;
        }
    }
}
