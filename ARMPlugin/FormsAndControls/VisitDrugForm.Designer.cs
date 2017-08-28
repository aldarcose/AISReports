namespace ARMPlugin.FormsAndControls
{
    partial class VisitDrugForm
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.memoEditComment = new DevExpress.XtraEditors.MemoEdit();
            this.txtSigna = new DevExpress.XtraEditors.TextEdit();
            this.txtDose = new DevExpress.XtraEditors.TextEdit();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateBegin = new DevExpress.XtraEditors.DateEdit();
            this.txtLSName = new DevExpress.XtraEditors.TextEdit();
            this.cBoxLS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cBoxDiagnose = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lueInjections = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSigna.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLSName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBoxLS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBoxDiagnose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInjections.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lueInjections);
            this.layoutControl1.Controls.Add(this.cBoxDiagnose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.memoEditComment);
            this.layoutControl1.Controls.Add(this.txtSigna);
            this.layoutControl1.Controls.Add(this.txtDose);
            this.layoutControl1.Controls.Add(this.dateEnd);
            this.layoutControl1.Controls.Add(this.dateBegin);
            this.layoutControl1.Controls.Add(this.txtLSName);
            this.layoutControl1.Controls.Add(this.cBoxLS);
            this.layoutControl1.Location = new System.Drawing.Point(2, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(662, 317, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(496, 527);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 493);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(234, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(250, 493);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(234, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // memoEditComment
            // 
            this.memoEditComment.Location = new System.Drawing.Point(154, 247);
            this.memoEditComment.Name = "memoEditComment";
            this.memoEditComment.Size = new System.Drawing.Size(330, 242);
            this.memoEditComment.StyleController = this.layoutControl1;
            this.memoEditComment.TabIndex = 11;
            // 
            // txtSigna
            // 
            this.txtSigna.Location = new System.Drawing.Point(154, 199);
            this.txtSigna.Name = "txtSigna";
            this.txtSigna.Size = new System.Drawing.Size(330, 20);
            this.txtSigna.StyleController = this.layoutControl1;
            this.txtSigna.TabIndex = 10;
            // 
            // txtDose
            // 
            this.txtDose.Location = new System.Drawing.Point(154, 151);
            this.txtDose.Name = "txtDose";
            this.txtDose.Size = new System.Drawing.Size(330, 20);
            this.txtDose.StyleController = this.layoutControl1;
            this.txtDose.TabIndex = 8;
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(166, 115);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Size = new System.Drawing.Size(306, 20);
            this.dateEnd.StyleController = this.layoutControl1;
            this.dateEnd.TabIndex = 7;
            // 
            // dateBegin
            // 
            this.dateBegin.EditValue = null;
            this.dateBegin.Location = new System.Drawing.Point(166, 91);
            this.dateBegin.Name = "dateBegin";
            this.dateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBegin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBegin.Size = new System.Drawing.Size(306, 20);
            this.dateBegin.StyleController = this.layoutControl1;
            this.dateBegin.TabIndex = 6;
            // 
            // txtLSName
            // 
            this.txtLSName.Location = new System.Drawing.Point(154, 36);
            this.txtLSName.Name = "txtLSName";
            this.txtLSName.Size = new System.Drawing.Size(330, 20);
            this.txtLSName.StyleController = this.layoutControl1;
            this.txtLSName.TabIndex = 5;
            // 
            // cBoxLS
            // 
            this.cBoxLS.Location = new System.Drawing.Point(154, 12);
            this.cBoxLS.Name = "cBoxLS";
            this.cBoxLS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cBoxLS.Size = new System.Drawing.Size(330, 20);
            this.cBoxLS.StyleController = this.layoutControl1;
            this.cBoxLS.TabIndex = 4;
            this.cBoxLS.TextChanged += new System.EventHandler(this.cBoxLS_TextChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlItem6,
            this.layoutControlItem9,
            this.layoutControlItem8,
            this.layoutControlItem11,
            this.layoutControlItem10,
            this.layoutControlItem12,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(496, 527);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cBoxLS;
            this.layoutControlItem1.CustomizationFormText = "Препарат";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem1.Text = "Препарат (из справочника)";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtLSName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem2.Text = "Название препарата";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(476, 91);
            this.layoutControlGroup2.Text = "Прием";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dateBegin;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(452, 24);
            this.layoutControlItem3.Text = "Начало приёма";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateEnd;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(452, 24);
            this.layoutControlItem4.Text = "Конец приёма";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtDose;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 139);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem6.Text = "Доза";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.memoEditComment;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 235);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(476, 246);
            this.layoutControlItem9.Text = "Комментарии";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtSigna;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 187);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem8.Text = "Signa";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnSave;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 481);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnCancel;
            this.layoutControlItem10.Location = new System.Drawing.Point(238, 481);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // cBoxDiagnose
            // 
            this.cBoxDiagnose.Location = new System.Drawing.Point(154, 223);
            this.cBoxDiagnose.Name = "cBoxDiagnose";
            this.cBoxDiagnose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cBoxDiagnose.Size = new System.Drawing.Size(330, 20);
            this.cBoxDiagnose.StyleController = this.layoutControl1;
            this.cBoxDiagnose.TabIndex = 14;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.cBoxDiagnose;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 211);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem12.Text = "Диагноз";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(139, 13);
            // 
            // lueInjections
            // 
            this.lueInjections.Location = new System.Drawing.Point(154, 175);
            this.lueInjections.Name = "lueInjections";
            this.lueInjections.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueInjections.Size = new System.Drawing.Size(330, 20);
            this.lueInjections.StyleController = this.layoutControl1;
            this.lueInjections.TabIndex = 15;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lueInjections;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 163);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem5.Text = "Тип инъекции";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(139, 13);
            // 
            // VisitDrugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 533);
            this.Controls.Add(this.layoutControl1);
            this.Name = "VisitDrugForm";
            this.Text = "Редактирование препаратов";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSigna.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLSName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBoxLS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBoxDiagnose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInjections.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtLSName;
        private DevExpress.XtraEditors.ComboBoxEdit cBoxLS;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraEditors.DateEdit dateBegin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtSigna;
        private DevExpress.XtraEditors.TextEdit txtDose;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.MemoEdit memoEditComment;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.ComboBoxEdit cBoxDiagnose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.LookUpEdit lueInjections;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}