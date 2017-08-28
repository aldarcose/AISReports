namespace ARMPlugin.FormsAndControls
{
    partial class AddNewTalonForm
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
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ok = new DevExpress.XtraEditors.SimpleButton();
            this.cmb_target = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmb_payment_type = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.de_visitdate = new DevExpress.XtraEditors.DateEdit();
            this.chk_at_home = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_target.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_payment_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_visitdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_visitdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_at_home.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(276, 88);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Отмена";
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.Location = new System.Drawing.Point(195, 88);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "Ок";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // cmb_target
            // 
            this.cmb_target.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_target.Location = new System.Drawing.Point(103, 35);
            this.cmb_target.Name = "cmb_target";
            this.cmb_target.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_target.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_target.Size = new System.Drawing.Size(248, 20);
            this.cmb_target.TabIndex = 3;
            this.cmb_target.Visible = false;
            this.cmb_target.SelectedValueChanged += new System.EventHandler(this.cmb_target_SelectedValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Вид оплаты";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 38);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(85, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Цель посещения";
            this.labelControl3.Visible = false;
            // 
            // cmb_payment_type
            // 
            this.cmb_payment_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_payment_type.Location = new System.Drawing.Point(103, 9);
            this.cmb_payment_type.Name = "cmb_payment_type";
            this.cmb_payment_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_payment_type.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_payment_type.Size = new System.Drawing.Size(248, 20);
            this.cmb_payment_type.TabIndex = 6;
            this.cmb_payment_type.SelectedValueChanged += new System.EventHandler(this.cmb_payment_type_SelectedValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Дата посещения";
            this.labelControl2.Visible = false;
            // 
            // de_visitdate
            // 
            this.de_visitdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.de_visitdate.EditValue = null;
            this.de_visitdate.Location = new System.Drawing.Point(103, 61);
            this.de_visitdate.Name = "de_visitdate";
            this.de_visitdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_visitdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_visitdate.Size = new System.Drawing.Size(248, 20);
            this.de_visitdate.TabIndex = 8;
            this.de_visitdate.Visible = false;
            this.de_visitdate.DateTimeChanged += new System.EventHandler(this.de_visitdate_DateTimeChanged);
            // 
            // chk_at_home
            // 
            this.chk_at_home.Location = new System.Drawing.Point(13, 88);
            this.chk_at_home.Name = "chk_at_home";
            this.chk_at_home.Properties.Caption = "На дому";
            this.chk_at_home.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chk_at_home.Size = new System.Drawing.Size(75, 19);
            this.chk_at_home.TabIndex = 10;
            this.chk_at_home.Visible = false;
            // 
            // AddNewTalonForm
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(363, 123);
            this.Controls.Add(this.chk_at_home);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.de_visitdate);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cmb_payment_type);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmb_target);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(8, 150);
            this.Name = "AddNewTalonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Укажите параметры нового талона";
            ((System.ComponentModel.ISupportInitialize)(this.cmb_target.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_payment_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_visitdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_visitdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_at_home.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private DevExpress.XtraEditors.SimpleButton btn_ok;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_target;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_payment_type;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit de_visitdate;
        private DevExpress.XtraEditors.CheckEdit chk_at_home;
    }
}