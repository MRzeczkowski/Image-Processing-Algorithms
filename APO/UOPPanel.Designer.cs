namespace APO
{
    partial class UOPPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pctbImage = new System.Windows.Forms.PictureBox();
            this.chHisto = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pctbUOP = new System.Windows.Forms.PictureBox();
            this.panImage = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbX = new System.Windows.Forms.Label();
            this.lbY = new System.Windows.Forms.Label();
            this.lbXValue = new System.Windows.Forms.Label();
            this.lbYValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbUOP)).BeginInit();
            this.panImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctbImage
            // 
            this.pctbImage.Location = new System.Drawing.Point(17, 14);
            this.pctbImage.Name = "pctbImage";
            this.pctbImage.Size = new System.Drawing.Size(429, 254);
            this.pctbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbImage.TabIndex = 0;
            this.pctbImage.TabStop = false;
            // 
            // chHisto
            // 
            this.chHisto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chHisto.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chHisto.Legends.Add(legend1);
            this.chHisto.Location = new System.Drawing.Point(560, 12);
            this.chHisto.Name = "chHisto";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chHisto.Series.Add(series1);
            this.chHisto.Size = new System.Drawing.Size(559, 376);
            this.chHisto.TabIndex = 1;
            this.chHisto.Text = "chart1";
            // 
            // pctbUOP
            // 
            this.pctbUOP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pctbUOP.Location = new System.Drawing.Point(862, 394);
            this.pctbUOP.Name = "pctbUOP";
            this.pctbUOP.Size = new System.Drawing.Size(257, 257);
            this.pctbUOP.TabIndex = 2;
            this.pctbUOP.TabStop = false;
            this.pctbUOP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctbUOP_MouseDown);
            this.pctbUOP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctbUOP_MouseMove);
            // 
            // panImage
            // 
            this.panImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panImage.AutoScroll = true;
            this.panImage.Controls.Add(this.pctbImage);
            this.panImage.Location = new System.Drawing.Point(12, 12);
            this.panImage.Name = "panImage";
            this.panImage.Size = new System.Drawing.Size(542, 639);
            this.panImage.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(577, 628);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Zatwierdz";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(760, 628);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Anuluj";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbX
            // 
            this.lbX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(800, 410);
            this.lbX.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(17, 13);
            this.lbX.TabIndex = 6;
            this.lbX.Text = "X:";
            // 
            // lbY
            // 
            this.lbY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbY.AutoSize = true;
            this.lbY.Location = new System.Drawing.Point(800, 439);
            this.lbY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(17, 13);
            this.lbY.TabIndex = 7;
            this.lbY.Text = "Y:";
            // 
            // lbXValue
            // 
            this.lbXValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXValue.AutoSize = true;
            this.lbXValue.Location = new System.Drawing.Point(826, 410);
            this.lbXValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbXValue.Name = "lbXValue";
            this.lbXValue.Size = new System.Drawing.Size(10, 13);
            this.lbXValue.TabIndex = 8;
            this.lbXValue.Text = " ";
            // 
            // lbYValue
            // 
            this.lbYValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbYValue.AutoSize = true;
            this.lbYValue.Location = new System.Drawing.Point(826, 439);
            this.lbYValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbYValue.Name = "lbYValue";
            this.lbYValue.Size = new System.Drawing.Size(10, 13);
            this.lbYValue.TabIndex = 9;
            this.lbYValue.Text = " ";
            // 
            // UOPPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 661);
            this.Controls.Add(this.lbYValue);
            this.Controls.Add(this.lbXValue);
            this.Controls.Add(this.lbY);
            this.Controls.Add(this.lbX);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pctbUOP);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panImage);
            this.Controls.Add(this.chHisto);
            this.Name = "UOPPanel";
            this.Text = "UOPPanel";
            ((System.ComponentModel.ISupportInitialize)(this.pctbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbUOP)).EndInit();
            this.panImage.ResumeLayout(false);
            this.panImage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctbImage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chHisto;
        private System.Windows.Forms.PictureBox pctbUOP;
        private System.Windows.Forms.Panel panImage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.Label lbXValue;
        private System.Windows.Forms.Label lbYValue;
    }
}