using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Blending;
using DevExpress.XtraReports.UI;
using Model;
using Model.Classes;
using Model.Classes.Referral;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class ReferralControl : UserControl
    {
        public ReferralControl()
        {
            InitializeComponent();
            //IsAddButtonVisible = true;
            btn_preview.Enabled = false;
            grid_RefView.FocusedRowChanged +=  grid_RefView_FocusedRowChanged;
            grid_RefView.OptionsBehavior.Editable = false;
            new ReferralForm().InitCache();
        }

        private void grid_RefView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedItem = grid_RefView.GetFocusedRow() as Referral;

            if (selectedItem != null)
            {
                btn_preview.Enabled = true;
                selectedItem.Patient = Patient;
            }
                
        }
        private Referral selectedItem;

        public Patient Patient { get; set; }

        public Operator Operator { get; set; }

        public List<Referral> Referrals { get; set; }

        public Action CreateReferralAction { get; set; }
        public event EventHandler<ReferralSelectEventArgs> ReferralSelected;

        public void RefreshReferral()
        {
            if (Patient == null) return;
            GetReferrals();
            grid_referral.DataSource = Referrals;
        }

        public void GetReferrals()
        {
            Referrals = new List<Referral>();
            if (Patient == null) return;
            using (var db = new DbWorker())
            {
                var result = ReferralRepository.GetItems(Patient.Id);
                if (result!=null)
                    Referrals.AddRange(result);
            }

            grid_referral.DataSource = Referrals;

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            ////CreateReferralAction();
            //var referral = new Referral();
            //referral.Patient = Patient;
            
            //var refferalForm = new ReferralForm(referral);
            //refferalForm.ShowDialog();
            //GetReferrals();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
           GetReferrals();
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                //var preview = new ReferralReport(selectedItem);
                //preview.ShowPreview();

                if (selectedItem != null)
                {
                    //selectedItem.Operator = Operator;
                    var refferal = ReferralRepository.GetItem(selectedItem.Id);
                    refferal.Operator = Operator;
                    var form = new ReferralForm(refferal);
                    form.ShowDialog();
                }
                
            }
            
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            var referral = new Referral();
            referral.ReferralDate = DateTime.Now;
            //referral.DoctorId = visit.DoctorId;
            referral.Patient = Patient;
            referral.Operator = Operator;
            referral.DoctorId = null;
            
            var form = new ReferralForm(referral);
            form.ShowDialog();

            GetReferrals();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                ReferralRepository.Delete(selectedItem.Id);
            }
            GetReferrals();

        }

        //public Referral SelectedReferral
        //{
        //    get
        //    {
        //        //grid_referral.SelectedRowCount>
        //    }
        //}
    }

    public class ReferralSelectEventArgs:EventArgs
    {
        public Referral SelectedReferral { get; set; }
    }
}
