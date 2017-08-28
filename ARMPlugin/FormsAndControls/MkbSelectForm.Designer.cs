namespace ARMPlugin.FormsAndControls
{
    partial class MkbSelectForm
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_select = new DevExpress.XtraEditors.SimpleButton();
            this.te_chosen = new DevExpress.XtraEditors.TextEdit();
            this.cmb_diagn = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_subclass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_class = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_chosen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_diagn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_subclass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_class.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.te_chosen);
            this.layoutControl1.Controls.Add(this.cmb_diagn);
            this.layoutControl1.Controls.Add(this.cmb_subclass);
            this.layoutControl1.Controls.Add(this.cmb_class);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1008, 387, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(740, 124);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(648, 126);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 22);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "Отмена";
            // 
            // btn_select
            // 
            this.btn_select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_select.Location = new System.Drawing.Point(562, 126);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(80, 22);
            this.btn_select.TabIndex = 8;
            this.btn_select.Text = "Выбрать";
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // te_chosen
            // 
            this.te_chosen.Location = new System.Drawing.Point(117, 84);
            this.te_chosen.Name = "te_chosen";
            this.te_chosen.Properties.ReadOnly = true;
            this.te_chosen.Size = new System.Drawing.Size(611, 20);
            this.te_chosen.StyleController = this.layoutControl1;
            this.te_chosen.TabIndex = 7;
            this.te_chosen.TextChanged += new System.EventHandler(this.te_chosen_TextChanged);
            // 
            // cmb_diagn
            // 
            this.cmb_diagn.Location = new System.Drawing.Point(117, 60);
            this.cmb_diagn.Name = "cmb_diagn";
            this.cmb_diagn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_diagn.Size = new System.Drawing.Size(611, 20);
            this.cmb_diagn.StyleController = this.layoutControl1;
            this.cmb_diagn.TabIndex = 6;
            this.cmb_diagn.SelectedIndexChanged += new System.EventHandler(this.cmb_diagn_SelectedIndexChanged);
            // 
            // cmb_subclass
            // 
            this.cmb_subclass.Location = new System.Drawing.Point(117, 36);
            this.cmb_subclass.Name = "cmb_subclass";
            this.cmb_subclass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_subclass.Size = new System.Drawing.Size(611, 20);
            this.cmb_subclass.StyleController = this.layoutControl1;
            this.cmb_subclass.TabIndex = 5;
            this.cmb_subclass.SelectedIndexChanged += new System.EventHandler(this.cmb_subclass_SelectedIndexChanged);
            // 
            // cmb_class
            // 
            this.cmb_class.Location = new System.Drawing.Point(117, 12);
            this.cmb_class.Name = "cmb_class";
            this.cmb_class.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_class.Size = new System.Drawing.Size(611, 20);
            this.cmb_class.StyleController = this.layoutControl1;
            this.cmb_class.TabIndex = 4;
            this.cmb_class.SelectedIndexChanged += new System.EventHandler(this.cmb_class_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(740, 124);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmb_class;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(720, 24);
            this.layoutControlItem1.Text = "Класс";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmb_subclass;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(720, 24);
            this.layoutControlItem2.Text = "Подкласс";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmb_diagn;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(720, 24);
            this.layoutControlItem3.Text = "Диагноз";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.te_chosen;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(720, 32);
            this.layoutControlItem4.Text = "Выбранный диагноз";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(102, 13);
            // 
            // MkbSelectForm
            // 
            this.AcceptButton = this.btn_select;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(740, 160);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_select);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(8, 187);
            this.Name = "MkbSelectForm";
            this.Text = "Выбор диагноза";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_chosen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_diagn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_subclass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_class.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private DevExpress.XtraEditors.SimpleButton btn_select;
        private DevExpress.XtraEditors.TextEdit te_chosen;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_diagn;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_subclass;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_class;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}