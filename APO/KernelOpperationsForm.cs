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
    public partial class KernelOpperationsForm : Form
    {
        private Bitmap orignal;
        private Bitmap output;

        private Image_Panel caller;

        private KernelOpperations kernel;

        private int divisor;
        private int type;
        private int outlierMethod;

        public KernelOpperationsForm(Bitmap inputImage, Form caller)
        {
            InitializeComponent();

            this.orignal = inputImage;
            this.output = (Bitmap)inputImage.Clone();
            this.caller = (Image_Panel)caller;
            
            this.pctbInput.Image = (Bitmap)orignal.Clone();
            this.pctbOutput.Image = output;

            type = 1;
            this.chbProportional.Checked = true;


            outlierMethod = 1;
            this.chbIgnore.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void PerformOpperation()
        {
            kernel = new KernelOpperations(orignal);

            int[,] mask = new int[3, 3] {
                                        {(int)nud1.Value, (int)nud2.Value, (int)nud3.Value},
                                        {(int)nud4.Value, (int)nud5.Value, (int)nud6.Value},
                                        {(int)nud7.Value, (int)nud8.Value, (int)nud9.Value}
                                       };

            divisor = 0;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    divisor += mask[i, j];

            nudDivisor.Value = divisor;

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

            if (chbProportional.Checked == true)
            {
                type = 1;
            }

            if (chbTriValue.Checked == true)
            {
                type = 2;
            }

            if (chbCutDown.Checked == true)
            {
                type = 3;
            }

            output = kernel.LinearFilter(mask, type, outlierMethod);
            
            pctbOutput.Image = output;
            panelOutput.Refresh();
            pctbOutput.Refresh();
        }

        private void Kernel_ValueChanged(object sender, EventArgs e)
        {
            PerformOpperation();
        }

        private void chbIgnore_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 1;
            if (chbIgnore.Checked)
            {
                outlierMethod = 1;
                chbRehash.Checked= false;
                chbUseExisting.Checked = false;
            }
        }

        private void chbRehash_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 0;
            if (chbRehash.Checked)
            {
                outlierMethod = 0;
                chbIgnore.Checked = false;
                chbUseExisting.Checked = false;
            }
        }

        private void chbUseExisting_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 0;
            if (chbUseExisting.Checked)
            {
                outlierMethod = 0;
                chbIgnore.Checked = false;
                chbRehash.Checked = false;
            }
        }

        private void chbProportional_CheckedChanged(object sender, EventArgs e)
        {
            type = 1;
            if (chbProportional.Checked)
            {
                chbTriValue.Checked = false;
                chbCutDown.Checked = false;
            }
        }

        private void chbTriValue_CheckedChanged(object sender, EventArgs e)
        {
            type = 2;
            if (chbTriValue.Checked)
            {
                chbProportional.Checked = false;
                chbCutDown.Checked = false;
            }
        }

        private void chbCutDown_CheckedChanged(object sender, EventArgs e)
        {
            type = 3;
            if (chbCutDown.Checked)
            {
                chbProportional.Checked = false;
                chbTriValue.Checked = false;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.caller.SetImageAndHist(output);
            this.Close();
        }        
    }
}
