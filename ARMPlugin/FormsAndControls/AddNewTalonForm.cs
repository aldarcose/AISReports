using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model.Classes.Codifiers;

namespace ARMPlugin.FormsAndControls
{
    public partial class AddNewTalonForm : XtraForm
    {
        private List<VisitPurpose> _purposes = null;
        private List<PaymentType> _paymentTypes = null;

        public DateTime VisitDate { get; set; }
        public VisitPurpose Purpose { get; set; }

        public PaymentType PaymentType { get; set; }

        public AddNewTalonForm()
        {
            InitializeComponent();

            var defaultPurposeId = 1; // ИД Лечебно-профилактическая
            VisitDate = DateTime.Now;
            _purposes = CodifiersHelper.GetVisitPurposes();
            Purpose = _purposes.FirstOrDefault(t => t.Id == defaultPurposeId);

            var defaultPaymentTypeId = 0; // ОМС
            _paymentTypes = CodifiersHelper.GetPaymentTypes();
            this.PaymentType = _paymentTypes.FirstOrDefault(t => t.Id == defaultPaymentTypeId);


            de_visitdate.DateTime = VisitDate;
            cmb_target.Properties.Items.AddRange(_purposes.Select(t=>t.Name).ToList());
            cmb_payment_type.Properties.Items.AddRange(_paymentTypes.Select(t=>t.Name).ToList());

            if (Purpose!=null)
                cmb_target.Text = Purpose.Name;
        }

        private void de_visitdate_DateTimeChanged(object sender, EventArgs e)
        {
            VisitDate = de_visitdate.DateTime;
        }

        private void cmb_target_SelectedValueChanged(object sender, EventArgs e)
        {
            var name = cmb_target.Text;
            Purpose = _purposes.FirstOrDefault(t => t.Name.Equals(name));
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (Purpose != null && PaymentType!=null)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("Укажите все данные формы", "Внимание", MessageBoxButtons.OK);
            }
        }

        private void cmb_payment_type_SelectedValueChanged(object sender, EventArgs e)
        {
            var name = cmb_payment_type.Text;
            PaymentType = _paymentTypes.FirstOrDefault(t => t.Name.Equals(name));
        }
    }
}
