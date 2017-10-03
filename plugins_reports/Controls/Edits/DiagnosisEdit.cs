using SharedDbWorker;
using SharedDbWorker.Classes;
using System.Windows.Forms;
using System.Linq;
using System;

namespace Reports.Controls
{
    public partial class DiagnosisEdit : UserControl, IParameter
    {
        private Connection conn;
        private const string sqlQuery = @"select kod_d, mkb from public.mkb_tab where kod_d || mkb like '%{0}%' order by kodname";

        public DiagnosisEdit()
        {
            InitializeComponent();
            conn = new Connection(ConnectionParameters.Instance);
        }

        public object Value
        {
            get
            {
                if (string.IsNullOrEmpty(CodeIn))
                    throw new InvalidOperationException("Не выбран диагноз с");
                if (string.IsNullOrEmpty(CodeOut))
                    throw new InvalidOperationException("Не выбран диагноз по");
                
                return new Tuple<string, string>(CodeIn, CodeOut); 
            }
        }

        public string CodeIn
        {
            get
            {
                var selectedItem = cbCodeIn.SelectedItem as CodeItem;
                if (selectedItem != null)
                    return selectedItem.Code;
                return null;
            }
        }

        public string CodeOut
        {
            get
            {
                var selectedItem = cbCodeOut.SelectedItem as CodeItem;
                if (selectedItem != null)
                    return selectedItem.Code;
                return null;
            }
        }

        private void cbCodeIn_TextChanged(object sender, System.EventArgs e)
        {
            DoQueryCodeIn();
        }

        private void cbCodeOut_TextChanged(object sender, System.EventArgs e)
        {
            DoQueryCodeOut();
        }

        private void DoQueryCodeIn()
        {
            string text = cbCodeIn.Text;
            if (string.IsNullOrEmpty(text)) return;
            var dbQuery = new DbQuery("");
            dbQuery.Sql = string.Format(sqlQuery, text);
            var dbResults = conn.GetResults(dbQuery);
            if (dbResults.Count != 0)
            {
                cbCodeIn.Items.Clear();
                cbCodeIn.Items.AddRange(dbResults.Select(r => new CodeItem(r)).ToArray());
            }
        }

        private void DoQueryCodeOut()
        {
            string text = cbCodeOut.Text;
            if (string.IsNullOrEmpty(text)) return;
            var dbQuery = new DbQuery("");
            dbQuery.Sql = string.Format(sqlQuery, text);
            var dbResults = conn.GetResults(dbQuery);
            if (dbResults.Count != 0)
            {
                cbCodeOut.Items.Clear();
                cbCodeOut.Items.AddRange(dbResults.Select(r => new CodeItem(r)).ToArray());
            }
        }
    }

    public class CodeItem
    {
        private DbResult dbResult;

        public CodeItem(DbResult dbResult)
        {
            this.dbResult = dbResult;
        }

        public string Code
        {
            get { return (string)dbResult.Fields[0]; }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", 
                dbResult.Fields[0], 
                dbResult.Fields[1].ToString().Trim());
        }
    }
}
