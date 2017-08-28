using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace ARMPlugin.FormsAndControls
{
    public partial class PrintNprForm : XtraForm
    {
        public PrintNprForm()
        {
            InitializeComponent();

            lookUpEdit1.Properties.DataSource = Model.Classes.CodifiersHelper.GetMOs();
            lookUpEdit1.Properties.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NprData d = new NprData();

            d.Lpu = textEdit1.Text;
            d.Patient = textEdit2.Text;

            var report = new NprReport(d);
            report.ShowPreview();
        }
    }


    public class NprData
    {
        public string Lpu { get; set; }
        public string Patient { get; set; }
    }
}
