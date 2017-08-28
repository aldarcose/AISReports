namespace Reports.Controls
{
    partial class FloatPeriodEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.floatInEdit = new System.Windows.Forms.NumericUpDown();
            this.floatOutEdit = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.floatInEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatOutEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "c";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "по";
            // 
            // floatInEdit
            // 
            this.floatInEdit.DecimalPlaces = 2;
            this.floatInEdit.Location = new System.Drawing.Point(24, 4);
            this.floatInEdit.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.floatInEdit.Name = "floatInEdit";
            this.floatInEdit.Size = new System.Drawing.Size(124, 22);
            this.floatInEdit.TabIndex = 2;
            this.floatInEdit.ThousandsSeparator = true;
            // 
            // floatOutEdit
            // 
            this.floatOutEdit.DecimalPlaces = 2;
            this.floatOutEdit.Location = new System.Drawing.Point(184, 4);
            this.floatOutEdit.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.floatOutEdit.Name = "floatOutEdit";
            this.floatOutEdit.Size = new System.Drawing.Size(121, 22);
            this.floatOutEdit.TabIndex = 3;
            this.floatOutEdit.ThousandsSeparator = true;
            // 
            // FloatPeriodEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.floatOutEdit);
            this.Controls.Add(this.floatInEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FloatPeriodEdit";
            this.Size = new System.Drawing.Size(313, 33);
            ((System.ComponentModel.ISupportInitialize)(this.floatInEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatOutEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown floatInEdit;
        private System.Windows.Forms.NumericUpDown floatOutEdit;
    }
}
