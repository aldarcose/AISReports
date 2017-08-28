using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ARMPlugin.FormsAndControls
{
    public partial class NprReport : DevExpress.XtraReports.UI.XtraReport
    {
        public NprReport(NprData d)
        {
            InitializeComponent();

            lbl_lpu.Text = d.Lpu;
        }

    }
}
