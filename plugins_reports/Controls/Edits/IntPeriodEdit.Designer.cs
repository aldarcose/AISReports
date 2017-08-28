namespace Reports.Controls
{
    partial class IntPeriodEdit
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
            this.intInEdit = new System.Windows.Forms.NumericUpDown();
            this.intOutEdit = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.intInEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intOutEdit)).BeginInit();
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
            this.label2.Location = new System.Drawing.Point(111, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "по";
            // 
            // intInEdit
            // 
            this.intInEdit.Location = new System.Drawing.Point(24, 4);
            this.intInEdit.Name = "intInEdit";
            this.intInEdit.Size = new System.Drawing.Size(81, 22);
            this.intInEdit.TabIndex = 2;
            // 
            // intOutEdit
            // 
            this.intOutEdit.Location = new System.Drawing.Point(141, 5);
            this.intOutEdit.Name = "intOutEdit";
            this.intOutEdit.Size = new System.Drawing.Size(81, 22);
            this.intOutEdit.TabIndex = 3;
            // 
            // IntPeriodEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.intOutEdit);
            this.Controls.Add(this.intInEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "IntPeriodEdit";
            this.Size = new System.Drawing.Size(231, 33);
            ((System.ComponentModel.ISupportInitialize)(this.intInEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intOutEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown intInEdit;
        private System.Windows.Forms.NumericUpDown intOutEdit;
    }
}
