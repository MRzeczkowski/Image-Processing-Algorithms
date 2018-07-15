namespace APO
{
    partial class LogicalFiltrationForm
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
            this.panelInput = new System.Windows.Forms.Panel();
            this.pctbInput = new System.Windows.Forms.PictureBox();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.pctbOutput = new System.Windows.Forms.PictureBox();
            this.cmbAngle = new System.Windows.Forms.ComboBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbAngle = new System.Windows.Forms.Label();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).BeginInit();
            this.panelOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.pctbInput);
            this.panelInput.Location = new System.Drawing.Point(12, 12);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(326, 291);
            this.panelInput.TabIndex = 0;
            // 
            // pctbInput
            // 
            this.pctbInput.Location = new System.Drawing.Point(0, 0);
            this.pctbInput.Name = "pctbInput";
            this.pctbInput.Size = new System.Drawing.Size(100, 50);
            this.pctbInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbInput.TabIndex = 0;
            this.pctbInput.TabStop = false;
            // 
            // panelOutput
            // 
            this.panelOutput.Controls.Add(this.pctbOutput);
            this.panelOutput.Location = new System.Drawing.Point(360, 12);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(326, 291);
            this.panelOutput.TabIndex = 1;
            // 
            // pctbOutput
            // 
            this.pctbOutput.Location = new System.Drawing.Point(0, 0);
            this.pctbOutput.Name = "pctbOutput";
            this.pctbOutput.Size = new System.Drawing.Size(100, 50);
            this.pctbOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctbOutput.TabIndex = 0;
            this.pctbOutput.TabStop = false;
            // 
            // cmbAngle
            // 
            this.cmbAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAngle.FormattingEnabled = true;
            this.cmbAngle.Items.AddRange(new object[] {
            "0",
            "45",
            "90",
            "135"});
            this.cmbAngle.Location = new System.Drawing.Point(290, 309);
            this.cmbAngle.Name = "cmbAngle";
            this.cmbAngle.Size = new System.Drawing.Size(121, 21);
            this.cmbAngle.TabIndex = 2;
            this.cmbAngle.SelectedIndexChanged += new System.EventHandler(this.cmbAngle_SelectedIndexChanged);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(530, 309);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Zatwiedz";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(611, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Anuluj";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbAngle
            // 
            this.lbAngle.AutoSize = true;
            this.lbAngle.Location = new System.Drawing.Point(261, 312);
            this.lbAngle.Name = "lbAngle";
            this.lbAngle.Size = new System.Drawing.Size(23, 13);
            this.lbAngle.TabIndex = 5;
            this.lbAngle.Text = "Kąt";
            // 
            // LogicalFiltrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 344);
            this.Controls.Add(this.lbAngle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.cmbAngle);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.panelInput);
            this.Name = "LogicalFiltrationForm";
            this.Text = "LogicalFiltrationForm";
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).EndInit();
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.PictureBox pctbInput;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.PictureBox pctbOutput;
        private System.Windows.Forms.ComboBox cmbAngle;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbAngle;
    }
}