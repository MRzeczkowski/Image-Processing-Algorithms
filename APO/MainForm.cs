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
    public partial class APO : Form
    {
        private FileManipulation FileM = new FileManipulation();

        string fileName;

        private Bitmap bmp;

        public APO()
        {
            InitializeComponent();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bmp = FileM.OpenFile();
            fileName = FileM.fileName;

            if (bmp != null)
            {

                Image_Panel Image_panel = new Image_Panel(bmp, fileName);
                Image_panel.MdiParent = this;
                //Image_panel.SetImage(bmp);
                Image_panel.Show();
            }
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "APO Projekt"
                    + Environment.NewLine
                    + "Autor: Mateusz Rzeczkowski"
                    + Environment.NewLine
                    + "Grupa: ID06IO1"
                    + Environment.NewLine
                    + "Prowadzący: dr inż. Marek Doros";

            MessageBox.Show(text, "O programie");
        }
    }
}
