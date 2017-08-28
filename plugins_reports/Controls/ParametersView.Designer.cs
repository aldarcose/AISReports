namespace Reports.Controls
{
    partial class ParametersView
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
            this.verticalLayout = new VerticalLayoutPanel();
            this.SuspendLayout();
            // 
            // verticalPanel
            // 
            this.verticalLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalLayout.Location = new System.Drawing.Point(0, 0);
            this.verticalLayout.Name = "verticalPanel";
            this.verticalLayout.Size = new System.Drawing.Size(223, 69);
            this.verticalLayout.TabIndex = 0;
            // 
            // ParametersEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.verticalLayout);
            this.Name = "ParametersEdit";
            this.Size = new System.Drawing.Size(223, 69);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel verticalLayout;
    }
}
