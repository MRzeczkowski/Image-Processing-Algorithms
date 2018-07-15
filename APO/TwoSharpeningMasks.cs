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
    public partial class TwoSharpeningMasks : Form
    {
        private Bitmap orignal;
        private Bitmap output;

        private Image_Panel caller;

        private KernelOpperations kernel;

        private List<NumericUpDown> Matrices;

        int outlierMethod = 1;
        int type = 1;

        bool UniversalMask = true;

        int[,] maskHor;
        int[,] maskVer;
        int[,] twoOpMask;
        
        NumericUpDown[,] fiveByFive;

        public TwoSharpeningMasks(Bitmap inputImage, Image_Panel caller)
        {
            InitializeComponent();

            this.orignal = inputImage;
            this.output = (Bitmap)inputImage.Clone();
            this.caller = (Image_Panel)caller;

            this.pctbInput.Image = (Bitmap)orignal.Clone();
            this.pctbOutput.Image = output;

            Matrices = new List<NumericUpDown>(){
                                        nud1_1, nud2_1, nud3_1,
                                        nud4_1, nud5_1, nud6_1,
                                        nud7_1, nud8_1, nud9_1,

                                        nud1_2, nud2_2, nud3_2,
                                        nud4_2, nud5_2, nud6_2,
                                        nud7_2, nud8_2, nud9_2 };

            fiveByFive = new NumericUpDown[5,5] {               
                                        { nud1_3, nud2_3, nud3_3, nud4_3,nud5_3},
                                        { nud6_3, nud7_3, nud8_3,nud9_3,nud10_3},
                                        { nud11_3, nud12_3, nud13_3,nud14_3,nud15_3},
                                        { nud16_3, nud17_3, nud18_3,nud19_3,nud20_3},
                                        { nud21_3, nud22_3, nud23_3,nud24_3,nud25_3},
                                    };
            
        }

        private void PerformOpperation()
        {
            this.pctbInput.Image = orignal;
            kernel = new KernelOpperations((Bitmap)this.pctbInput.Image);
            //FillFiveByFiveMask();
            FillMatrix();
            

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

            //int divisor5x5 = 0;

            //int divisor3x3_1 = 0;
            //int divisor3x3_2 = 0;

            //for (int i = 0; i < 5; i++)
            //    for (int j = 0; j < 5; j++)
            //        divisor5x5 += twoOpMask[i, j];

            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        divisor3x3_1 += maskHor[i, j];
            //        divisor3x3_2 += maskVer[i, j];
            //    }
            //}

            if (chbTwoMasks.Checked == true)
            {
                output = kernel.TwoMaskFilter(maskHor, maskVer, type, outlierMethod);

                //Bitmap output1 = kernel.LinearFilter(maskHor, type, outlierMethod);
                //kernel = new KernelOpperations(output1);
                //output = kernel.LinearFilter(maskVer, type, outlierMethod);
            }
            else if (chbOneMask.Checked == true)
                output = kernel.LinearFilter(twoOpMask,  type, outlierMethod);
            
            

            pctbOutput.Image = output;
            panelOutput.Refresh();
            pctbOutput.Refresh();
        }

        private void rdbUniversal_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbUniversal.Checked)
            {
                foreach (NumericUpDown nud in Matrices)
                {
                    nud.Enabled = true;
                    nud.Visible = true;
                }
                UniversalMask = true;
            }
        }

        private void rdbRoberts_CheckedChanged(object sender, EventArgs e)
        {
            UniversalMask = false;
            foreach (NumericUpDown nud in Matrices)
                nud.Enabled = false;

            nud1_1.Value = 0;
            nud1_1.Visible = false;
            nud2_1.Value = 0;
            nud2_1.Visible = false;
            nud3_1.Value = 0;
            nud3_1.Visible = false;

            nud4_1.Value = 0;
            nud4_1.Visible = false;
            nud5_1.Value = 1;
            nud6_1.Value = 0;

            nud7_1.Value = 0;
            nud7_1.Visible = false;
            nud8_1.Value = 0;
            nud9_1.Value = -1;

            nud1_2.Value = 0;
            nud1_2.Visible = false;
            nud2_2.Value = 0;
            nud2_2.Visible = false;
            nud3_2.Value = 0;
            nud3_2.Visible = false;

            nud4_2.Value = 0;
            nud4_2.Visible = false;
            nud5_2.Value = 0;
            nud6_2.Value = -1;

            nud7_2.Value = 0;
            nud7_2.Visible = false;
            nud8_2.Value = 1;
            nud9_2.Value = 0;

            PerformOpperation();
        }

        private void rdbSobel_CheckedChanged(object sender, EventArgs e)
        {
            UniversalMask = false;
            foreach (NumericUpDown nud in Matrices)
            {
                nud.Enabled = false;
                nud.Visible = true;
            }

            nud1_1.Value = -1;
            nud2_1.Value = 0;
            nud3_1.Value = 1;

            nud4_1.Value = -2;
            nud5_1.Value = 0;
            nud6_1.Value = 2;

            nud7_1.Value = -1;
            nud8_1.Value = 0;
            nud9_1.Value = 1;

            nud1_2.Value = -1;
            nud2_2.Value = -2;
            nud3_2.Value = -1;

            nud4_2.Value = 0;
            nud5_2.Value = 0;
            nud6_2.Value = 0;

            nud7_2.Value = 1;
            nud8_2.Value = 2;
            nud9_2.Value = 1;

            PerformOpperation();
        }

        private void rdbPrewitt_CheckedChanged(object sender, EventArgs e)
        {
            UniversalMask = false;
            foreach (NumericUpDown nud in Matrices)
            {
                nud.Enabled = false;
                nud.Visible = true;
            }

            nud1_1.Value = -1;
            nud2_1.Value = 0;
            nud3_1.Value = 1;

            nud4_1.Value = -2;
            nud5_1.Value = 0;
            nud6_1.Value = 2;

            nud7_1.Value = -1;
            nud8_1.Value = 0;
            nud9_1.Value = 1;

            nud1_2.Value = -1;
            nud2_2.Value = -2;
            nud3_1.Value = -1;

            nud4_2.Value = 0;
            nud5_2.Value = 0;
            nud6_2.Value = 0;

            nud7_2.Value = 1;
            nud8_2.Value = 2;
            nud9_2.Value = 1;

            PerformOpperation();
        }

        private void FillMatrix()
        {
            maskHor = new int[3, 3] {
                                        {(int)nud1_1.Value, (int)nud2_1.Value, (int)nud3_1.Value},
                                        {(int)nud4_1.Value, (int)nud5_1.Value, (int)nud6_1.Value},
                                        {(int)nud7_1.Value, (int)nud8_1.Value, (int)nud9_1.Value}
                                       };

            maskVer = new int[3, 3] {
                                        {(int)nud1_2.Value, (int)nud2_2.Value, (int)nud3_2.Value},
                                        {(int)nud4_2.Value, (int)nud5_2.Value, (int)nud6_2.Value},
                                        {(int)nud7_2.Value, (int)nud8_2.Value, (int)nud9_2.Value}
                                       };

            FillFiveByFiveMask();

            twoOpMask = new int[5, 5] {
                                        {(int)nud1_3.Value, (int)nud2_3.Value, (int)nud3_3.Value,(int)nud4_3.Value,(int)nud5_3.Value},
                                        {(int)nud6_3.Value, (int)nud7_3.Value, (int)nud8_3.Value,(int)nud9_3.Value,(int)nud10_3.Value},
                                        {(int)nud11_3.Value, (int)nud12_3.Value, (int)nud13_3.Value,(int)nud14_3.Value,(int)nud15_3.Value},
                                        {(int)nud16_3.Value, (int)nud17_3.Value, (int)nud18_3.Value,(int)nud19_3.Value,(int)nud20_3.Value},
                                        {(int)nud21_3.Value, (int)nud22_3.Value, (int)nud23_3.Value,(int)nud24_3.Value,(int)nud25_3.Value},
                                    };
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

        private void chbIgnore_CheckedChanged(object sender, EventArgs e)
        {
            outlierMethod = 1;
            if (chbIgnore.Checked)
            {
                outlierMethod = 1;
                chbRehash.Checked = false;
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

        private void nud_ValueChanged(object sender, EventArgs e)
        {   
            if (UniversalMask)
                PerformOpperation();
        }

        private void FillFiveByFiveMask()
        {
            int[,] temp = new int[7, 7];
            for (int i = 2; i < 5; i++)
                for (int j = 2; j < 5; j++)
                    temp[i, j] = (int)maskVer[i - 2, j - 2];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int value = 0;
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            value += temp[i + k, j + l] * (int)maskHor[k, l];
                    fiveByFive[i, j].Value = value;
                }
            }
        }

        private void chbTwoMasks_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTwoMasks.Checked)
            {
                chbOneMask.Checked = false;                
            }
        }

        private void chbOneMask_CheckedChanged(object sender, EventArgs e)
        {
            if (chbOneMask.Checked)
            {
                chbTwoMasks.Checked = false;
            }
        }
    }
}
