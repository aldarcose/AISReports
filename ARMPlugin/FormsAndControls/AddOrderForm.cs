using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model.Classes.Laboratory;
using SharedUtils.FormsAndControls;
using DbCaching;
using Model.Classes.Codifiers;
using DevExpress.XtraEditors.Controls;

namespace ARMPlugin.FormsAndControls
{
    public partial class AddOrderForm : XtraForm
    {
        public LabOrder NewLabOrder { get; private set; }

        private List<Exam> _exams { get; set; }
        private List<ExamParameter> _examsParameters { get; set; }

        private List<Mkb> _mkbs = null;

        private string address=string.Empty;

        private List<string> trimestr = new List<string>() { "", "1", "2", "3"};
        
        public AddOrderForm(Patient patient, Doctor doctor)
        {
            InitializeComponent();
            NewLabOrder = new LabOrder()
            {
                Patient = patient,
                Doctor = doctor
            };
            NewLabOrder.Exams = new List<Exam>();

            if (!string.IsNullOrEmpty(patient.FactAddress.Text))
            {
                address = patient.FactAddress.Text;
            }
            else if (!string.IsNullOrEmpty(patient.RegAddress.Text))
            {
                address = patient.RegAddress.Text;
            }

            lblAdress.Text = lblAdress.Text+address;

            if (patient.UchastokDopId != default(long) && patient.UchastokDopId != -1)
            {
                var medArea=new MedArea();
                medArea.LoadData(patient.UchastokDopId);
                lblDopUchastok.Text += " " + medArea.Name;
            }
                
            _exams = Exam.GetAll();
            //_examsParameters = ExamParameter.GetAll();

            cmb_trimestr.Properties.Items.AddRange(trimestr);

            //MO
            lueLPU.Properties.DataSource = CodifiersHelper.GetMOs().OrderBy(x => x.Name);
            lueLPU.Properties.ShowHeader = false;
            lueLPU.Properties.Columns.Clear();
            lueLPU.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 10) { Visible = false }
                );
            lueLPU.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueLPU.Properties.DisplayMember = "Name";
            lueLPU.Properties.ValueMember = "Id";
            lueLPU.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueLPU.Properties.AutoSearchColumnIndex = 1;

            RefreshTree();
        }

        private void RefreshTree()
        {
            treeList1.ClearNodes();
            
            const string header1 = "Исследование";
            const string header2 = "Параметр";

            treeList1.BeginUnboundLoad();
            foreach (var exam in _exams)
            {
                var obj1 = new object[]
                {
                    exam.Name,
                    exam.Id
                };

                var testNode = treeList1.AppendNode(obj1, null);

                //foreach (var param in _examsParameters.Where(t => t.ExamId == exam.Id))
                //{
                //    var obj2 = new object[]
                //    {
                //        param.Name,
                //        param.Id
                //    };
                //    treeList1.AppendNode(obj2, testNode);
                //}
            }
            treeList1.EndUnboundLoad();

            treeList1.Refresh();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            var err_message = string.Empty;
            if (!Validate(out err_message))
            {
                XtraMessageBox.Show(err_message);
            }
            else
            {
                NewLabOrder.Exams.Clear();

                foreach (var node in treeList1.Nodes.Where(t=>t.CheckState!= CheckState.Unchecked))
                {
                    var exam = _exams.First(t => t.Id == (long) node[treeListColumn_id]);
                    //exam.Parameters = new List<ExamParameter>();
                    //foreach (var paramNode in node.Nodes.Where(t=>t.Checked))
                    //{
                    //    var param = _examsParameters.First(t => t.Id == (long)paramNode[treeListColumn_id]);
                    //    exam.Parameters.Add(param);
                    //}
                    NewLabOrder.Exams.Add(exam);
                }

                if (mkbSearchControl.SelectedItem!=null)
                {
                    NewLabOrder.MKBCode = mkbSearchControl.SelectedItem.Code;
                }

                if (chVoenkomat.EditValue!=null)
                {
                    NewLabOrder.Voenkomat = (bool)chVoenkomat.EditValue;
                }

                if (cmb_trimestr.EditValue != null)
                {
                    NewLabOrder.Trimestr = string.IsNullOrEmpty(cmb_trimestr.Text) ? null : cmb_trimestr.Text;
                }

                if (lueLPU.EditValue!=null)
                {
                    NewLabOrder.SendedLpuCode = lueLPU.EditValue.ToString();
                }

                var confirmForm = new AddOrderConfirmForm(NewLabOrder);
                if (confirmForm.ShowDialog() == DialogResult.OK)
                {
                    NewLabOrder.Save();
                    this.DialogResult = DialogResult.OK;
                }

            }
        }

        private bool Validate(out string message)
        {
            if (NewLabOrder.Patient == null)
            {
                message = "Укажите пациента";
                return false;
            }

            if (treeList1.Nodes.All(t=>t.CheckState == CheckState.Unchecked))
            {
                message = "Не выбрано ни одно исследование";
                return false;
            }
            
            if (NewLabOrder.Patient!=null && string.IsNullOrEmpty(NewLabOrder.Patient.FactAddress.Text))
            {
                message = "Не указан адрес проживания пациента";
                return false;
            }

            

            message = "";
            return true;
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            //if (e.Node.HasChildren)
            //{
            //    if (e.Node.Checked)
            //    {
            //        e.Node.CheckAll();
            //    }
            //    else
            //    {
            //        e.Node.UncheckAll();
            //    }
            //}
            //else
            //{
            //    e.Node.Checked = true;
            //    //if (e.Node.ParentNode.Nodes.All(t => t.Checked))
            //    //    e.Node.ParentNode.Checked = true;
            //    //else if (e.Node.ParentNode.Nodes.All(t => !t.Checked))
            //    //    e.Node.ParentNode.Checked = false;
            //    //else
            //    //    e.Node.ParentNode.CheckState = CheckState.Indeterminate;
            //}

            
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}
