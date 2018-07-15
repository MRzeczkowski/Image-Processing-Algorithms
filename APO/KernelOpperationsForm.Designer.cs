namespace APO
{
    partial class KernelOpperationsForm
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
            this.nud1 = new System.Windows.Forms.NumericUpDown();
            this.nud8 = new System.Windows.Forms.NumericUpDown();
            this.nud5 = new System.Windows.Forms.NumericUpDown();
            this.nud2 = new System.Windows.Forms.NumericUpDown();
            this.nud3 = new System.Windows.Forms.NumericUpDown();
            this.nud6 = new System.Windows.Forms.NumericUpDown();
            this.nud9 = new System.Windows.Forms.NumericUpDown();
            this.nud7 = new System.Windows.Forms.NumericUpDown();
            this.nud4 = new System.Windows.Forms.NumericUpDown();
            this.nudDivisor = new System.Windows.Forms.NumericUpDown();
            this.lbMask = new System.Windows.Forms.Label();
            this.lbDivisor = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelInput = new System.Windows.Forms.Panel();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.grbOutlierMethod = new System.Windows.Forms.GroupBox();
            this.chbUseExisting = new System.Windows.Forms.CheckBox();
            this.chbRehash = new System.Windows.Forms.CheckBox();
            this.chbIgnore = new System.Windows.Forms.CheckBox();
            this.grbScalingMethods = new System.Windows.Forms.GroupBox();
            this.chbCutDown = new System.Windows.Forms.CheckBox();
            this.chbTriValue = new System.Windows.Forms.CheckBox();
            this.chbProportional = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivisor)).BeginInit();
            this.panelInput.SuspendLayout();
            this.panelOutput.SuspendLayout();
            this.grbOutlierMethod.SuspendLayout();
            this.grbScalingMethods.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctbInput
            // 
            this.pctbInput.Location = new System.Drawing.Point(19, 0);
            this.pctbInput.Name = "pctbInput";
            this.pctbInput.Size = new System.Drawing.Size(291, 278);
            this.pctbInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbInput.TabIndex = 0;
            this.pctbInput.TabStop = false;
            // 
            // pctbOutput
            // 
            this.pctbOutput.Location = new System.Drawing.Point(29, 0);
            this.pctbOutput.Name = "pctbOutput";
            this.pctbOutput.Size = new System.Drawing.Size(291, 278);
            this.pctbOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbOutput.TabIndex = 1;
            this.pctbOutput.TabStop = false;
            // 
            // nud1
            // 
            this.nud1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud1.Location = new System.Drawing.Point(35, 334);
            this.nud1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud1.Name = "nud1";
            this.nud1.Size = new System.Drawing.Size(41, 20);
            this.nud1.TabIndex = 2;
            this.nud1.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud8
            // 
            this.nud8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud8.Location = new System.Drawing.Point(82, 386);
            this.nud8.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud8.Name = "nud8";
            this.nud8.Size = new System.Drawing.Size(41, 20);
            this.nud8.TabIndex = 3;
            this.nud8.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud5
            // 
            this.nud5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud5.Location = new System.Drawing.Point(82, 360);
            this.nud5.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud5.Name = "nud5";
            this.nud5.Size = new System.Drawing.Size(41, 20);
            this.nud5.TabIndex = 4;
            this.nud5.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud2
            // 
            this.nud2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud2.Location = new System.Drawing.Point(82, 334);
            this.nud2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud2.Name = "nud2";
            this.nud2.Size = new System.Drawing.Size(41, 20);
            this.nud2.TabIndex = 5;
            this.nud2.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud3
            // 
            this.nud3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud3.Location = new System.Drawing.Point(129, 334);
            this.nud3.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud3.Name = "nud3";
            this.nud3.Size = new System.Drawing.Size(41, 20);
            this.nud3.TabIndex = 6;
            this.nud3.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud6
            // 
            this.nud6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud6.Location = new System.Drawing.Point(129, 360);
            this.nud6.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud6.Name = "nud6";
            this.nud6.Size = new System.Drawing.Size(41, 20);
            this.nud6.TabIndex = 7;
            this.nud6.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud9
            // 
            this.nud9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud9.Location = new System.Drawing.Point(129, 386);
            this.nud9.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud9.Name = "nud9";
            this.nud9.Size = new System.Drawing.Size(41, 20);
            this.nud9.TabIndex = 8;
            this.nud9.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud7
            // 
            this.nud7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud7.Location = new System.Drawing.Point(35, 386);
            this.nud7.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud7.Name = "nud7";
            this.nud7.Size = new System.Drawing.Size(41, 20);
            this.nud7.TabIndex = 9;
            this.nud7.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nud4
            // 
            this.nud4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud4.Location = new System.Drawing.Point(35, 360);
            this.nud4.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nud4.Name = "nud4";
            this.nud4.Size = new System.Drawing.Size(41, 20);
            this.nud4.TabIndex = 10;
            this.nud4.ValueChanged += new System.EventHandler(this.Kernel_ValueChanged);
            // 
            // nudDivisor
            // 
            this.nudDivisor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudDivisor.Enabled = false;
            this.nudDivisor.Location = new System.Drawing.Point(198, 352);
            this.nudDivisor.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.nudDivisor.Minimum = new decimal(new int[] {
            900,
            0,
            0,
            -2147483648});
            this.nudDivisor.Name = "nudDivisor";
            this.nudDivisor.ReadOnly = true;
            this.nudDivisor.Size = new System.Drawing.Size(41, 20);
            this.nudDivisor.TabIndex = 11;
            // 
            // lbMask
            // 
            this.lbMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbMask.AutoSize = true;
            this.lbMask.Location = new System.Drawing.Point(91, 308);
            this.lbMask.Name = "lbMask";
            this.lbMask.Size = new System.Drawing.Size(39, 13);
            this.lbMask.TabIndex = 12;
            this.lbMask.Text = "Maska";
            // 
            // lbDivisor
            // 
            this.lbDivisor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDivisor.AutoSize = true;
            this.lbDivisor.Location = new System.Drawing.Point(195, 336);
            this.lbDivisor.Name = "lbDivisor";
            this.lbDivisor.Size = new System.Drawing.Size(44, 13);
            this.lbDivisor.TabIndex = 13;
            this.lbDivisor.Text = "Dzielnik";
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.Location = new System.Drawing.Point(29, 412);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 14;
            this.btnAccept.Text = "Zatwierdz";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(110, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Anuluj";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelInput
            // 
            this.panelInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelInput.AutoScroll = true;
            this.panelInput.Controls.Add(this.pctbInput);
            this.panelInput.Location = new System.Drawing.Point(10, 12);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(348, 278);
            this.panelInput.TabIndex = 16;
            // 
            // panelOutput
            // 
            this.panelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOutput.AutoScroll = true;
            this.panelOutput.Controls.Add(this.pctbOutput);
            this.panelOutput.Location = new System.Drawing.Point(361, 12);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(348, 278);
            this.panelOutput.TabIndex = 17;
            // 
            // grbOutlierMethod
            // 
            this.grbOutlierMethod.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grbOutlierMethod.Controls.Add(this.chbUseExisting);
            this.grbOutlierMethod.Controls.Add(this.chbRehash);
            this.grbOutlierMethod.Controls.Add(this.chbIgnore);
            this.grbOutlierMethod.Location = new System.Drawing.Point(355, 308);
            this.grbOutlierMethod.Name = "grbOutlierMethod";
            this.grbOutlierMethod.Size = new System.Drawing.Size(163, 90);
            this.grbOutlierMethod.TabIndex = 18;
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
            // grbScalingMethods
            // 
            this.grbScalingMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grbScalingMethods.Controls.Add(this.chbCutDown);
            this.grbScalingMethods.Controls.Add(this.chbTriValue);
            this.grbScalingMethods.Controls.Add(this.chbProportional);
            this.grbScalingMethods.Location = new System.Drawing.Point(524, 308);
            this.grbScalingMethods.Name = "grbScalingMethods";
            this.grbScalingMethods.Size = new System.Drawing.Size(157, 90);
            this.grbScalingMethods.TabIndex = 19;
            this.grbScalingMethods.TabStop = false;
            this.grbScalingMethods.Text = "Skalowanie";
            // 
            // chbCutDown
            // 
            this.chbCutDown.AutoSize = true;
            this.chbCutDown.Location = new System.Drawing.Point(6, 67);
            this.chbCutDown.Name = "chbCutDown";
            this.chbCutDown.Size = new System.Drawing.Size(68, 17);
            this.chbCutDown.TabIndex = 2;
            this.chbCutDown.Text = "Obciecie";
            this.chbCutDown.UseVisualStyleBackColor = true;
            this.chbCutDown.CheckedChanged += new System.EventHandler(this.chbCutDown_CheckedChanged);
            // 
            // chbTriValue
            // 
            this.chbTriValue.AutoSize = true;
            this.chbTriValue.Location = new System.Drawing.Point(6, 42);
            this.chbTriValue.Name = "chbTriValue";
            this.chbTriValue.Size = new System.Drawing.Size(102, 17);
            this.chbTriValue.TabIndex = 1;
            this.chbTriValue.Text = "Trojwartosciowy";
            this.chbTriValue.UseVisualStyleBackColor = true;
            this.chbTriValue.CheckedChanged += new System.EventHandler(this.chbTriValue_CheckedChanged);
            // 
            // chbProportional
            // 
            this.chbProportional.AutoSize = true;
            this.chbProportional.Location = new System.Drawing.Point(6, 19);
            this.chbProportional.Name = "chbProportional";
            this.chbProportional.Size = new System.Drawing.Size(97, 17);
            this.chbProportional.TabIndex = 0;
            this.chbProportional.Text = "Proporcjonalne";
            this.chbProportional.UseVisualStyleBackColor = true;
            this.chbProportional.CheckedChanged += new System.EventHandler(this.chbProportional_CheckedChanged);
            // 
            // KernelOpperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 445);
            this.Controls.Add(this.grbScalingMethods);
            this.Controls.Add(this.nud2);
            this.Controls.Add(this.grbOutlierMethod);
            this.Controls.Add(this.nud8);
            this.Controls.Add(this.nud5);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbDivisor);
            this.Controls.Add(this.lbMask);
            this.Controls.Add(this.nudDivisor);
            this.Controls.Add(this.nud4);
            this.Controls.Add(this.nud7);
            this.Controls.Add(this.nud9);
            this.Controls.Add(this.nud6);
            this.Controls.Add(this.nud3);
            this.Controls.Add(this.nud1);
            this.Name = "KernelOpperationsForm";
            this.Text = "KernelOpperations";
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivisor)).EndInit();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            this.grbOutlierMethod.ResumeLayout(false);
            this.grbOutlierMethod.PerformLayout();
            this.grbScalingMethods.ResumeLayout(false);
            this.grbScalingMethods.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctbInput;
        private System.Windows.Forms.PictureBox pctbOutput;
        private System.Windows.Forms.NumericUpDown nud1;
        private System.Windows.Forms.NumericUpDown nud8;
        private System.Windows.Forms.NumericUpDown nud5;
        private System.Windows.Forms.NumericUpDown nud2;
        private System.Windows.Forms.NumericUpDown nud3;
        private System.Windows.Forms.NumericUpDown nud6;
        private System.Windows.Forms.NumericUpDown nud9;
        private System.Windows.Forms.NumericUpDown nud7;
        private System.Windows.Forms.NumericUpDown nud4;
        private System.Windows.Forms.NumericUpDown nudDivisor;
        private System.Windows.Forms.Label lbMask;
        private System.Windows.Forms.Label lbDivisor;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.GroupBox grbOutlierMethod;
        private System.Windows.Forms.CheckBox chbUseExisting;
        private System.Windows.Forms.CheckBox chbRehash;
        private System.Windows.Forms.CheckBox chbIgnore;
        private System.Windows.Forms.GroupBox grbScalingMethods;
        private System.Windows.Forms.CheckBox chbCutDown;
        private System.Windows.Forms.CheckBox chbTriValue;
        private System.Windows.Forms.CheckBox chbProportional;
    }
}