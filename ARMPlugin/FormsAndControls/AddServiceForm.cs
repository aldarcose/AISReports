using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model.Classes.Codifiers;

namespace ARMPlugin.FormsAndControls
{
    public partial class AddServiceForm : XtraForm
    {
        private List<Mkb> _mkbs = null;
        public AddServiceForm()
        {
            InitializeComponent();
            _mkbs = CodifiersHelper.GetMkbs();
        }

        private void cmb_diagnose_TextChanged(object sender, EventArgs e)
        {
            var filter = cmb_diagnose.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                var normalFilter = filter.ToLower();
                cmb_diagnose.Properties.Items.Clear();
                cmb_diagnose.Properties.Items.AddRange(_mkbs.Where(t => t.Code.ToLower().StartsWith(normalFilter) || t.Name.ToLower().StartsWith(filter)).Select(t => string.Format("{0} {1}", t.Code, t.Name)).ToList());
            }
        }

        public int Count
        {
            get { return (int)spin_count.Value; }
        }

        public Mkb SelectedMkb
        {
            get
            {
                var text = cmb_diagnose.Text;

                if (string.IsNullOrEmpty(text))
                    return null;

                var mkb = _mkbs.FirstOrDefault(t => string.Format("{0} {1}", t.Code, t.Name).Equals(text));
                return mkb;
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (Count > 0 && SelectedMkb != null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
