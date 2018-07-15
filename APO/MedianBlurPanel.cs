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
    public partial class MedianBlurPanel : Form
    {
        private Bitmap orignal;
        private Bitmap output;

        private Image_Panel caller;

        private KernelOpperations kernel;

        private int outlierMethod;


        public MedianBlurPanel(Bitmap inputImage, Form caller)
        {
            InitializeComponent();

            this.orignal = inputImage;
            this.output = (Bitmap)inputImage.Clone();
            this.caller = (Image_Panel)caller;

            this.pctbInput.Image = (Bitmap)orignal.Clone();
            this.pctbOutput.Image = output;

            outlierMethod = 1;
            chbIgnore.Checked = true;

            PerformBlur();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PerformBlur()
        {
            kernel = new KernelOpperations((Bitmap)this.pctbInput.Image);
            
            if (chbIgnore.Checked == true)
            {
                outlierMethod = 1;
            }

            if (chbRehash.Checked == true)
            {
                outlierMethod = 0;
            }

            if (chbUseExisting.Checked == true)
            {
                outlierMethod = 0;
            }

            output = kernel.MedianFilter((int)this.nudNumOfRows.Value, (int)this.nudNumOfColumns.Value, outlierMethod);

            pctbOutput.Image = output;
            panelOutput.Refresh();
            pctbOutput.Refresh();
        }

        private void nubSize_ValueChanged(object sender, EventArgs e)
        {
            PerformBlur();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            caller.SetImage(output);
            this.Close();
        }

        private void chbIgnore_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 1;
            if (chbIgnore.Checked)
            {
                chbRehash.Checked = false;
                chbUseExisting.Checked = false;
            }
        }

        private void chbRehash_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 0;
            if (chbRehash.Checked)
            {
                chbIgnore.Checked = false;
                chbUseExisting.Checked = false;
            }
        }

        private void chbUseExisting_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 0;
            if (chbUseExisting.Checked)
            {
                chbIgnore.Checked = false;
                chbRehash.Checked = false;
            }
        }
    }
}
