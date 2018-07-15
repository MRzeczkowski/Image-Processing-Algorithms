namespace APO
{
    partial class BeforeAfterPanel
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
            this.pctbEffect = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbArgument = new System.Windows.Forms.TrackBar();
            this.lbValue = new System.Windows.Forms.Label();
            this.tbSecondArgument = new System.Windows.Forms.TrackBar();
            this.lbValue2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbEffect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbArgument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSecondArgument)).BeginInit();
            this.SuspendLayout();
            // 
            // pctbInput
            // 
            this.pctbInput.Location = new System.Drawing.Point(12, 7);
            this.pctbInput.Name = "pctbInput";
            this.pctbInput.Size = new System.Drawing.Size(361, 238);
            this.pctbInput.TabIndex = 0;
            this.pctbInput.TabStop = false;
            // 
            // pctbEffect
            // 
            this.pctbEffect.Location = new System.Drawing.Point(399, 7);
            this.pctbEffect.Name = "pctbEffect";
            this.pctbEffect.Size = new System.Drawing.Size(361, 238);
            this.pctbEffect.TabIndex = 1;
            this.pctbEffect.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(298, 337);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(399, 337);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbArgument
            // 
            this.tbArgument.Location = new System.Drawing.Point(216, 251);
            this.tbArgument.Maximum = 255;
            this.tbArgument.Name = "tbArgument";
            this.tbArgument.Size = new System.Drawing.Size(335, 45);
            this.tbArgument.TabIndex = 4;
            this.tbArgument.Value = 127;
            this.tbArgument.ValueChanged += new System.EventHandler(this.tbArgument_ValueChanged);
            this.tbArgument.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbArgument_MouseUp);
            // 
            // lbValue
            // 
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(557, 263);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(35, 13);
            this.lbValue.TabIndex = 5;
            this.lbValue.Text = "label1";
            // 
            // tbSecondArgument
            // 
            this.tbSecondArgument.Enabled = false;
            this.tbSecondArgument.Location = new System.Drawing.Point(216, 286);
            this.tbSecondArgument.Maximum = 255;
            this.tbSecondArgument.Name = "tbSecondArgument";
            this.tbSecondArgument.Size = new System.Drawing.Size(335, 45);
            this.tbSecondArgument.TabIndex = 6;
            this.tbSecondArgument.Value = 255;
            this.tbSecondArgument.Visible = false;
            this.tbSecondArgument.ValueChanged += new System.EventHandler(this.tbSecondArgument_ValueChanged);
            this.tbSecondArgument.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbSecondArgument_MouseUp);
            // 
            // lbValue2
            // 
            this.lbValue2.AutoSize = true;
            this.lbValue2.Location = new System.Drawing.Point(557, 295);
            this.lbValue2.Name = "lbValue2";
            this.lbValue2.Size = new System.Drawing.Size(35, 13);
            this.lbValue2.TabIndex = 7;
            this.lbValue2.Text = "label1";
            this.lbValue2.Visible = false;
            // 
            // BeforeAfterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 362);
            this.Controls.Add(this.lbValue2);
            this.Controls.Add(this.tbSecondArgument);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.tbArgument);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pctbEffect);
            this.Controls.Add(this.pctbInput);
            this.Name = "BeforeAfterPanel";
            this.Text = "Before & After";
            ((System.ComponentModel.ISupportInitialize)(this.pctbInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbEffect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbArgument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSecondArgument)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctbInput;
        private System.Windows.Forms.PictureBox pctbEffect;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TrackBar tbArgument;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.TrackBar tbSecondArgument;
        private System.Windows.Forms.Label lbValue2;
    }
}