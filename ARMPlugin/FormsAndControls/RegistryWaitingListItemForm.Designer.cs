namespace ARMPlugin.FormsAndControls
{
    partial class RegistryWaitingListItemForm
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
            this.rgRecordType = new DevExpress.XtraEditors.RadioGroup();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.deDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.deDateBegin = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciDateEnd = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDateBegin = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lueSpec = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lueDoctor = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgRecordType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateBegin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSpec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoctor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lueDoctor);
            this.layoutControl1.Controls.Add(this.lueSpec);
            this.layoutControl1.Controls.Add(this.rgRecordType);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.deDateEnd);
            this.layoutControl1.Controls.Add(this.deDateBegin);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(910, 245, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(554, 151);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rgRecordType
            // 
            this.rgRecordType.Location = new System.Drawing.Point(98, 60);
            this.rgRecordType.Name = "rgRecordType";
            this.rgRecordType.Properties.Columns = 2;
            this.rgRecordType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Одиночная"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Групповая")});
            this.rgRecordType.Size = new System.Drawing.Size(444, 53);
            this.rgRecordType.StyleController = this.layoutControl1;
            this.rgRecordType.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(279, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(263, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(263, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // deDateEnd
            // 
            this.deDateEnd.EditValue = null;
            this.deDateEnd.Location = new System.Drawing.Point(365, 12);
            this.deDateEnd.Name = "deDateEnd";
            this.deDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateEnd.Size = new System.Drawing.Size(177, 20);
            this.deDateEnd.StyleController = this.layoutControl1;
            this.deDateEnd.TabIndex = 5;
            // 
            // deDateBegin
            // 
            this.deDateBegin.EditValue = null;
            this.deDateBegin.Location = new System.Drawing.Point(98, 12);
            this.deDateBegin.Name = "deDateBegin";
            this.deDateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateBegin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateBegin.Size = new System.Drawing.Size(177, 20);
            this.deDateBegin.StyleController = this.layoutControl1;
            this.deDateBegin.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciDateEnd,
            this.lciDateBegin,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(554, 151);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciDateEnd
            // 
            this.lciDateEnd.Control = this.deDateEnd;
            this.lciDateEnd.Location = new System.Drawing.Point(267, 0);
            this.lciDateEnd.Name = "lciDateEnd";
            this.lciDateEnd.Size = new System.Drawing.Size(267, 24);
            this.lciDateEnd.Text = "Дата окончания";
            this.lciDateEnd.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lciDateBegin
            // 
            this.lciDateBegin.Control = this.deDateBegin;
            this.lciDateBegin.Location = new System.Drawing.Point(0, 0);
            this.lciDateBegin.Name = "lciDateBegin";
            this.lciDateBegin.Size = new System.Drawing.Size(267, 24);
            this.lciDateBegin.Text = "Дата начала";
            this.lciDateBegin.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 105);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(267, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(267, 105);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(267, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.rgRecordType;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(141, 29);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(534, 57);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Тип записи";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lueSpec
            // 
            this.lueSpec.Location = new System.Drawing.Point(98, 36);
            this.lueSpec.Name = "lueSpec";
            this.lueSpec.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSpec.Size = new System.Drawing.Size(177, 20);
            this.lueSpec.StyleController = this.layoutControl1;
            this.lueSpec.TabIndex = 11;
            this.lueSpec.EditValueChanged += new System.EventHandler(this.lueSpec_EditValueChanged);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lueSpec;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(267, 24);
            this.layoutControlItem1.Text = "Специальность";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lueDoctor
            // 
            this.lueDoctor.Location = new System.Drawing.Point(365, 36);
            this.lueDoctor.Name = "lueDoctor";
            this.lueDoctor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDoctor.Size = new System.Drawing.Size(177, 20);
            this.lueDoctor.StyleController = this.layoutControl1;
            this.lueDoctor.TabIndex = 12;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lueDoctor;
            this.layoutControlItem2.Location = new System.Drawing.Point(267, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(267, 24);
            this.layoutControlItem2.Text = "Врач";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 13);
            // 
            // RegistryWaitingListItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 151);
            this.Controls.Add(this.layoutControl1);
            this.Name = "RegistryWaitingListItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запись в  лист ожидания";
            this.Load += new System.EventHandler(this.RegistryWaitingListItemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgRecordType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateBegin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSpec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoctor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.RadioGroup rgRecordType;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.DateEdit deDateEnd;
        private DevExpress.XtraEditors.DateEdit deDateBegin;
        private DevExpress.XtraLayout.LayoutControlItem lciDateEnd;
        private DevExpress.XtraLayout.LayoutControlItem lciDateBegin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LookUpEdit lueDoctor;
        private DevExpress.XtraEditors.LookUpEdit lueSpec;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}