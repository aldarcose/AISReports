namespace ARMPlugin.FormsAndControls
{
    partial class DoctorQueue
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
            this.gridSchedule = new DevExpress.XtraGrid.GridControl();
            this.gvSchedule = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.lue_doc = new DevExpress.XtraEditors.LookUpEdit();
            this.lue_spec = new DevExpress.XtraEditors.LookUpEdit();
            this.lue_districts = new DevExpress.XtraEditors.LookUpEdit();
            this.lue_lpu = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnHistory = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_doc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_spec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_districts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_lpu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnHistory);
            this.layoutControl1.Controls.Add(this.gridSchedule);
            this.layoutControl1.Controls.Add(this.calendar);
            this.layoutControl1.Controls.Add(this.lue_doc);
            this.layoutControl1.Controls.Add(this.lue_spec);
            this.layoutControl1.Controls.Add(this.lue_districts);
            this.layoutControl1.Controls.Add(this.lue_lpu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(680, 230, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(652, 438);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridSchedule
            // 
            this.gridSchedule.Location = new System.Drawing.Point(182, 108);
            this.gridSchedule.MainView = this.gvSchedule;
            this.gridSchedule.Name = "gridSchedule";
            this.gridSchedule.Size = new System.Drawing.Size(458, 269);
            this.gridSchedule.TabIndex = 6;
            this.gridSchedule.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSchedule});
            // 
            // gvSchedule
            // 
            this.gvSchedule.GridControl = this.gridSchedule;
            this.gvSchedule.Name = "gvSchedule";
            this.gvSchedule.OptionsView.ShowGroupPanel = false;
            this.gvSchedule.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvSchedule_PopupMenuShowing);
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(12, 108);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 5;
            this.calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateSelected);
            // 
            // lue_doc
            // 
            this.lue_doc.Location = new System.Drawing.Point(93, 84);
            this.lue_doc.Name = "lue_doc";
            this.lue_doc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_doc.Size = new System.Drawing.Size(547, 20);
            this.lue_doc.StyleController = this.layoutControl1;
            this.lue_doc.TabIndex = 4;
            this.lue_doc.EditValueChanged += new System.EventHandler(this.lue_doc_EditValueChanged);
            // 
            // lue_spec
            // 
            this.lue_spec.Location = new System.Drawing.Point(93, 60);
            this.lue_spec.Name = "lue_spec";
            this.lue_spec.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_spec.Size = new System.Drawing.Size(547, 20);
            this.lue_spec.StyleController = this.layoutControl1;
            this.lue_spec.TabIndex = 3;
            this.lue_spec.EditValueChanged += new System.EventHandler(this.lue_spec_EditValueChanged);
            // 
            // lue_districts
            // 
            this.lue_districts.Location = new System.Drawing.Point(93, 12);
            this.lue_districts.Name = "lue_districts";
            this.lue_districts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_districts.Size = new System.Drawing.Size(547, 20);
            this.lue_districts.StyleController = this.layoutControl1;
            this.lue_districts.TabIndex = 0;
            this.lue_districts.EditValueChanged += new System.EventHandler(this.lue_districts_EditValueChanged);
            // 
            // lue_lpu
            // 
            this.lue_lpu.Location = new System.Drawing.Point(93, 36);
            this.lue_lpu.Name = "lue_lpu";
            this.lue_lpu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_lpu.Size = new System.Drawing.Size(547, 20);
            this.lue_lpu.StyleController = this.layoutControl1;
            this.lue_lpu.TabIndex = 2;
            this.lue_lpu.EditValueChanged += new System.EventHandler(this.lue_lpu_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(652, 438);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lue_lpu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(632, 24);
            this.layoutControlItem1.Text = "Учреждение";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lue_districts;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(632, 24);
            this.layoutControlItem2.Text = "Тип ЛПУ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lue_spec;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(632, 24);
            this.layoutControlItem3.Text = "Специальность";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lue_doc;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(632, 24);
            this.layoutControlItem4.Text = "Врач";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.calendar;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(170, 273);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridSchedule;
            this.layoutControlItem6.Location = new System.Drawing.Point(170, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(462, 273);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(412, 381);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(228, 45);
            this.btnHistory.StyleController = this.layoutControl1;
            this.btnHistory.TabIndex = 7;
            this.btnHistory.Text = "История записей";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnHistory;
            this.layoutControlItem7.Location = new System.Drawing.Point(400, 369);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(232, 49);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 369);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(400, 49);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DoctorQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 438);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DoctorQueue";
            this.Text = "Очередь";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_doc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_spec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_districts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_lpu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lue_doc;
        private DevExpress.XtraEditors.LookUpEdit lue_spec;
        private DevExpress.XtraEditors.LookUpEdit lue_districts;
        private DevExpress.XtraEditors.LookUpEdit lue_lpu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gridSchedule;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSchedule;
        private System.Windows.Forms.MonthCalendar calendar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnHistory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}