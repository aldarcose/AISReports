namespace ARMPlugin.FormsAndControls
{
    partial class AddOrderForm
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
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn_name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn_id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btn_add = new DevExpress.XtraEditors.SimpleButton();
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAdress = new System.Windows.Forms.Label();
            this.mkbBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mkbSearchControl = new SharedUtils.FormsAndControls.MkbSearchControl();
            this.lblDopUchastok = new System.Windows.Forms.Label();
            this.chVoenkomat = new DevExpress.XtraEditors.CheckEdit();
            this.cmb_trimestr = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lueLPU = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mkbBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chVoenkomat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_trimestr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLPU.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn_name,
            this.treeListColumn_id});
            this.treeList1.Location = new System.Drawing.Point(15, 141);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.ReadOnly = true;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.Size = new System.Drawing.Size(587, 209);
            this.treeList1.TabIndex = 2;
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCheckNode);
            // 
            // treeListColumn_name
            // 
            this.treeListColumn_name.Caption = "Имя";
            this.treeListColumn_name.FieldName = "Имя";
            this.treeListColumn_name.MinWidth = 32;
            this.treeListColumn_name.Name = "treeListColumn_name";
            this.treeListColumn_name.Visible = true;
            this.treeListColumn_name.VisibleIndex = 0;
            // 
            // treeListColumn_id
            // 
            this.treeListColumn_id.Caption = "Id";
            this.treeListColumn_id.FieldName = "Id";
            this.treeListColumn_id.Name = "treeListColumn_id";
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.Location = new System.Drawing.Point(446, 356);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 3;
            this.btn_add.Text = "Заказать";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(527, 356);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "Отмена";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Выберите необходимые исследования:";
            // 
            // lblAdress
            // 
            this.lblAdress.AutoSize = true;
            this.lblAdress.Location = new System.Drawing.Point(12, 56);
            this.lblAdress.Name = "lblAdress";
            this.lblAdress.Size = new System.Drawing.Size(45, 13);
            this.lblAdress.TabIndex = 8;
            this.lblAdress.Text = "Адрес: ";
            // 
            // mkbBindingSource
            // 
            this.mkbBindingSource.DataSource = typeof(Model.Classes.Codifiers.Mkb);
            // 
            // mkbSearchControl
            // 
            this.mkbSearchControl.Location = new System.Drawing.Point(14, 31);
            this.mkbSearchControl.Name = "mkbSearchControl";
            this.mkbSearchControl.Size = new System.Drawing.Size(590, 25);
            this.mkbSearchControl.TabIndex = 9;
            // 
            // lblDopUchastok
            // 
            this.lblDopUchastok.AutoSize = true;
            this.lblDopUchastok.Location = new System.Drawing.Point(11, 74);
            this.lblDopUchastok.Name = "lblDopUchastok";
            this.lblDopUchastok.Size = new System.Drawing.Size(79, 13);
            this.lblDopUchastok.TabIndex = 10;
            this.lblDopUchastok.Text = "Доп. участок:";
            // 
            // chVoenkomat
            // 
            this.chVoenkomat.Location = new System.Drawing.Point(15, 92);
            this.chVoenkomat.Name = "chVoenkomat";
            this.chVoenkomat.Properties.Caption = "Военкомат";
            this.chVoenkomat.Size = new System.Drawing.Size(75, 19);
            this.chVoenkomat.TabIndex = 11;
            this.chVoenkomat.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // cmb_trimestr
            // 
            this.cmb_trimestr.Location = new System.Drawing.Point(149, 91);
            this.cmb_trimestr.Name = "cmb_trimestr";
            this.cmb_trimestr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_trimestr.Size = new System.Drawing.Size(53, 20);
            this.cmb_trimestr.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(96, 95);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Триместр";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Направившее ЛПУ:";
            // 
            // lueLPU
            // 
            this.lueLPU.Location = new System.Drawing.Point(116, 6);
            this.lueLPU.Name = "lueLPU";
            this.lueLPU.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLPU.Properties.NullText = "";
            this.lueLPU.Size = new System.Drawing.Size(486, 20);
            this.lueLPU.TabIndex = 15;
            // 
            // AddOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(614, 391);
            this.Controls.Add(this.lueLPU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmb_trimestr);
            this.Controls.Add(this.chVoenkomat);
            this.Controls.Add(this.lblDopUchastok);
            this.Controls.Add(this.mkbSearchControl);
            this.Controls.Add(this.lblAdress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.treeList1);
            this.Name = "AddOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить запрос";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mkbBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chVoenkomat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_trimestr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLPU.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.SimpleButton btn_add;
        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_id;
        private System.Windows.Forms.Label lblAdress;
        private System.Windows.Forms.BindingSource mkbBindingSource;
        private SharedUtils.FormsAndControls.MkbSearchControl mkbSearchControl;
        private System.Windows.Forms.Label lblDopUchastok;
        private DevExpress.XtraEditors.CheckEdit chVoenkomat;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_trimestr;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lueLPU;
    }
}