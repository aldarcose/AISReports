namespace ARMPlugin.FormsAndControls
{
    partial class DoctorServiceControl
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
            this.btn_service = new DevExpress.XtraEditors.SimpleButton();
            this.grid_services = new DevExpress.XtraGrid.GridControl();
            this.grid_services_view = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repo_service_btn = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_services)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_services_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_service_btn)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_service
            // 
            this.btn_service.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_service.Location = new System.Drawing.Point(3, 305);
            this.btn_service.Name = "btn_service";
            this.btn_service.Size = new System.Drawing.Size(675, 23);
            this.btn_service.TabIndex = 0;
            this.btn_service.Text = "Оказать";
            this.btn_service.Click += new System.EventHandler(this.btn_service_Click);
            // 
            // grid_services
            // 
            this.grid_services.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_services.Location = new System.Drawing.Point(3, 3);
            this.grid_services.MainView = this.grid_services_view;
            this.grid_services.Name = "grid_services";
            this.grid_services.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_service_btn});
            this.grid_services.Size = new System.Drawing.Size(675, 296);
            this.grid_services.TabIndex = 1;
            this.grid_services.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grid_services_view});
            // 
            // grid_services_view
            // 
            this.grid_services_view.GridControl = this.grid_services;
            this.grid_services_view.Name = "grid_services_view";
            this.grid_services_view.OptionsBehavior.Editable = false;
            this.grid_services_view.OptionsBehavior.ReadOnly = true;
            this.grid_services_view.OptionsView.ShowGroupPanel = false;
            this.grid_services_view.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.grid_services_view_RowClick);
            // 
            // repo_service_btn
            // 
            this.repo_service_btn.AutoHeight = false;
            this.repo_service_btn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.repo_service_btn.Name = "repo_service_btn";
            this.repo_service_btn.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repo_service_btn.Click += new System.EventHandler(this.repo_service_btn_Click);
            // 
            // DoctorServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid_services);
            this.Controls.Add(this.btn_service);
            this.Name = "DoctorServiceControl";
            this.Size = new System.Drawing.Size(681, 331);
            ((System.ComponentModel.ISupportInitialize)(this.grid_services)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_services_view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_service_btn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_service;
        private DevExpress.XtraGrid.GridControl grid_services;
        private DevExpress.XtraGrid.Views.Grid.GridView grid_services_view;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repo_service_btn;
    }
}
