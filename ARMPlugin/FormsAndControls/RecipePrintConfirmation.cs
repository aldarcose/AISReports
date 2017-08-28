using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ARMPlugin.FormsAndControls
{
    public partial class RecipePrintConfirmation : XtraForm
    {
        public RecipePrintConfirmation()
        {
            InitializeComponent();
        }

        public int Count
        {
            get
            {
                int s = 2;
                try
                {
                    s = (int)spinEdit1.Value;
                }
                catch (Exception ex)
                {
                    return 2;
                }
                return s;
            }
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
