using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Reports.Controls
{
    public partial class ParametersForm : Form, IReportParametersForm
    {
        private ParametersView parameters;
        private Report report;

        public ParametersForm()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            parameters = new ParametersView();
            panelParameters.Controls.Add(parameters);
            parameters.Dock = DockStyle.Fill;
            // Подписка на событие
            parameters.HeightChanged += parameters_HeightChanged;
        }

        public event EventHandler<ParametersValuesEventArgs> OK;

        public ReportParameterCollection Value
        {
            get { return parameters.Value; }
            set { parameters.Value = value; }
        }

        public Report Report
        {
            get { return report; }
            set { report = value; }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var parsValues = parameters.GetParametersValues();
            if (OK != null)
                OK(this, new ParametersValuesEventArgs(parsValues));
            DialogResult = DialogResult.OK;
        }

        void parameters_HeightChanged(object sender, AdditionalDataEditEventArgs e)
        {
            Height += e.Dy;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
