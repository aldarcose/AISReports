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
            this.fieldsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteFieldRow = new System.Windows.Forms.ToolStripMenuItem();
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
            this.fieldsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1363, 608);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Условия выборки";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 19);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.parametersGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1355, 585);
            this.splitContainer1.SplitterDistance = 510;
            this.splitContainer1.SplitterWidth = 5;
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 585);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeButton.Location = new System.Drawing.Point(4, 538);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(502, 43);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton2_Click);
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Location = new System.Drawing.Point(4, 488);
            this.nextButton.Margin = new System.Windows.Forms.Padding(4);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(502, 42);
            this.nextButton.TabIndex = 0;
            this.nextButton.Text = "Далее >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // parametersTreeView
            // 
            this.parametersTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parametersTreeView.Location = new System.Drawing.Point(4, 4);
            this.parametersTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.parametersTreeView.Name = "parametersTreeView";
            this.parametersTreeView.Size = new System.Drawing.Size(502, 476);
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
            this.parametersGridView.Margin = new System.Windows.Forms.Padding(4);
            this.parametersGridView.Name = "parametersGridView";
            this.parametersGridView.Size = new System.Drawing.Size(840, 585);
            this.parametersGridView.TabIndex = 0;
            this.parametersGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.parametersGridView_DataBindingComplete);
            this.parametersGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.parametersGridView_MouseDown);
            // 
            // parametersContextMenuStrip
            // 
            this.parametersContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteParameterRow});
            this.parametersContextMenuStrip.Name = "contextMenuStrip1";
            this.parametersContextMenuStrip.Size = new System.Drawing.Size(207, 28);
            // 
            // DeleteParameterRow
            // 
            this.DeleteParameterRow.Name = "DeleteParameterRow";
            this.DeleteParameterRow.Size = new System.Drawing.Size(206, 24);
            this.DeleteParameterRow.Text = "Удалить параметр";
            this.DeleteParameterRow.Click += new System.EventHandler(this.DeleteParameterRow_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1363, 608);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поля в результирущей выборке";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(4, 19);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.fieldsGridView);
            this.splitContainer2.Size = new System.Drawing.Size(1355, 585);
            this.splitContainer2.SplitterDistance = 438;
            this.splitContainer2.SplitterWidth = 5;
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
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(438, 585);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // showQueryButton
            // 
            this.showQueryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showQueryButton.Location = new System.Drawing.Point(4, 385);
            this.showQueryButton.Margin = new System.Windows.Forms.Padding(4);
            this.showQueryButton.Name = "showQueryButton";
            this.showQueryButton.Size = new System.Drawing.Size(430, 43);
            this.showQueryButton.TabIndex = 0;
            this.showQueryButton.Text = "Запрос";
            this.showQueryButton.UseVisualStyleBackColor = true;
            this.showQueryButton.Click += new System.EventHandler(this.showQueryButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previousButton.Location = new System.Drawing.Point(4, 436);
            this.previousButton.Margin = new System.Windows.Forms.Padding(4);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(430, 43);
            this.previousButton.TabIndex = 1;
            this.previousButton.Text = "<< Назад";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Location = new System.Drawing.Point(4, 487);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(430, 43);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Создать отчет";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // closeButton2
            // 
            this.closeButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeButton2.Location = new System.Drawing.Point(4, 538);
            this.closeButton2.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton2.Name = "closeButton2";
            this.closeButton2.Size = new System.Drawing.Size(430, 43);
            this.closeButton2.TabIndex = 3;
            this.closeButton2.Text = "Закрыть";
            this.closeButton2.UseVisualStyleBackColor = true;
            this.closeButton2.Click += new System.EventHandler(this.closeButton2_Click);
            // 
            // fieldsTreeView
            // 
            this.fieldsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsTreeView.Location = new System.Drawing.Point(4, 4);
            this.fieldsTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.fieldsTreeView.Name = "fieldsTreeView";
            this.fieldsTreeView.Size = new System.Drawing.Size(430, 373);
            this.fieldsTreeView.TabIndex = 4;
            this.fieldsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fieldsTreeView_NodeMouseDoubleClick);
            // 
            // fieldsGridView
            // 
            this.fieldsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fieldsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldsGridView.ContextMenuStrip = this.fieldsContextMenuStrip;
            this.fieldsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsGridView.Location = new System.Drawing.Point(0, 0);
            this.fieldsGridView.Margin = new System.Windows.Forms.Padding(4);
            this.fieldsGridView.Name = "fieldsGridView";
            this.fieldsGridView.Size = new System.Drawing.Size(912, 585);
            this.fieldsGridView.TabIndex = 0;
            this.fieldsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.parametersGridView_DataBindingComplete);
            this.fieldsGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fieldsGridView_MouseDown);
            // 
            // fieldsContextMenuStrip
            // 
            this.fieldsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteFieldRow});
            this.fieldsContextMenuStrip.Name = "fieldsContextMenuStrip";
            this.fieldsContextMenuStrip.Size = new System.Drawing.Size(173, 28);
            // 
            // DeleteFieldRow
            // 
            this.DeleteFieldRow.Name = "DeleteFieldRow";
            this.DeleteFieldRow.Size = new System.Drawing.Size(172, 24);
            this.DeleteFieldRow.Text = "Удалить поле";
            this.DeleteFieldRow.Click += new System.EventHandler(this.DeleteFieldRow_Click);
            // 
            // ReportDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 608);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.fieldsContextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip fieldsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteFieldRow;
    }
}

