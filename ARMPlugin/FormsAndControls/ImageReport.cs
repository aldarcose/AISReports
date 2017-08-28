using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class ImageReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ImageReport(ImageList list)
        {
            InitializeComponent();

            objectDataSource1.DataSource = list;
        }

    }
}
