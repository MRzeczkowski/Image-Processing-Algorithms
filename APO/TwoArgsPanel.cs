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
    public partial class TwoArgsPanel : Form
    {
        Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        
        BasicImageOpertions operationOnImage;

        Form[] forms;

        public TwoArgsPanel(Form[] forms)
        {
            InitializeComponent();

            this.forms = forms;

            foreach (Form form in forms)
            {
                Image_Panel imageForm = (Image_Panel)form;
                if (!bitmaps.ContainsKey(imageForm.imageName))
                {
                    bitmaps.Add(imageForm.imageName, imageForm.originalImage);
                    cmbFirstImage.Items.Add(imageForm.imageName);
                    cmbSecondImage.Items.Add(imageForm.imageName);
                }
            }
            cmbFirstImage.Sorted = true;
            cmbSecondImage.Sorted = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbFirstImage.SelectedItem != null && cmbSecondImage.SelectedItem != null)
            {
                Bitmap bmp1 = bitmaps[(string)cmbFirstImage.SelectedItem];
                Bitmap bmp2 = new Bitmap((Bitmap)bitmaps[(string)cmbSecondImage.SelectedItem], bmp1.Width, bmp1.Height);//(Bitmap)bitmaps[(string)cmbSecondImage.SelectedItem].Clone();
                //resultBmp = (Bitmap)bmp1.Clone();


                operationOnImage = new BasicImageOpertions(bmp2);
                operationOnImage.ToGreyScale();

                operationOnImage = new BasicImageOpertions(bmp1);

                if (rdbAdd.Checked) operationOnImage.ADD(bmp2);
                else if (rdbSub.Checked) operationOnImage.SUB(bmp2);
                else if (rdbDiff.Checked) operationOnImage.DIFF(bmp2);
                else if (rdbOR.Checked) operationOnImage.OR(bmp2);
                else if (rdbAND.Checked) operationOnImage.AND(bmp2);
                else if (rdbXOR.Checked) operationOnImage.XOR(bmp2);
                //else if (radioButtonAnd.Checked) And(bmp1, bmp2);
                //else if (radioButtonOr.Checked) Or(bmp1, bmp2);
                //else if (radioButtonXor.Checked) Xor(bmp1, bmp2);
            }
            foreach (Image_Panel form in forms)
            {
                form.Refresh();
                form.RemakeHistogram();
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
