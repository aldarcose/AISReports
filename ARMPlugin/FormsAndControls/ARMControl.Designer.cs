using Shared.FormsAndControls;

namespace ARMPlugin.FormsAndControls
{
    partial class ARMControl : PluginControl
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
            this.tcDisp = new DevExpress.XtraTab.XtraTabControl();
            this.PatientTab = new DevExpress.XtraTab.XtraTabPage();
            this.tabData = new DevExpress.XtraTab.XtraTabControl();
            this.tabCommon = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl10 = new DevExpress.XtraLayout.LayoutControl();
            this.btnChange = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteLgota = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddLgota = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.grid_lgotas = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.group_fluo = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl8 = new DevExpress.XtraLayout.LayoutControl();
            this.te_fluo_num = new DevExpress.XtraEditors.TextEdit();
            this.te_risk_group = new DevExpress.XtraEditors.TextEdit();
            this.te_fluo_date = new DevExpress.XtraEditors.TextEdit();
            this.te_fluo_place = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gc_med = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_dopUchastok = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_uchastok = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gc_main = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.luAttachLPU = new DevExpress.XtraEditors.LookUpEdit();
            this.cmb_socStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btne_factPlace = new DevExpress.XtraEditors.ButtonEdit();
            this.btne_regPlace = new DevExpress.XtraEditors.ButtonEdit();
            this.cmb_gender = new DevExpress.XtraEditors.ComboBoxEdit();
            this.de_bornDate = new DevExpress.XtraEditors.DateEdit();
            this.te_fio = new DevExpress.XtraEditors.TextEdit();
            this.te_bornPlace = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabDocs = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.group_mse = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl7 = new DevExpress.XtraLayout.LayoutControl();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gc_policy = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.cmb_polSmo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_polSmoRegion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.te_polNumber = new DevExpress.XtraEditors.TextEdit();
            this.te_polSerial = new DevExpress.XtraEditors.TextEdit();
            this.de_polDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.de_polDateBeg = new DevExpress.XtraEditors.DateEdit();
            this.cmb_polType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gc_document = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.te_docOrg = new DevExpress.XtraEditors.TextEdit();
            this.te_docNumber = new DevExpress.XtraEditors.TextEdit();
            this.te_docSerial = new DevExpress.XtraEditors.TextEdit();
            this.de_docDateBeg = new DevExpress.XtraEditors.DateEdit();
            this.cmb_docType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.phoneControl1 = new SharedUtils.FormsAndControls.PhoneControl();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEdit6 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tapControl1 = new ARMPlugin.FormsAndControls.TAPControl();
            this.tabLaboratory = new DevExpress.XtraTab.XtraTabPage();
            this.labResearchControl1 = new ARMPlugin.FormsAndControls.LabResearchControl();
            this.tabReferral = new DevExpress.XtraTab.XtraTabPage();
            this.referralControl1 = new ARMPlugin.FormsAndControls.ReferralControl();
            this.tabVaccination = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl9 = new DevExpress.XtraLayout.LayoutControl();
            this.vaccinationControl1 = new ARMPlugin.FormsAndControls.VaccinationControl();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl11 = new DevExpress.XtraLayout.LayoutControl();
            this.dispenseryControl1 = new ARMPlugin.FormsAndControls.DispenseryControl();
            this.layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl12 = new DevExpress.XtraLayout.LayoutControl();
            this.seakLeaveControl1 = new ARMPlugin.FormsAndControls.SeakLeaveControl();
            this.layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.DopDispPage = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl13 = new DevExpress.XtraLayout.LayoutControl();
            this.dopDispControl1 = new ARMPlugin.FormsAndControls.DopDispControl();
            this.layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.tcDisp)).BeginInit();
            this.tcDisp.SuspendLayout();
            this.PatientTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabData)).BeginInit();
            this.tabData.SuspendLayout();
            this.tabCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).BeginInit();
            this.layoutControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_lgotas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.group_fluo)).BeginInit();
            this.group_fluo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).BeginInit();
            this.layoutControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_fluo_num.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_risk_group.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_fluo_date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_fluo_place.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_med)).BeginInit();
            this.gc_med.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_dopUchastok.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_uchastok.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_main)).BeginInit();
            this.gc_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luAttachLPU.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_socStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btne_factPlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btne_regPlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_gender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_bornDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_bornDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_fio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_bornPlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            this.tabDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.group_mse)).BeginInit();
            this.group_mse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).BeginInit();
            this.layoutControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_policy)).BeginInit();
            this.gc_policy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_polSmo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_polSmoRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_polNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_polSerial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateBeg.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateBeg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_polType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_document)).BeginInit();
            this.gc_document.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_docOrg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_docNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_docSerial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_docDateBeg.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_docDateBeg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_docType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            this.tabLaboratory.SuspendLayout();
            this.tabReferral.SuspendLayout();
            this.tabVaccination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).BeginInit();
            this.layoutControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).BeginInit();
            this.layoutControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl12)).BeginInit();
            this.layoutControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            this.DopDispPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl13)).BeginInit();
            this.layoutControl13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).BeginInit();
            this.SuspendLayout();
            // 
            // tcDisp
            // 
            this.tcDisp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDisp.Location = new System.Drawing.Point(0, 0);
            this.tcDisp.Margin = new System.Windows.Forms.Padding(0);
            this.tcDisp.Name = "tcDisp";
            this.tcDisp.SelectedTabPage = this.PatientTab;
            this.tcDisp.Size = new System.Drawing.Size(902, 565);
            this.tcDisp.TabIndex = 0;
            this.tcDisp.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.PatientTab,
            this.xtraTabPage2,
            this.tabLaboratory,
            this.tabReferral,
            this.tabVaccination,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.DopDispPage});
            // 
            // PatientTab
            // 
            this.PatientTab.Controls.Add(this.tabData);
            this.PatientTab.Name = "PatientTab";
            this.PatientTab.Size = new System.Drawing.Size(896, 537);
            this.PatientTab.Text = "Данные";
            // 
            // tabData
            // 
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.tabData.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.tabData.Location = new System.Drawing.Point(0, 0);
            this.tabData.Name = "tabData";
            this.tabData.SelectedTabPage = this.tabCommon;
            this.tabData.Size = new System.Drawing.Size(896, 537);
            this.tabData.TabIndex = 0;
            this.tabData.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabCommon,
            this.tabDocs,
            this.xtraTabPage1});
            // 
            // tabCommon
            // 
            this.tabCommon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabCommon.Controls.Add(this.layoutControl10);
            this.tabCommon.Controls.Add(this.grid_lgotas);
            this.tabCommon.Controls.Add(this.group_fluo);
            this.tabCommon.Controls.Add(this.gc_med);
            this.tabCommon.Controls.Add(this.gc_main);
            this.tabCommon.Name = "tabCommon";
            this.tabCommon.Size = new System.Drawing.Size(818, 531);
            this.tabCommon.Text = "Общие";
            // 
            // layoutControl10
            // 
            this.layoutControl10.Controls.Add(this.btnChange);
            this.layoutControl10.Controls.Add(this.btnDeleteLgota);
            this.layoutControl10.Controls.Add(this.btnAddLgota);
            this.layoutControl10.Location = new System.Drawing.Point(7, 489);
            this.layoutControl10.Name = "layoutControl10";
            this.layoutControl10.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(938, 362, 250, 350);
            this.layoutControl10.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl10.Root = this.layoutControlGroup10;
            this.layoutControl10.Size = new System.Drawing.Size(804, 38);
            this.layoutControl10.TabIndex = 4;
            this.layoutControl10.Text = "layoutControl10";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(285, 7);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(220, 22);
            this.btnChange.StyleController = this.layoutControl10;
            this.btnChange.TabIndex = 6;
            this.btnChange.Text = "Изменить";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnDeleteLgota
            // 
            this.btnDeleteLgota.Location = new System.Drawing.Point(509, 7);
            this.btnDeleteLgota.Name = "btnDeleteLgota";
            this.btnDeleteLgota.Size = new System.Drawing.Size(288, 22);
            this.btnDeleteLgota.StyleController = this.layoutControl10;
            this.btnDeleteLgota.TabIndex = 5;
            this.btnDeleteLgota.Text = "Удалить";
            this.btnDeleteLgota.Click += new System.EventHandler(this.btnDeleteLgota_Click);
            // 
            // btnAddLgota
            // 
            this.btnAddLgota.Location = new System.Drawing.Point(7, 7);
            this.btnAddLgota.Name = "btnAddLgota";
            this.btnAddLgota.Size = new System.Drawing.Size(274, 22);
            this.btnAddLgota.StyleController = this.layoutControl10;
            this.btnAddLgota.TabIndex = 4;
            this.btnAddLgota.Text = "Добавить";
            this.btnAddLgota.Click += new System.EventHandler(this.btnAddLgota_Click);
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup10.GroupBordersVisible = false;
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem37,
            this.layoutControlItem38,
            this.layoutControlItem39});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup10.Name = "Root";
            this.layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup10.Size = new System.Drawing.Size(804, 38);
            this.layoutControlGroup10.TextVisible = false;
            // 
            // layoutControlItem37
            // 
            this.layoutControlItem37.Control = this.btnAddLgota;
            this.layoutControlItem37.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem37.Name = "layoutControlItem37";
            this.layoutControlItem37.Size = new System.Drawing.Size(278, 28);
            this.layoutControlItem37.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem37.TextVisible = false;
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.btnDeleteLgota;
            this.layoutControlItem38.Location = new System.Drawing.Point(502, 0);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(292, 28);
            this.layoutControlItem38.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.btnChange;
            this.layoutControlItem39.Location = new System.Drawing.Point(278, 0);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(224, 28);
            this.layoutControlItem39.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem39.TextVisible = false;
            // 
            // grid_lgotas
            // 
            this.grid_lgotas.Location = new System.Drawing.Point(7, 387);
            this.grid_lgotas.MainView = this.gridView1;
            this.grid_lgotas.Name = "grid_lgotas";
            this.grid_lgotas.Size = new System.Drawing.Size(808, 96);
            this.grid_lgotas.TabIndex = 3;
            this.grid_lgotas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grid_lgotas;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // group_fluo
            // 
            this.group_fluo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_fluo.Controls.Add(this.layoutControl8);
            this.group_fluo.Location = new System.Drawing.Point(7, 277);
            this.group_fluo.Name = "group_fluo";
            this.group_fluo.Size = new System.Drawing.Size(808, 106);
            this.group_fluo.TabIndex = 2;
            this.group_fluo.Text = "Флюорография";
            // 
            // layoutControl8
            // 
            this.layoutControl8.Controls.Add(this.te_fluo_num);
            this.layoutControl8.Controls.Add(this.te_risk_group);
            this.layoutControl8.Controls.Add(this.te_fluo_date);
            this.layoutControl8.Controls.Add(this.te_fluo_place);
            this.layoutControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl8.Location = new System.Drawing.Point(2, 21);
            this.layoutControl8.Name = "layoutControl8";
            this.layoutControl8.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl8.Root = this.layoutControlGroup8;
            this.layoutControl8.Size = new System.Drawing.Size(804, 83);
            this.layoutControl8.TabIndex = 0;
            this.layoutControl8.Text = "layoutControl8";
            // 
            // te_fluo_num
            // 
            this.te_fluo_num.Location = new System.Drawing.Point(613, 36);
            this.te_fluo_num.Name = "te_fluo_num";
            this.te_fluo_num.Properties.ReadOnly = true;
            this.te_fluo_num.Size = new System.Drawing.Size(179, 20);
            this.te_fluo_num.StyleController = this.layoutControl8;
            this.te_fluo_num.TabIndex = 7;
            // 
            // te_risk_group
            // 
            this.te_risk_group.Location = new System.Drawing.Point(110, 36);
            this.te_risk_group.Name = "te_risk_group";
            this.te_risk_group.Properties.ReadOnly = true;
            this.te_risk_group.Size = new System.Drawing.Size(401, 20);
            this.te_risk_group.StyleController = this.layoutControl8;
            this.te_risk_group.TabIndex = 6;
            // 
            // te_fluo_date
            // 
            this.te_fluo_date.Location = new System.Drawing.Point(613, 12);
            this.te_fluo_date.Name = "te_fluo_date";
            this.te_fluo_date.Properties.ReadOnly = true;
            this.te_fluo_date.Size = new System.Drawing.Size(179, 20);
            this.te_fluo_date.StyleController = this.layoutControl8;
            this.te_fluo_date.TabIndex = 5;
            // 
            // te_fluo_place
            // 
            this.te_fluo_place.Location = new System.Drawing.Point(110, 12);
            this.te_fluo_place.Name = "te_fluo_place";
            this.te_fluo_place.Properties.ReadOnly = true;
            this.te_fluo_place.Size = new System.Drawing.Size(401, 20);
            this.te_fluo_place.StyleController = this.layoutControl8;
            this.te_fluo_place.TabIndex = 4;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.layoutControlItem34});
            this.layoutControlGroup8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Size = new System.Drawing.Size(804, 83);
            this.layoutControlGroup8.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.te_fluo_place;
            this.layoutControlItem31.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(503, 24);
            this.layoutControlItem31.Text = "Место проведения";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.te_fluo_date;
            this.layoutControlItem32.Location = new System.Drawing.Point(503, 0);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(281, 24);
            this.layoutControlItem32.Text = "Дата проведения:";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.te_risk_group;
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(503, 39);
            this.layoutControlItem33.Text = "Группа риска:";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.te_fluo_num;
            this.layoutControlItem34.Location = new System.Drawing.Point(503, 24);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(281, 39);
            this.layoutControlItem34.Text = "№ исследования:";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(95, 13);
            // 
            // gc_med
            // 
            this.gc_med.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_med.Controls.Add(this.layoutControl3);
            this.gc_med.Location = new System.Drawing.Point(5, 196);
            this.gc_med.Name = "gc_med";
            this.gc_med.ShowCaption = false;
            this.gc_med.Size = new System.Drawing.Size(808, 75);
            this.gc_med.TabIndex = 1;
            this.gc_med.Text = "groupControl1";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.buttonEdit1);
            this.layoutControl3.Controls.Add(this.comboBoxEdit3);
            this.layoutControl3.Controls.Add(this.cmb_dopUchastok);
            this.layoutControl3.Controls.Add(this.cmb_uchastok);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(2, 2);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(804, 71);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(520, 36);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(272, 20);
            this.buttonEdit1.StyleController = this.layoutControl3;
            this.buttonEdit1.TabIndex = 7;
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.Location = new System.Drawing.Point(129, 36);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Size = new System.Drawing.Size(270, 20);
            this.comboBoxEdit3.StyleController = this.layoutControl3;
            this.comboBoxEdit3.TabIndex = 6;
            // 
            // cmb_dopUchastok
            // 
            this.cmb_dopUchastok.Location = new System.Drawing.Point(520, 12);
            this.cmb_dopUchastok.Name = "cmb_dopUchastok";
            this.cmb_dopUchastok.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_dopUchastok.Size = new System.Drawing.Size(272, 20);
            this.cmb_dopUchastok.StyleController = this.layoutControl3;
            this.cmb_dopUchastok.TabIndex = 5;
            this.cmb_dopUchastok.SelectedIndexChanged += new System.EventHandler(this.cmb_dopUchastok_SelectedIndexChanged);
            // 
            // cmb_uchastok
            // 
            this.cmb_uchastok.Location = new System.Drawing.Point(129, 12);
            this.cmb_uchastok.Name = "cmb_uchastok";
            this.cmb_uchastok.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_uchastok.Size = new System.Drawing.Size(270, 20);
            this.cmb_uchastok.StyleController = this.layoutControl3;
            this.cmb_uchastok.TabIndex = 4;
            this.cmb_uchastok.SelectedIndexChanged += new System.EventHandler(this.cmb_uchastok_SelectedIndexChanged);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(804, 71);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmb_uchastok;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(391, 24);
            this.layoutControlItem8.Text = "Участок";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(114, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cmb_dopUchastok;
            this.layoutControlItem9.Location = new System.Drawing.Point(391, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(393, 24);
            this.layoutControlItem9.Text = "Доп. участок";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(114, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.comboBoxEdit3;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(391, 27);
            this.layoutControlItem10.Text = "Состояние";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(114, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.buttonEdit1;
            this.layoutControlItem11.Location = new System.Drawing.Point(391, 24);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(393, 27);
            this.layoutControlItem11.Text = "Дата смерти/выбытия";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(114, 13);
            // 
            // gc_main
            // 
            this.gc_main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_main.Controls.Add(this.layoutControl1);
            this.gc_main.Location = new System.Drawing.Point(3, 3);
            this.gc_main.Name = "gc_main";
            this.gc_main.ShowCaption = false;
            this.gc_main.Size = new System.Drawing.Size(812, 187);
            this.gc_main.TabIndex = 0;
            this.gc_main.Text = "groupControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.luAttachLPU);
            this.layoutControl1.Controls.Add(this.cmb_socStatus);
            this.layoutControl1.Controls.Add(this.btne_factPlace);
            this.layoutControl1.Controls.Add(this.btne_regPlace);
            this.layoutControl1.Controls.Add(this.cmb_gender);
            this.layoutControl1.Controls.Add(this.de_bornDate);
            this.layoutControl1.Controls.Add(this.te_fio);
            this.layoutControl1.Controls.Add(this.te_bornPlace);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(808, 183);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // luAttachLPU
            // 
            this.luAttachLPU.Location = new System.Drawing.Point(111, 36);
            this.luAttachLPU.Name = "luAttachLPU";
            this.luAttachLPU.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luAttachLPU.Properties.NullText = "";
            this.luAttachLPU.Size = new System.Drawing.Size(669, 20);
            this.luAttachLPU.StyleController = this.layoutControl1;
            this.luAttachLPU.TabIndex = 12;
            // 
            // cmb_socStatus
            // 
            this.cmb_socStatus.Location = new System.Drawing.Point(111, 156);
            this.cmb_socStatus.Name = "cmb_socStatus";
            this.cmb_socStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_socStatus.Size = new System.Drawing.Size(669, 20);
            this.cmb_socStatus.StyleController = this.layoutControl1;
            this.cmb_socStatus.TabIndex = 11;
            // 
            // btne_factPlace
            // 
            this.btne_factPlace.Location = new System.Drawing.Point(111, 132);
            this.btne_factPlace.Name = "btne_factPlace";
            this.btne_factPlace.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btne_factPlace.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btne_factPlace_Properties_ButtonClick);
            this.btne_factPlace.Size = new System.Drawing.Size(669, 20);
            this.btne_factPlace.StyleController = this.layoutControl1;
            this.btne_factPlace.TabIndex = 10;
            // 
            // btne_regPlace
            // 
            this.btne_regPlace.Location = new System.Drawing.Point(111, 108);
            this.btne_regPlace.Name = "btne_regPlace";
            this.btne_regPlace.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btne_regPlace.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btne_regPlace_Properties_ButtonClick);
            this.btne_regPlace.Size = new System.Drawing.Size(669, 20);
            this.btne_regPlace.StyleController = this.layoutControl1;
            this.btne_regPlace.TabIndex = 9;
            // 
            // cmb_gender
            // 
            this.cmb_gender.Location = new System.Drawing.Point(496, 60);
            this.cmb_gender.Name = "cmb_gender";
            this.cmb_gender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_gender.Size = new System.Drawing.Size(284, 20);
            this.cmb_gender.StyleController = this.layoutControl1;
            this.cmb_gender.TabIndex = 8;
            // 
            // de_bornDate
            // 
            this.de_bornDate.EditValue = null;
            this.de_bornDate.Location = new System.Drawing.Point(111, 60);
            this.de_bornDate.Name = "de_bornDate";
            this.de_bornDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_bornDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_bornDate.Size = new System.Drawing.Size(282, 20);
            this.de_bornDate.StyleController = this.layoutControl1;
            this.de_bornDate.TabIndex = 7;
            // 
            // te_fio
            // 
            this.te_fio.Location = new System.Drawing.Point(111, 12);
            this.te_fio.Name = "te_fio";
            this.te_fio.Size = new System.Drawing.Size(669, 20);
            this.te_fio.StyleController = this.layoutControl1;
            this.te_fio.TabIndex = 4;
            // 
            // te_bornPlace
            // 
            this.te_bornPlace.Location = new System.Drawing.Point(111, 84);
            this.te_bornPlace.Name = "te_bornPlace";
            this.te_bornPlace.Size = new System.Drawing.Size(669, 20);
            this.te_bornPlace.StyleController = this.layoutControl1;
            this.te_bornPlace.TabIndex = 9;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem36});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(792, 188);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_fio;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem1.Text = "ФИО";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.de_bornDate;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(385, 24);
            this.layoutControlItem4.Text = "Дата рождения";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmb_gender;
            this.layoutControlItem5.Location = new System.Drawing.Point(385, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(387, 24);
            this.layoutControlItem5.Text = "Пол";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btne_regPlace;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem2.Text = "Адрес прописки";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btne_factPlace;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem3.Text = "Адрес проживания";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.te_bornPlace;
            this.layoutControlItem6.CustomizationFormText = "Адрес прописки";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem6.Text = "Место рождения";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmb_socStatus;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem7.Text = "Соц. статус";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.luAttachLPU;
            this.layoutControlItem36.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem36.Text = "Прикреплен";
            this.layoutControlItem36.TextSize = new System.Drawing.Size(96, 13);
            // 
            // tabDocs
            // 
            this.tabDocs.Controls.Add(this.labelControl1);
            this.tabDocs.Controls.Add(this.textEdit1);
            this.tabDocs.Controls.Add(this.group_mse);
            this.tabDocs.Controls.Add(this.gc_policy);
            this.tabDocs.Controls.Add(this.gc_document);
            this.tabDocs.Name = "tabDocs";
            this.tabDocs.Size = new System.Drawing.Size(818, 531);
            this.tabDocs.Text = "Документы";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 256);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(35, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "СНИЛС";
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit1.Location = new System.Drawing.Point(83, 253);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(721, 20);
            this.textEdit1.TabIndex = 3;
            // 
            // group_mse
            // 
            this.group_mse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_mse.Controls.Add(this.layoutControl7);
            this.group_mse.Location = new System.Drawing.Point(5, 279);
            this.group_mse.Name = "group_mse";
            this.group_mse.Size = new System.Drawing.Size(813, 100);
            this.group_mse.TabIndex = 2;
            this.group_mse.Text = "МСЭ";
            this.group_mse.Visible = false;
            // 
            // layoutControl7
            // 
            this.layoutControl7.Controls.Add(this.dateEdit2);
            this.layoutControl7.Controls.Add(this.textEdit3);
            this.layoutControl7.Controls.Add(this.textEdit2);
            this.layoutControl7.Controls.Add(this.dateEdit1);
            this.layoutControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl7.Location = new System.Drawing.Point(2, 21);
            this.layoutControl7.Name = "layoutControl7";
            this.layoutControl7.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl7.Root = this.layoutControlGroup7;
            this.layoutControl7.Size = new System.Drawing.Size(809, 77);
            this.layoutControl7.TabIndex = 0;
            this.layoutControl7.Text = "layoutControl7";
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(517, 36);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Size = new System.Drawing.Size(280, 20);
            this.dateEdit2.StyleController = this.layoutControl7;
            this.dateEdit2.TabIndex = 7;
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(517, 12);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(280, 20);
            this.textEdit3.StyleController = this.layoutControl7;
            this.textEdit3.TabIndex = 5;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(96, 12);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(333, 20);
            this.textEdit2.StyleController = this.layoutControl7;
            this.textEdit2.TabIndex = 4;
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(96, 36);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Size = new System.Drawing.Size(333, 20);
            this.dateEdit1.StyleController = this.layoutControl7;
            this.dateEdit1.TabIndex = 6;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup7.GroupBordersVisible = false;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27,
            this.layoutControlItem28,
            this.layoutControlItem29,
            this.layoutControlItem30});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Size = new System.Drawing.Size(809, 77);
            this.layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.textEdit2;
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(421, 24);
            this.layoutControlItem27.Text = "Серия";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.textEdit3;
            this.layoutControlItem28.Location = new System.Drawing.Point(421, 0);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(368, 24);
            this.layoutControlItem28.Text = "Номер";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.dateEdit1;
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(421, 33);
            this.layoutControlItem29.Text = "Действителен с";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.dateEdit2;
            this.layoutControlItem30.Location = new System.Drawing.Point(421, 24);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(368, 33);
            this.layoutControlItem30.Text = "по";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(81, 13);
            // 
            // gc_policy
            // 
            this.gc_policy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_policy.Controls.Add(this.layoutControl4);
            this.gc_policy.Location = new System.Drawing.Point(3, 130);
            this.gc_policy.Name = "gc_policy";
            this.gc_policy.Size = new System.Drawing.Size(815, 120);
            this.gc_policy.TabIndex = 1;
            this.gc_policy.Text = "Полис";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.cmb_polSmo);
            this.layoutControl4.Controls.Add(this.cmb_polSmoRegion);
            this.layoutControl4.Controls.Add(this.te_polNumber);
            this.layoutControl4.Controls.Add(this.te_polSerial);
            this.layoutControl4.Controls.Add(this.de_polDateEnd);
            this.layoutControl4.Controls.Add(this.de_polDateBeg);
            this.layoutControl4.Controls.Add(this.cmb_polType);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(2, 21);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(811, 97);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // cmb_polSmo
            // 
            this.cmb_polSmo.Location = new System.Drawing.Point(410, 60);
            this.cmb_polSmo.Name = "cmb_polSmo";
            this.cmb_polSmo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_polSmo.Size = new System.Drawing.Size(389, 20);
            this.cmb_polSmo.StyleController = this.layoutControl4;
            this.cmb_polSmo.TabIndex = 11;
            // 
            // cmb_polSmoRegion
            // 
            this.cmb_polSmoRegion.Location = new System.Drawing.Point(78, 60);
            this.cmb_polSmoRegion.Name = "cmb_polSmoRegion";
            this.cmb_polSmoRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_polSmoRegion.Size = new System.Drawing.Size(262, 20);
            this.cmb_polSmoRegion.StyleController = this.layoutControl4;
            this.cmb_polSmoRegion.TabIndex = 10;
            this.cmb_polSmoRegion.SelectedValueChanged += new System.EventHandler(this.cmb_polSmoRegion_SelectedValueChanged);
            // 
            // te_polNumber
            // 
            this.te_polNumber.Location = new System.Drawing.Point(410, 36);
            this.te_polNumber.Name = "te_polNumber";
            this.te_polNumber.Size = new System.Drawing.Size(389, 20);
            this.te_polNumber.StyleController = this.layoutControl4;
            this.te_polNumber.TabIndex = 8;
            // 
            // te_polSerial
            // 
            this.te_polSerial.Location = new System.Drawing.Point(78, 36);
            this.te_polSerial.Name = "te_polSerial";
            this.te_polSerial.Size = new System.Drawing.Size(262, 20);
            this.te_polSerial.StyleController = this.layoutControl4;
            this.te_polSerial.TabIndex = 7;
            // 
            // de_polDateEnd
            // 
            this.de_polDateEnd.EditValue = null;
            this.de_polDateEnd.Location = new System.Drawing.Point(649, 12);
            this.de_polDateEnd.Name = "de_polDateEnd";
            this.de_polDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_polDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_polDateEnd.Size = new System.Drawing.Size(150, 20);
            this.de_polDateEnd.StyleController = this.layoutControl4;
            this.de_polDateEnd.TabIndex = 6;
            // 
            // de_polDateBeg
            // 
            this.de_polDateBeg.EditValue = null;
            this.de_polDateBeg.Location = new System.Drawing.Point(410, 12);
            this.de_polDateBeg.Name = "de_polDateBeg";
            this.de_polDateBeg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_polDateBeg.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_polDateBeg.Size = new System.Drawing.Size(169, 20);
            this.de_polDateBeg.StyleController = this.layoutControl4;
            this.de_polDateBeg.TabIndex = 5;
            // 
            // cmb_polType
            // 
            this.cmb_polType.Location = new System.Drawing.Point(78, 12);
            this.cmb_polType.Name = "cmb_polType";
            this.cmb_polType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_polType.Size = new System.Drawing.Size(262, 20);
            this.cmb_polType.StyleController = this.layoutControl4;
            this.cmb_polType.TabIndex = 4;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem23,
            this.layoutControlItem24});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(811, 97);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.cmb_polType;
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(332, 24);
            this.layoutControlItem17.Text = "Вид полиса";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.de_polDateBeg;
            this.layoutControlItem18.Location = new System.Drawing.Point(332, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(239, 24);
            this.layoutControlItem18.Text = "Действует с";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.de_polDateEnd;
            this.layoutControlItem19.Location = new System.Drawing.Point(571, 0);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(220, 24);
            this.layoutControlItem19.Text = "по";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.te_polSerial;
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(332, 24);
            this.layoutControlItem20.Text = "Серия";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.te_polNumber;
            this.layoutControlItem21.Location = new System.Drawing.Point(332, 24);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(459, 24);
            this.layoutControlItem21.Text = "Номер";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.cmb_polSmoRegion;
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(332, 29);
            this.layoutControlItem23.Text = "Регион";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.cmb_polSmo;
            this.layoutControlItem24.Location = new System.Drawing.Point(332, 48);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(459, 29);
            this.layoutControlItem24.Text = "СМО";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(63, 13);
            // 
            // gc_document
            // 
            this.gc_document.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_document.Controls.Add(this.layoutControl2);
            this.gc_document.Location = new System.Drawing.Point(3, 3);
            this.gc_document.Name = "gc_document";
            this.gc_document.Size = new System.Drawing.Size(812, 121);
            this.gc_document.TabIndex = 0;
            this.gc_document.Text = "Документ, удостоверяющий личность";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.te_docOrg);
            this.layoutControl2.Controls.Add(this.te_docNumber);
            this.layoutControl2.Controls.Add(this.te_docSerial);
            this.layoutControl2.Controls.Add(this.de_docDateBeg);
            this.layoutControl2.Controls.Add(this.cmb_docType);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 21);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(808, 98);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // te_docOrg
            // 
            this.te_docOrg.Location = new System.Drawing.Point(92, 60);
            this.te_docOrg.Name = "te_docOrg";
            this.te_docOrg.Size = new System.Drawing.Size(704, 20);
            this.te_docOrg.StyleController = this.layoutControl2;
            this.te_docOrg.TabIndex = 8;
            // 
            // te_docNumber
            // 
            this.te_docNumber.Location = new System.Drawing.Point(485, 36);
            this.te_docNumber.Name = "te_docNumber";
            this.te_docNumber.Size = new System.Drawing.Size(311, 20);
            this.te_docNumber.StyleController = this.layoutControl2;
            this.te_docNumber.TabIndex = 7;
            // 
            // te_docSerial
            // 
            this.te_docSerial.Location = new System.Drawing.Point(92, 36);
            this.te_docSerial.Name = "te_docSerial";
            this.te_docSerial.Size = new System.Drawing.Size(309, 20);
            this.te_docSerial.StyleController = this.layoutControl2;
            this.te_docSerial.TabIndex = 6;
            // 
            // de_docDateBeg
            // 
            this.de_docDateBeg.EditValue = null;
            this.de_docDateBeg.Location = new System.Drawing.Point(485, 12);
            this.de_docDateBeg.Name = "de_docDateBeg";
            this.de_docDateBeg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_docDateBeg.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_docDateBeg.Size = new System.Drawing.Size(311, 20);
            this.de_docDateBeg.StyleController = this.layoutControl2;
            this.de_docDateBeg.TabIndex = 5;
            // 
            // cmb_docType
            // 
            this.cmb_docType.Location = new System.Drawing.Point(92, 12);
            this.cmb_docType.Name = "cmb_docType";
            this.cmb_docType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_docType.Size = new System.Drawing.Size(309, 20);
            this.cmb_docType.StyleController = this.layoutControl2;
            this.cmb_docType.TabIndex = 4;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(808, 98);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.cmb_docType;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(393, 24);
            this.layoutControlItem12.Text = "Вид документа";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.de_docDateBeg;
            this.layoutControlItem13.Location = new System.Drawing.Point(393, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItem13.Text = "Дата выдачи";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.te_docSerial;
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(393, 24);
            this.layoutControlItem14.Text = "Серия";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.te_docNumber;
            this.layoutControlItem15.Location = new System.Drawing.Point(393, 24);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItem15.Text = "Номер";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.te_docOrg;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(788, 30);
            this.layoutControlItem16.Text = "Выдан";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(77, 13);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.layoutControl5);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(818, 531);
            this.xtraTabPage1.Text = "Контакты";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.groupControl1);
            this.layoutControl5.Controls.Add(this.textEdit6);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(818, 531);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl6);
            this.groupControl1.Location = new System.Drawing.Point(12, 36);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(794, 483);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Контактные телефоны";
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.phoneControl1);
            this.layoutControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl6.Location = new System.Drawing.Point(2, 21);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl6.Root = this.layoutControlGroup6;
            this.layoutControl6.Size = new System.Drawing.Size(790, 460);
            this.layoutControl6.TabIndex = 0;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // phoneControl1
            // 
            this.phoneControl1.Location = new System.Drawing.Point(12, 12);
            this.phoneControl1.Name = "phoneControl1";
            this.phoneControl1.Phones = null;
            this.phoneControl1.Size = new System.Drawing.Size(766, 436);
            this.phoneControl1.TabIndex = 4;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem22});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(790, 460);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.phoneControl1;
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(770, 440);
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextVisible = false;
            // 
            // textEdit6
            // 
            this.textEdit6.Location = new System.Drawing.Point(115, 12);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Size = new System.Drawing.Size(691, 20);
            this.textEdit6.StyleController = this.layoutControl5;
            this.textEdit6.TabIndex = 4;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem25,
            this.layoutControlItem26});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(818, 531);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.textEdit6;
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(798, 24);
            this.layoutControlItem25.Text = "Электронная почта";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.groupControl1;
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(798, 487);
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextVisible = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.tapControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(896, 537);
            this.xtraTabPage2.Text = "ТАП";
            // 
            // tapControl1
            // 
            this.tapControl1.ControlTemplate = null;
            this.tapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapControl1.Location = new System.Drawing.Point(0, 0);
            this.tapControl1.Name = "tapControl1";
            this.tapControl1.Patient = null;
            this.tapControl1.Size = new System.Drawing.Size(896, 537);
            this.tapControl1.TabIndex = 0;
            // 
            // tabLaboratory
            // 
            this.tabLaboratory.Controls.Add(this.labResearchControl1);
            this.tabLaboratory.Name = "tabLaboratory";
            this.tabLaboratory.Size = new System.Drawing.Size(896, 537);
            this.tabLaboratory.Text = "История исследований (Лаборатория)";
            // 
            // labResearchControl1
            // 
            this.labResearchControl1.CreateOrderAction = null;
            this.labResearchControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labResearchControl1.IsAddButtonVisible = true;
            this.labResearchControl1.Location = new System.Drawing.Point(0, 0);
            this.labResearchControl1.Name = "labResearchControl1";
            this.labResearchControl1.OpenOrderAction = null;
            this.labResearchControl1.Patient = null;
            this.labResearchControl1.Size = new System.Drawing.Size(896, 537);
            this.labResearchControl1.TabIndex = 0;
            // 
            // tabReferral
            // 
            this.tabReferral.Controls.Add(this.referralControl1);
            this.tabReferral.Name = "tabReferral";
            this.tabReferral.Size = new System.Drawing.Size(896, 537);
            this.tabReferral.Text = "Направления";
            // 
            // referralControl1
            // 
            this.referralControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.referralControl1.CreateReferralAction = null;
            this.referralControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.referralControl1.Location = new System.Drawing.Point(0, 0);
            this.referralControl1.Name = "referralControl1";
            this.referralControl1.Operator = null;
            this.referralControl1.Patient = null;
            this.referralControl1.Referrals = null;
            this.referralControl1.Size = new System.Drawing.Size(896, 537);
            this.referralControl1.TabIndex = 0;
            // 
            // tabVaccination
            // 
            this.tabVaccination.Controls.Add(this.layoutControl9);
            this.tabVaccination.Name = "tabVaccination";
            this.tabVaccination.Size = new System.Drawing.Size(896, 537);
            this.tabVaccination.Text = "Прививки";
            // 
            // layoutControl9
            // 
            this.layoutControl9.Controls.Add(this.vaccinationControl1);
            this.layoutControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl9.Location = new System.Drawing.Point(0, 0);
            this.layoutControl9.Name = "layoutControl9";
            this.layoutControl9.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl9.Root = this.layoutControlGroup9;
            this.layoutControl9.Size = new System.Drawing.Size(896, 537);
            this.layoutControl9.TabIndex = 0;
            this.layoutControl9.Text = "layoutControl9";
            // 
            // vaccinationControl1
            // 
            this.vaccinationControl1.Location = new System.Drawing.Point(3, 3);
            this.vaccinationControl1.LoggedUser = null;
            this.vaccinationControl1.Name = "vaccinationControl1";
            this.vaccinationControl1.Patient = null;
            this.vaccinationControl1.Size = new System.Drawing.Size(890, 531);
            this.vaccinationControl1.TabIndex = 4;
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup9.GroupBordersVisible = false;
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem35});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup9.Size = new System.Drawing.Size(896, 537);
            this.layoutControlGroup9.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.vaccinationControl1;
            this.layoutControlItem35.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem35.Size = new System.Drawing.Size(890, 531);
            this.layoutControlItem35.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem35.TextVisible = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.layoutControl11);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(896, 537);
            this.xtraTabPage3.Text = "Диспансерный учет";
            // 
            // layoutControl11
            // 
            this.layoutControl11.Controls.Add(this.dispenseryControl1);
            this.layoutControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl11.Location = new System.Drawing.Point(0, 0);
            this.layoutControl11.Name = "layoutControl11";
            this.layoutControl11.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl11.Root = this.layoutControlGroup11;
            this.layoutControl11.Size = new System.Drawing.Size(896, 537);
            this.layoutControl11.TabIndex = 0;
            this.layoutControl11.Text = "layoutControl11";
            // 
            // dispenseryControl1
            // 
            this.dispenseryControl1.Location = new System.Drawing.Point(12, 12);
            this.dispenseryControl1.LoggedUser = null;
            this.dispenseryControl1.Name = "dispenseryControl1";
            this.dispenseryControl1.Patient = null;
            this.dispenseryControl1.Size = new System.Drawing.Size(872, 513);
            this.dispenseryControl1.TabIndex = 4;
            // 
            // layoutControlGroup11
            // 
            this.layoutControlGroup11.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup11.GroupBordersVisible = false;
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem40});
            this.layoutControlGroup11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.Size = new System.Drawing.Size(896, 537);
            this.layoutControlGroup11.TextVisible = false;
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.dispenseryControl1;
            this.layoutControlItem40.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Size = new System.Drawing.Size(876, 517);
            this.layoutControlItem40.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem40.TextVisible = false;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.layoutControl12);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(896, 537);
            this.xtraTabPage4.Text = "Больничный лист";
            // 
            // layoutControl12
            // 
            this.layoutControl12.Controls.Add(this.seakLeaveControl1);
            this.layoutControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl12.Location = new System.Drawing.Point(0, 0);
            this.layoutControl12.Name = "layoutControl12";
            this.layoutControl12.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl12.Root = this.layoutControlGroup12;
            this.layoutControl12.Size = new System.Drawing.Size(896, 537);
            this.layoutControl12.TabIndex = 0;
            this.layoutControl12.Text = "layoutControl12";
            // 
            // seakLeaveControl1
            // 
            this.seakLeaveControl1.Location = new System.Drawing.Point(12, 12);
            this.seakLeaveControl1.Name = "seakLeaveControl1";
            this.seakLeaveControl1.Operator = null;
            this.seakLeaveControl1.Patient = null;
            this.seakLeaveControl1.Size = new System.Drawing.Size(872, 513);
            this.seakLeaveControl1.TabIndex = 4;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup12.GroupBordersVisible = false;
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem41});
            this.layoutControlGroup12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.Size = new System.Drawing.Size(896, 537);
            this.layoutControlGroup12.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.seakLeaveControl1;
            this.layoutControlItem41.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Size = new System.Drawing.Size(876, 517);
            this.layoutControlItem41.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem41.TextVisible = false;
            // 
            // DopDispPage
            // 
            this.DopDispPage.Controls.Add(this.layoutControl13);
            this.DopDispPage.Name = "DopDispPage";
            this.DopDispPage.Size = new System.Drawing.Size(896, 537);
            this.DopDispPage.Text = "Диспансериазция";
            // 
            // layoutControl13
            // 
            this.layoutControl13.Controls.Add(this.dopDispControl1);
            this.layoutControl13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl13.Location = new System.Drawing.Point(0, 0);
            this.layoutControl13.Name = "layoutControl13";
            this.layoutControl13.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl13.Root = this.layoutControlGroup13;
            this.layoutControl13.Size = new System.Drawing.Size(896, 537);
            this.layoutControl13.TabIndex = 0;
            this.layoutControl13.Text = "layoutControl13";
            // 
            // dopDispControl1
            // 
            this.dopDispControl1.Location = new System.Drawing.Point(12, 12);
            this.dopDispControl1.LoggedUser = null;
            this.dopDispControl1.Margin = new System.Windows.Forms.Padding(0);
            this.dopDispControl1.Name = "dopDispControl1";
            this.dopDispControl1.Patient = null;
            this.dopDispControl1.SelectedCard = null;
            this.dopDispControl1.Size = new System.Drawing.Size(872, 513);
            this.dopDispControl1.TabIndex = 4;
            // 
            // layoutControlGroup13
            // 
            this.layoutControlGroup13.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup13.GroupBordersVisible = false;
            this.layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem42});
            this.layoutControlGroup13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup13.Name = "layoutControlGroup13";
            this.layoutControlGroup13.Size = new System.Drawing.Size(896, 537);
            this.layoutControlGroup13.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            this.layoutControlItem42.Control = this.dopDispControl1;
            this.layoutControlItem42.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem42.Name = "layoutControlItem42";
            this.layoutControlItem42.Size = new System.Drawing.Size(876, 517);
            this.layoutControlItem42.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem42.TextVisible = false;
            // 
            // ARMControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tcDisp);
            this.Name = "ARMControl";
            this.Size = new System.Drawing.Size(902, 565);
            ((System.ComponentModel.ISupportInitialize)(this.tcDisp)).EndInit();
            this.tcDisp.ResumeLayout(false);
            this.PatientTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabData)).EndInit();
            this.tabData.ResumeLayout(false);
            this.tabCommon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).EndInit();
            this.layoutControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_lgotas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.group_fluo)).EndInit();
            this.group_fluo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).EndInit();
            this.layoutControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_fluo_num.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_risk_group.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_fluo_date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_fluo_place.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_med)).EndInit();
            this.gc_med.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_dopUchastok.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_uchastok.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_main)).EndInit();
            this.gc_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luAttachLPU.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_socStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btne_factPlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btne_regPlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_gender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_bornDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_bornDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_fio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_bornPlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            this.tabDocs.ResumeLayout(false);
            this.tabDocs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.group_mse)).EndInit();
            this.group_mse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).EndInit();
            this.layoutControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_policy)).EndInit();
            this.gc_policy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmb_polSmo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_polSmoRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_polNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_polSerial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateBeg.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_polDateBeg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_polType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_document)).EndInit();
            this.gc_document.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_docOrg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_docNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_docSerial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_docDateBeg.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_docDateBeg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_docType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.tabLaboratory.ResumeLayout(false);
            this.tabReferral.ResumeLayout(false);
            this.tabVaccination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).EndInit();
            this.layoutControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).EndInit();
            this.layoutControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl12)).EndInit();
            this.layoutControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            this.DopDispPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl13)).EndInit();
            this.layoutControl13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tcDisp;
        private DevExpress.XtraTab.XtraTabPage PatientTab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabControl tabData;
        private DevExpress.XtraTab.XtraTabPage tabCommon;
        private DevExpress.XtraTab.XtraTabPage tabDocs;
        private DevExpress.XtraEditors.GroupControl gc_main;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_gender;
        private DevExpress.XtraEditors.DateEdit de_bornDate;
        private DevExpress.XtraEditors.TextEdit te_fio;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.ButtonEdit btne_factPlace;
        private DevExpress.XtraEditors.ButtonEdit btne_regPlace;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.GroupControl gc_med;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_socStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit3;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_dopUchastok;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_uchastok;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.GroupControl gc_policy;
        private DevExpress.XtraEditors.GroupControl gc_document;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_polSmo;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_polSmoRegion;
        private DevExpress.XtraEditors.TextEdit te_polNumber;
        private DevExpress.XtraEditors.TextEdit te_polSerial;
        private DevExpress.XtraEditors.DateEdit de_polDateEnd;
        private DevExpress.XtraEditors.DateEdit de_polDateBeg;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_polType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.TextEdit te_docOrg;
        private DevExpress.XtraEditors.TextEdit te_docNumber;
        private DevExpress.XtraEditors.TextEdit te_docSerial;
        private DevExpress.XtraEditors.DateEdit de_docDateBeg;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_docType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraEditors.TextEdit textEdit6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraEditors.TextEdit te_bornPlace;
        private SharedUtils.FormsAndControls.PhoneControl phoneControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private TAPControl tapControl1;
        private DevExpress.XtraEditors.GroupControl group_fluo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.GroupControl group_mse;
        private DevExpress.XtraLayout.LayoutControl layoutControl7;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraTab.XtraTabPage tabLaboratory;
        private LabResearchControl labResearchControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl8;
        private DevExpress.XtraEditors.TextEdit te_fluo_num;
        private DevExpress.XtraEditors.TextEdit te_risk_group;
        private DevExpress.XtraEditors.TextEdit te_fluo_date;
        private DevExpress.XtraEditors.TextEdit te_fluo_place;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraGrid.GridControl grid_lgotas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage tabReferral;
        private ReferralControl referralControl1;
        private DevExpress.XtraTab.XtraTabPage tabVaccination;
        private DevExpress.XtraLayout.LayoutControl layoutControl9;
        private VaccinationControl vaccinationControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraEditors.LookUpEdit luAttachLPU;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControl layoutControl10;
        private DevExpress.XtraEditors.SimpleButton btnDeleteLgota;
        private DevExpress.XtraEditors.SimpleButton btnAddLgota;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraEditors.SimpleButton btnChange;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraLayout.LayoutControl layoutControl11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DispenseryControl dispenseryControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraLayout.LayoutControl layoutControl12;
        private SeakLeaveControl seakLeaveControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraTab.XtraTabPage DopDispPage;
        private DevExpress.XtraLayout.LayoutControl layoutControl13;
        private DopDispControl dopDispControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
    }
}
