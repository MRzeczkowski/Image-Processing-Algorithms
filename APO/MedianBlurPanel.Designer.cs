namespace APO
{
    partial class MedianBlurPanel
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
            this.pctbInput = new System.Windows.Forms.PictureBox();
            this.pctbOutput = new System.Windows.Forms.PictureBox();
            this.panelInput = new System.Windows.Forms.Panel();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.nudNumOfRows = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfColumns = new System.Windows.Forms.NumericUpDown();
            this.lbX = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbOutlierMethod = new System.Windows.Forms.GroupBox();
            this.chbUseExisting = new System.Windows.Forms.CheckBox();
            this.chbRehash = new System.Windows.Forms.CheckBox();
            this.chbIgnore = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbOutput)).BeginInit();
            this.panelInput.SuspendLayout();
            this.panelOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfColumns)).BeginInit();
            this.grbOutlierMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctbInput
            // 
            this.pctbInput.Location = new System.Drawing.Point(3, 3);
            this.pctbInput.Name = "pctbInput";
            this.pctbInput.Size = new System.Drawing.Size(100, 50);
            this.pctbInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbInput.TabIndex = 0;
            this.pctbInput.TabStop = false;
            // 
            // pctbOutput
            // 
            this.pctbOutput.Location = new System.Drawing.Point(3, 3);
            this.pctbOutput.Name = "pctbOutput";
            this.pctbOutput.Size = new System.Drawing.Size(252, 233);
            this.pctbOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbOutput.TabIndex = 1;
            this.pctbOutput.TabStop = false;
            // 
            // panelInput
            // 
            this.panelInput.AutoScroll = true;
            this.panelInput.Controls.Add(this.pctbInput);
            this.panelInput.Location = new System.Drawing.Point(12, 12);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(258, 239);
            this.panelInput.TabIndex = 2;
            // 
            // panelOutput
            // 
            this.panelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOutput.AutoScroll = true;
            this.panelOutput.Controls.Add(this.pctbOutput);
            this.panelOutput.Location = new System.Drawing.Point(292, 12);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(258, 239);
            this.panelOutput.TabIndex = 3;
            // 
            // nudNumOfRows
            // 
            this.nudNumOfRows.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumOfRows.Location = new System.Drawing.Point(228, 281);
            this.nudNumOfRows.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudNumOfRows.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudNumOfRows.Name = "nudNumOfRows";
            this.nudNumOfRows.Size = new System.Drawing.Size(42, 20);
            this.nudNumOfRows.TabIndex = 4;
            this.nudNumOfRows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudNumOfRows.ValueChanged += new System.EventHandler(this.nubSize_ValueChanged);
            // 
            // nudNumOfColumns
            // 
            this.nudNumOfColumns.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumOfColumns.Location = new System.Drawing.Point(292, 281);
            this.nudNumOfColumns.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudNumOfColumns.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudNumOfColumns.Name = "nudNumOfColumns";
            this.nudNumOfColumns.Size = new System.Drawing.Size(42, 20);
            this.nudNumOfColumns.TabIndex = 5;
            this.nudNumOfColumns.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudNumOfColumns.ValueChanged += new System.EventHandler(this.nubSize_ValueChanged);
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(276, 283);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(14, 13);
            this.lbX.TabIndex = 6;
            this.lbX.Text = "X";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(394, 281);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 7;
            this.btnAccept.Text = "Zatwierdz";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(475, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Anuluj";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grbOutlierMethod
            // 
            this.grbOutlierMethod.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grbOutlierMethod.Controls.Add(this.chbUseExisting);
            this.grbOutlierMethod.Controls.Add(this.chbRehash);
            this.grbOutlierMethod.Controls.Add(this.chbIgnore);
            this.grbOutlierMethod.Location = new System.Drawing.Point(12, 257);
            this.grbOutlierMethod.Name = "grbOutlierMethod";
            this.grbOutlierMethod.Size = new System.Drawing.Size(163, 90);
            this.grbOutlierMethod.TabIndex = 19;
            this.grbOutlierMethod.TabStop = false;
            this.grbOutlierMethod.Text = "Metoda na brzegowych";
            // 
            // chbUseExisting
            // 
            this.chbUseExisting.AutoSize = true;
            this.chbUseExisting.Location = new System.Drawing.Point(6, 65);
            this.chbUseExisting.Name = "chbUseExisting";
            this.chbUseExisting.Size = new System.Drawing.Size(151, 17);
            this.chbUseExisting.TabIndex = 2;
            this.chbUseExisting.Text = "Uzyj istniejacych sasiadow";
            this.chbUseExisting.UseVisualStyleBackColor = true;
            this.chbUseExisting.CheckedChanged += new System.EventHandler(this.chbUseExisting_CheckedChanged);
            // 
            // chbRehash
            // 
            this.chbRehash.AutoSize = true;
            this.chbRehash.Location = new System.Drawing.Point(6, 42);
            this.chbRehash.Name = "chbRehash";
            this.chbRehash.Size = new System.Drawing.Size(57, 17);
            this.chbRehash.TabIndex = 1;
            this.chbRehash.Text = "Powiel";
            this.chbRehash.UseVisualStyleBackColor = true;
            this.chbRehash.CheckedChanged += new System.EventHandler(this.chbRehash_CheckedChanged);
            // 
            // chbIgnore
            // 
            this.chbIgnore.AutoSize = true;
            this.chbIgnore.Location = new System.Drawing.Point(6, 19);
            this.chbIgnore.Name = "chbIgnore";
            this.chbIgnore.Size = new System.Drawing.Size(58, 17);
            this.chbIgnore.TabIndex = 0;
            this.chbIgnore.Text = "Ignoruj";
            this.chbIgnore.UseVisualStyleBackColor = true;
            this.chbIgnore.CheckedChanged += new System.EventHandler(this.chbIgnore_CheckedChanged);
            // 
            // MedianBlurPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 358);
            this.Controls.Add(this.grbOutlierMethod);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbX);
            this.Controls.Add(this.nudNumOfColumns);
            this.Controls.Add(this.nudNumOfRows);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.panelInput);
            this.Name = "MedianBlurPanel";
            this.Text = "MedianBlur";
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbOutput)).EndInit();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfColumns)).EndInit();
            this.grbOutlierMethod.ResumeLayout(false);
            this.grbOutlierMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctbInput;
        private System.Windows.Forms.PictureBox pctbOutput;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.NumericUpDown nudNumOfRows;
        private System.Windows.Forms.NumericUpDown nudNumOfColumns;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grbOutlierMethod;
        private System.Windows.Forms.CheckBox chbUseExisting;
        private System.Windows.Forms.CheckBox chbRehash;
        private System.Windows.Forms.CheckBox chbIgnore;
    }
}