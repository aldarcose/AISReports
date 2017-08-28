namespace ARMPlugin.FormsAndControls
{
    partial class DopDispDiagnosForm
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
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.MKBCodeLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForMKBCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DiseaseVidIdLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForDiseaseVidId = new DevExpress.XtraLayout.LayoutControlItem();
            this.StageIdLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForStageId = new DevExpress.XtraLayout.LayoutControlItem();
            this.DoctorIdLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForDoctorId = new DevExpress.XtraLayout.LayoutControlItem();
            this.EtapLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForEtap = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btnPrevDiagnos = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dopDispDiagnosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MKBCodeLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMKBCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiseaseVidIdLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDiseaseVidId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StageIdLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForStageId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorIdLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDoctorId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EtapLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEtap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dopDispDiagnosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.btnSave);
            this.dataLayoutControl1.Controls.Add(this.btnCancel);
            this.dataLayoutControl1.Controls.Add(this.btnPrevDiagnos);
            this.dataLayoutControl1.Controls.Add(this.MKBCodeLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.DiseaseVidIdLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.StageIdLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.DoctorIdLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.EtapLookUpEdit);
            this.dataLayoutControl1.DataSource = this.dopDispDiagnosBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(483, 197);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(483, 197);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // MKBCodeLookUpEdit
            // 
            this.MKBCodeLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dopDispDiagnosBindingSource, "MKBCode", true));
            this.MKBCodeLookUpEdit.Location = new System.Drawing.Point(84, 38);
            this.MKBCodeLookUpEdit.Name = "MKBCodeLookUpEdit";
            this.MKBCodeLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MKBCodeLookUpEdit.Properties.NullText = "";
            this.MKBCodeLookUpEdit.Size = new System.Drawing.Size(387, 20);
            this.MKBCodeLookUpEdit.StyleController = this.dataLayoutControl1;
            this.MKBCodeLookUpEdit.TabIndex = 4;
            // 
            // ItemForMKBCode
            // 
            this.ItemForMKBCode.Control = this.MKBCodeLookUpEdit;
            this.ItemForMKBCode.Location = new System.Drawing.Point(0, 26);
            this.ItemForMKBCode.Name = "ItemForMKBCode";
            this.ItemForMKBCode.Size = new System.Drawing.Size(463, 24);
            this.ItemForMKBCode.Text = "МКБ";
            this.ItemForMKBCode.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForMKBCode,
            this.ItemForDiseaseVidId,
            this.ItemForStageId,
            this.ItemForDoctorId,
            this.ItemForEtap,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(463, 177);
            // 
            // DiseaseVidIdLookUpEdit
            // 
            this.DiseaseVidIdLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dopDispDiagnosBindingSource, "DiseaseVidId", true));
            this.DiseaseVidIdLookUpEdit.Location = new System.Drawing.Point(84, 62);
            this.DiseaseVidIdLookUpEdit.Name = "DiseaseVidIdLookUpEdit";
            this.DiseaseVidIdLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DiseaseVidIdLookUpEdit.Properties.NullText = "";
            this.DiseaseVidIdLookUpEdit.Size = new System.Drawing.Size(387, 20);
            this.DiseaseVidIdLookUpEdit.StyleController = this.dataLayoutControl1;
            this.DiseaseVidIdLookUpEdit.TabIndex = 5;
            // 
            // ItemForDiseaseVidId
            // 
            this.ItemForDiseaseVidId.Control = this.DiseaseVidIdLookUpEdit;
            this.ItemForDiseaseVidId.Location = new System.Drawing.Point(0, 50);
            this.ItemForDiseaseVidId.Name = "ItemForDiseaseVidId";
            this.ItemForDiseaseVidId.Size = new System.Drawing.Size(463, 24);
            this.ItemForDiseaseVidId.Text = "Тип диагноза";
            this.ItemForDiseaseVidId.TextSize = new System.Drawing.Size(68, 13);
            // 
            // StageIdLookUpEdit
            // 
            this.StageIdLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dopDispDiagnosBindingSource, "StageId", true));
            this.StageIdLookUpEdit.Location = new System.Drawing.Point(84, 86);
            this.StageIdLookUpEdit.Name = "StageIdLookUpEdit";
            this.StageIdLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StageIdLookUpEdit.Properties.NullText = "";
            this.StageIdLookUpEdit.Size = new System.Drawing.Size(387, 20);
            this.StageIdLookUpEdit.StyleController = this.dataLayoutControl1;
            this.StageIdLookUpEdit.TabIndex = 6;
            // 
            // ItemForStageId
            // 
            this.ItemForStageId.Control = this.StageIdLookUpEdit;
            this.ItemForStageId.Location = new System.Drawing.Point(0, 74);
            this.ItemForStageId.Name = "ItemForStageId";
            this.ItemForStageId.Size = new System.Drawing.Size(463, 24);
            this.ItemForStageId.Text = "Стадия";
            this.ItemForStageId.TextSize = new System.Drawing.Size(68, 13);
            // 
            // DoctorIdLookUpEdit
            // 
            this.DoctorIdLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dopDispDiagnosBindingSource, "DoctorId", true));
            this.DoctorIdLookUpEdit.Location = new System.Drawing.Point(84, 110);
            this.DoctorIdLookUpEdit.Name = "DoctorIdLookUpEdit";
            this.DoctorIdLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DoctorIdLookUpEdit.Properties.NullText = "";
            this.DoctorIdLookUpEdit.Size = new System.Drawing.Size(387, 20);
            this.DoctorIdLookUpEdit.StyleController = this.dataLayoutControl1;
            this.DoctorIdLookUpEdit.TabIndex = 7;
            // 
            // ItemForDoctorId
            // 
            this.ItemForDoctorId.Control = this.DoctorIdLookUpEdit;
            this.ItemForDoctorId.Location = new System.Drawing.Point(0, 98);
            this.ItemForDoctorId.Name = "ItemForDoctorId";
            this.ItemForDoctorId.Size = new System.Drawing.Size(463, 24);
            this.ItemForDoctorId.Text = "Врач";
            this.ItemForDoctorId.TextSize = new System.Drawing.Size(68, 13);
            // 
            // EtapLookUpEdit
            // 
            this.EtapLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dopDispDiagnosBindingSource, "Etap", true));
            this.EtapLookUpEdit.Location = new System.Drawing.Point(84, 134);
            this.EtapLookUpEdit.Name = "EtapLookUpEdit";
            this.EtapLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EtapLookUpEdit.Properties.NullText = "";
            this.EtapLookUpEdit.Size = new System.Drawing.Size(387, 20);
            this.EtapLookUpEdit.StyleController = this.dataLayoutControl1;
            this.EtapLookUpEdit.TabIndex = 8;
            // 
            // ItemForEtap
            // 
            this.ItemForEtap.Control = this.EtapLookUpEdit;
            this.ItemForEtap.Location = new System.Drawing.Point(0, 122);
            this.ItemForEtap.Name = "ItemForEtap";
            this.ItemForEtap.Size = new System.Drawing.Size(463, 24);
            this.ItemForEtap.Text = "Этап";
            this.ItemForEtap.TextSize = new System.Drawing.Size(68, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 146);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(280, 31);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnPrevDiagnos
            // 
            this.btnPrevDiagnos.Location = new System.Drawing.Point(12, 12);
            this.btnPrevDiagnos.Name = "btnPrevDiagnos";
            this.btnPrevDiagnos.Size = new System.Drawing.Size(459, 22);
            this.btnPrevDiagnos.StyleController = this.dataLayoutControl1;
            this.btnPrevDiagnos.TabIndex = 1;
            this.btnPrevDiagnos.Text = "Выбрать из ранее поставленных";
            this.btnPrevDiagnos.Click += new System.EventHandler(this.btnPrevDiagnos_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnPrevDiagnos;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(463, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(393, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 22);
            this.btnCancel.StyleController = this.dataLayoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(381, 146);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(82, 31);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(292, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 22);
            this.btnSave.StyleController = this.dataLayoutControl1;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            this.layoutControlItem2.Location = new System.Drawing.Point(280, 146);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(101, 31);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // dopDispDiagnosBindingSource
            // 
            this.dopDispDiagnosBindingSource.DataSource = typeof(Model.Classes.DopDisp.DopDispDiagnos);
            // 
            // DopDispDiagnosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 197);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "DopDispDiagnosForm";
            this.Text = "Диагноз";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MKBCodeLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMKBCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiseaseVidIdLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDiseaseVidId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StageIdLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForStageId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorIdLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDoctorId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EtapLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEtap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dopDispDiagnosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrevDiagnos;
        private DevExpress.XtraEditors.LookUpEdit MKBCodeLookUpEdit;
        private System.Windows.Forms.BindingSource dopDispDiagnosBindingSource;
        private DevExpress.XtraEditors.LookUpEdit DiseaseVidIdLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit StageIdLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit DoctorIdLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit EtapLookUpEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMKBCode;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDiseaseVidId;
        private DevExpress.XtraLayout.LayoutControlItem ItemForStageId;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDoctorId;
        private DevExpress.XtraLayout.LayoutControlItem ItemForEtap;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;


    }
}