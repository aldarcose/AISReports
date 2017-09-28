namespace Reports.Controls
{
    partial class DiagnosisEdit
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

            conn.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbCodeIn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCodeOut = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbCodeIn
            // 
            this.cbCodeIn.FormattingEnabled = true;
            this.cbCodeIn.Location = new System.Drawing.Point(23, 5);
            this.cbCodeIn.Name = "cbCodeIn";
            this.cbCodeIn.Size = new System.Drawing.Size(121, 24);
            this.cbCodeIn.TabIndex = 0;
            this.cbCodeIn.TextChanged += new System.EventHandler(this.cbCodeIn_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "с";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "по";
            // 
            // cbCodeOut
            // 
            this.cbCodeOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCodeOut.FormattingEnabled = true;
            this.cbCodeOut.Location = new System.Drawing.Point(190, 6);
            this.cbCodeOut.Name = "cbCodeOut";
            this.cbCodeOut.Size = new System.Drawing.Size(121, 24);
            this.cbCodeOut.TabIndex = 3;
            this.cbCodeOut.TextChanged += new System.EventHandler(this.cbCodeOut_TextChanged);
            // 
            // DiagnosisEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbCodeOut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCodeIn);
            this.Name = "DiagnosisEdit";
            this.Size = new System.Drawing.Size(320, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCodeIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCodeOut;
    }
}
