using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes.Reestr;
using System.IO;


namespace ARMPlugin.FormsAndControls
{
    public partial class ReestForm : DevExpress.XtraEditors.XtraForm
    {
        private ReestrTypes reestrType;

        public ReestForm(ReestrTypes reestrType)
        {
            InitializeComponent();
            this.reestrType = reestrType;
        }

        private void SaveXML_Click(object sender, EventArgs e)
        {
            var startDate = deStart.EditValue;
            var endDate = deEnd.EditValue;
            string folder=null;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result==DialogResult.OK)
            {
                folder = folderBrowserDialog1.SelectedPath;
            }
            if (startDate!=null && endDate!=null)
            {
                if (reestrType == ReestrTypes.Disp)
                {
                    var disp = new Disp((DateTime)startDate, (DateTime)endDate);
                    disp.InitFiles();
                    disp.Save(folder);
                }else if (reestrType == ReestrTypes.Service)
                {
                    var usl = new Services((DateTime)startDate, (DateTime)endDate);
                    usl.InitFiles();
                    usl.Save(folder);
                }else
                {
                    var usl = new Nonresident((DateTime)startDate, (DateTime)endDate);
                    usl.InitFiles();
                    usl.Save(folder);
                }
                
                XtraMessageBox.Show("Файлы сохранены","Реестр",MessageBoxButtons.OK);
            }
            
        }

        
    }
}