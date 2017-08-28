using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes.Laboratory;

namespace ARMPlugin.FormsAndControls
{
    public partial class AddOrderConfirmForm : XtraForm
    {
        private LabOrder _order;
        public AddOrderConfirmForm(LabOrder order)
        {
            InitializeComponent();
            _order = order;
            te_patient_data.Text = order.PatientData;
            FillTree();
        }

        private void FillTree()
        {
            treeList_tests.ClearNodes();

            treeList_tests.BeginUnboundLoad();
            foreach (var exam in _order.Exams)
            {
                var obj1 = new object[]
                {
                    exam.Name
                };

                var testNode = treeList_tests.AppendNode(obj1, null);

                //foreach (var param in exam.Parameters)
                //{
                //    var obj2 = new object[]
                //    {
                //        param.Name
                //    };
                //    treeList_tests.AppendNode(obj2, testNode);
                //}
            }
            treeList_tests.EndUnboundLoad();

            treeList_tests.Refresh();
            treeList_tests.ExpandAll();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
