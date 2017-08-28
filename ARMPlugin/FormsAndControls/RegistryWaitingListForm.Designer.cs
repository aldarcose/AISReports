namespace ARMPlugin.FormsAndControls
{
    partial class RegistryWaitingListForm
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.gridWaitList = new DevExpress.XtraGrid.GridControl();
            this.waitingListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvWaitList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Speciality = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DateBegin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DateEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TalonId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Doctor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ApptDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnReload = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWaitList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitingListItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWaitList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnReload);
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.btnEdit);
            this.layoutControl1.Controls.Add(this.btnDelete);
            this.layoutControl1.Controls.Add(this.gridWaitList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(639, 465);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(163, 431);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(147, 22);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(314, 431);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(147, 22);
            this.btnEdit.StyleController = this.layoutControl1;
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(465, 431);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(162, 22);
            this.btnDelete.StyleController = this.layoutControl1;
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gridWaitList
            // 
            this.gridWaitList.DataSource = this.waitingListItemBindingSource;
            this.gridWaitList.Location = new System.Drawing.Point(12, 12);
            this.gridWaitList.MainView = this.gvWaitList;
            this.gridWaitList.Name = "gridWaitList";
            this.gridWaitList.Size = new System.Drawing.Size(615, 415);
            this.gridWaitList.TabIndex = 4;
            this.gridWaitList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWaitList});
            // 
            // waitingListItemBindingSource
            // 
            this.waitingListItemBindingSource.DataSource = typeof(Model.Classes.Registry.WaitingListItem);
            // 
            // gvWaitList
            // 
            this.gvWaitList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Speciality,
            this.DateBegin,
            this.DateEnd,
            this.TalonId,
            this.Doctor,
            this.ApptDateTime});
            this.gvWaitList.GridControl = this.gridWaitList;
            this.gvWaitList.Name = "gvWaitList";
            this.gvWaitList.OptionsView.ShowGroupPanel = false;
            this.gvWaitList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvWaitList_FocusedRowChanged);
            // 
            // Speciality
            // 
            this.Speciality.Caption = "Специальность";
            this.Speciality.FieldName = "SpecialityName";
            this.Speciality.Name = "Speciality";
            this.Speciality.Visible = true;
            this.Speciality.VisibleIndex = 0;
            // 
            // DateBegin
            // 
            this.DateBegin.Caption = "Дата начала";
            this.DateBegin.FieldName = "DateBegin";
            this.DateBegin.Name = "DateBegin";
            this.DateBegin.Visible = true;
            this.DateBegin.VisibleIndex = 1;
            // 
            // DateEnd
            // 
            this.DateEnd.Caption = "Дата окончания";
            this.DateEnd.FieldName = "DateEnd";
            this.DateEnd.Name = "DateEnd";
            this.DateEnd.Visible = true;
            this.DateEnd.VisibleIndex = 2;
            // 
            // TalonId
            // 
            this.TalonId.Caption = "Номер талона";
            this.TalonId.FieldName = "TalonId";
            this.TalonId.Name = "TalonId";
            this.TalonId.Visible = true;
            this.TalonId.VisibleIndex = 3;
            // 
            // Doctor
            // 
            this.Doctor.Caption = "Врач";
            this.Doctor.FieldName = "Doctor.FIO";
            this.Doctor.Name = "Doctor";
            this.Doctor.Visible = true;
            this.Doctor.VisibleIndex = 4;
            // 
            // ApptDateTime
            // 
            this.ApptDateTime.Caption = "Дата/Время";
            this.ApptDateTime.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.ApptDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ApptDateTime.FieldName = "AppointmentDateTime";
            this.ApptDateTime.Name = "ApptDateTime";
            this.ApptDateTime.Visible = true;
            this.ApptDateTime.VisibleIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(639, 465);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridWaitList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(619, 419);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAdd;
            this.layoutControlItem5.Location = new System.Drawing.Point(151, 419);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(151, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnDelete;
            this.layoutControlItem3.Location = new System.Drawing.Point(453, 419);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(166, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnEdit;
            this.layoutControlItem4.Location = new System.Drawing.Point(302, 419);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(151, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(12, 431);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(147, 22);
            this.btnReload.StyleController = this.layoutControl1;
            this.btnReload.TabIndex = 9;
            this.btnReload.Text = "Обновить";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnReload;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 419);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(151, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // RegistryWaitingListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 465);
            this.Controls.Add(this.layoutControl1);
            this.Name = "RegistryWaitingListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лист ожидания";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridWaitList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitingListItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWaitList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridWaitList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWaitList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn Speciality;
        private DevExpress.XtraGrid.Columns.GridColumn DateBegin;
        private DevExpress.XtraGrid.Columns.GridColumn DateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn TalonId;
        private System.Windows.Forms.BindingSource waitingListItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn Doctor;
        private DevExpress.XtraGrid.Columns.GridColumn ApptDateTime;
        private DevExpress.XtraEditors.SimpleButton btnReload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}