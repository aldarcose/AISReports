namespace ARMPlugin.FormsAndControls
{
    partial class ReferralControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferralControl));
            this.gColDoctor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid_referral = new DevExpress.XtraGrid.GridControl();
            this.grid_RefView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDoctor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMkb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_preview = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_delete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_add = new DevExpress.XtraEditors.SimpleButton();
            this.btn_refresh = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grid_referral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_RefView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // gColDoctor
            // 
            this.gColDoctor.Caption = "Врач";
            this.gColDoctor.FieldName = "DoctorId";
            this.gColDoctor.Name = "gColDoctor";
            // 
            // grid_referral
            // 
            this.grid_referral.Location = new System.Drawing.Point(12, 12);
            this.grid_referral.MainView = this.grid_RefView;
            this.grid_referral.Name = "grid_referral";
            this.grid_referral.Size = new System.Drawing.Size(633, 440);
            this.grid_referral.TabIndex = 0;
            this.grid_referral.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grid_RefView,
            this.gridView1});
            // 
            // grid_RefView
            // 
            this.grid_RefView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNumber,
            this.gridColumnDoctor,
            this.gridColumnLpu,
            this.gridColumnDate,
            this.gridColumnMkb,
            this.gridColumnType});
            this.grid_RefView.GridControl = this.grid_referral;
            this.grid_RefView.Name = "grid_RefView";
            this.grid_RefView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnNumber
            // 
            this.gridColumnNumber.Caption = "Номер";
            this.gridColumnNumber.FieldName = "Number";
            this.gridColumnNumber.Name = "gridColumnNumber";
            this.gridColumnNumber.Visible = true;
            this.gridColumnNumber.VisibleIndex = 0;
            this.gridColumnNumber.Width = 67;
            // 
            // gridColumnDoctor
            // 
            this.gridColumnDoctor.Caption = "Врач";
            this.gridColumnDoctor.FieldName = "Doctor";
            this.gridColumnDoctor.Name = "gridColumnDoctor";
            this.gridColumnDoctor.Visible = true;
            this.gridColumnDoctor.VisibleIndex = 1;
            this.gridColumnDoctor.Width = 124;
            // 
            // gridColumnLpu
            // 
            this.gridColumnLpu.Caption = "МО";
            this.gridColumnLpu.FieldName = "ReferralLpu";
            this.gridColumnLpu.Name = "gridColumnLpu";
            this.gridColumnLpu.Visible = true;
            this.gridColumnLpu.VisibleIndex = 2;
            this.gridColumnLpu.Width = 274;
            // 
            // gridColumnDate
            // 
            this.gridColumnDate.Caption = "Дата";
            this.gridColumnDate.FieldName = "ReferralDate";
            this.gridColumnDate.Name = "gridColumnDate";
            this.gridColumnDate.Visible = true;
            this.gridColumnDate.VisibleIndex = 3;
            this.gridColumnDate.Width = 82;
            // 
            // gridColumnMkb
            // 
            this.gridColumnMkb.Caption = "МКБ";
            this.gridColumnMkb.FieldName = "MkbCode";
            this.gridColumnMkb.Name = "gridColumnMkb";
            this.gridColumnMkb.Visible = true;
            this.gridColumnMkb.VisibleIndex = 4;
            this.gridColumnMkb.Width = 68;
            // 
            // gridColumnType
            // 
            this.gridColumnType.Caption = "Тип";
            this.gridColumnType.FieldName = "ReferralType";
            this.gridColumnType.Name = "gridColumnType";
            this.gridColumnType.Visible = true;
            this.gridColumnType.VisibleIndex = 5;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.grid_referral;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Номер";
            this.gridColumn1.FieldName = "Number";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 40;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Врач";
            this.gridColumn2.FieldName = "Doctor";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "МО";
            this.gridColumn3.FieldName = "ReferralLpu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 326;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Дата";
            this.gridColumn4.FieldName = "ReferralDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 99;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "МКБ";
            this.gridColumn5.FieldName = "MkbCode";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // btn_preview
            // 
            this.btn_preview.Image = ((System.Drawing.Image)(resources.GetObject("btn_preview.Image")));
            this.btn_preview.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_preview.Location = new System.Drawing.Point(253, 456);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(112, 38);
            this.btn_preview.StyleController = this.layoutControl1;
            this.btn_preview.TabIndex = 7;
            this.btn_preview.Text = "Просмотреть";
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_delete);
            this.layoutControl1.Controls.Add(this.btn_add);
            this.layoutControl1.Controls.Add(this.btn_refresh);
            this.layoutControl1.Controls.Add(this.btn_preview);
            this.layoutControl1.Controls.Add(this.grid_referral);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(824, 358, 435, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(657, 506);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_delete
            // 
            this.btn_delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete.Image")));
            this.btn_delete.Location = new System.Drawing.Point(469, 456);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(176, 38);
            this.btn_delete.StyleController = this.layoutControl1;
            this.btn_delete.TabIndex = 10;
            this.btn_delete.Text = "Удалить";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_add
            // 
            this.btn_add.Image = ((System.Drawing.Image)(resources.GetObject("btn_add.Image")));
            this.btn_add.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_add.Location = new System.Drawing.Point(369, 456);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(96, 38);
            this.btn_add.StyleController = this.layoutControl1;
            this.btn_add.TabIndex = 9;
            this.btn_add.Text = "Добавить";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click_1);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh.Image")));
            this.btn_refresh.Location = new System.Drawing.Point(12, 456);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(237, 38);
            this.btn_refresh.StyleController = this.layoutControl1;
            this.btn_refresh.TabIndex = 8;
            this.btn_refresh.Text = "Получить данные";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(657, 506);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grid_referral;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(637, 444);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_preview;
            this.layoutControlItem2.Location = new System.Drawing.Point(241, 444);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(116, 42);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_delete;
            this.layoutControlItem5.Location = new System.Drawing.Point(457, 444);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(180, 42);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_refresh;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 444);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(241, 42);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_add;
            this.layoutControlItem4.Location = new System.Drawing.Point(357, 444);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 42);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // ReferralControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.layoutControl1);
            this.Name = "ReferralControl";
            this.Size = new System.Drawing.Size(657, 506);
            ((System.ComponentModel.ISupportInitialize)(this.grid_referral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_RefView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gColDoctor;
        private DevExpress.XtraGrid.GridControl grid_referral;
        private DevExpress.XtraGrid.Views.Grid.GridView grid_RefView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDoctor;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLpu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMkb;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton btn_preview;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btn_delete;
        private DevExpress.XtraEditors.SimpleButton btn_add;
        private DevExpress.XtraEditors.SimpleButton btn_refresh;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnType;

    }
}
