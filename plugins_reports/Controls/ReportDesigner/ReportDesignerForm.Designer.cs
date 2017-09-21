namespace Reports.Controls
{
    partial class ReportDesignerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.closeButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.parametersTreeView = new System.Windows.Forms.TreeView();
            this.parametersGridView = new System.Windows.Forms.DataGridView();
            this.parametersContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteParameterRow = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.showQueryButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.closeButton2 = new System.Windows.Forms.Button();
            this.fieldsTreeView = new System.Windows.Forms.TreeView();
            this.fieldsGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parametersGridView)).BeginInit();
            this.parametersContextMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1022, 494);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Условия выборки";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.parametersGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 475);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.closeButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nextButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.parametersTreeView, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(383, 475);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeButton.Location = new System.Drawing.Point(3, 437);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(377, 35);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton2_Click);
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Location = new System.Drawing.Point(3, 397);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(377, 34);
            this.nextButton.TabIndex = 0;
            this.nextButton.Text = "Далее >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // parametersTreeView
            // 
            this.parametersTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parametersTreeView.Location = new System.Drawing.Point(3, 3);
            this.parametersTreeView.Name = "parametersTreeView";
            this.parametersTreeView.Size = new System.Drawing.Size(377, 388);
            this.parametersTreeView.TabIndex = 0;
            this.parametersTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.parametersTreeView_NodeMouseDoubleClick);
            // 
            // parametersGridView
            // 
            this.parametersGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.parametersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parametersGridView.ContextMenuStrip = this.parametersContextMenuStrip;
            this.parametersGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parametersGridView.Location = new System.Drawing.Point(0, 0);
            this.parametersGridView.Name = "parametersGridView";
            this.parametersGridView.Size = new System.Drawing.Size(629, 475);
            this.parametersGridView.TabIndex = 0;
            this.parametersGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.parametersGridView_DataBindingComplete);
            this.parametersGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.parametersGridView_MouseDown);
            // 
            // parametersContextMenuStrip
            // 
            this.parametersContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteParameterRow});
            this.parametersContextMenuStrip.Name = "contextMenuStrip1";
            this.parametersContextMenuStrip.Size = new System.Drawing.Size(119, 26);
            // 
            // DeleteParameterRow
            // 
            this.DeleteParameterRow.Name = "DeleteParameterRow";
            this.DeleteParameterRow.Size = new System.Drawing.Size(152, 22);
            this.DeleteParameterRow.Text = "Удалить";
            this.DeleteParameterRow.Click += new System.EventHandler(this.DeleteParameterRow_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1022, 494);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поля в результирущей выборке";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.fieldsGridView);
            this.splitContainer2.Size = new System.Drawing.Size(1016, 475);
            this.splitContainer2.SplitterDistance = 329;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.showQueryButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.previousButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.buttonOK, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.closeButton2, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.fieldsTreeView, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(329, 475);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // showQueryButton
            // 
            this.showQueryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showQueryButton.Location = new System.Drawing.Point(3, 314);
            this.showQueryButton.Name = "showQueryButton";
            this.showQueryButton.Size = new System.Drawing.Size(323, 35);
            this.showQueryButton.TabIndex = 0;
            this.showQueryButton.Text = "Запрос";
            this.showQueryButton.UseVisualStyleBackColor = true;
            // 
            // previousButton
            // 
            this.previousButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previousButton.Location = new System.Drawing.Point(3, 355);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(323, 35);
            this.previousButton.TabIndex = 1;
            this.previousButton.Text = "<< Назад";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Location = new System.Drawing.Point(3, 396);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(323, 35);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Создать отчет";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // closeButton2
            // 
            this.closeButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeButton2.Location = new System.Drawing.Point(3, 437);
            this.closeButton2.Name = "closeButton2";
            this.closeButton2.Size = new System.Drawing.Size(323, 35);
            this.closeButton2.TabIndex = 3;
            this.closeButton2.Text = "Закрыть";
            this.closeButton2.UseVisualStyleBackColor = true;
            this.closeButton2.Click += new System.EventHandler(this.closeButton2_Click);
            // 
            // fieldsTreeView
            // 
            this.fieldsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsTreeView.Location = new System.Drawing.Point(3, 3);
            this.fieldsTreeView.Name = "fieldsTreeView";
            this.fieldsTreeView.Size = new System.Drawing.Size(323, 305);
            this.fieldsTreeView.TabIndex = 4;
            // 
            // fieldsGridView
            // 
            this.fieldsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fieldsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsGridView.Location = new System.Drawing.Point(0, 0);
            this.fieldsGridView.Name = "fieldsGridView";
            this.fieldsGridView.Size = new System.Drawing.Size(683, 475);
            this.fieldsGridView.TabIndex = 0;
            this.fieldsGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fieldsGridView_MouseDown);
            // 
            // ReportDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 494);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportDesignerForm";
            this.Text = "Параметры";
            this.Load += new System.EventHandler(this.ReportDesignerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.parametersGridView)).EndInit();
            this.parametersContextMenuStrip.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fieldsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView parametersGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.TreeView parametersTreeView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button showQueryButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button closeButton2;
        private System.Windows.Forms.TreeView fieldsTreeView;
        private System.Windows.Forms.DataGridView fieldsGridView;
        private System.Windows.Forms.ContextMenuStrip parametersContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteParameterRow;
    }
}

