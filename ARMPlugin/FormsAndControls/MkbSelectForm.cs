using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model.Classes.Codifiers;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class MkbSelectForm : XtraForm
    {
        private List<MkbClass> _mkbClasses = null;
        private List<MkbSubclass> _mkbSubClasses = null;
        private List<Mkb> _filteredMkb = null;
        public Mkb SelectedMkb { get; private set; }

        public MkbSelectForm() : this(null) { }

        public MkbSelectForm(Mkb mkb)
        {
            InitializeComponent();

            InitForm(mkb);
        }

        private void InitForm(Mkb mkb)
        {
            _mkbClasses = _mkbClasses ?? CodifiersHelper.GetMkbClasses();
            cmb_class.Properties.Items.AddRange(_mkbClasses.Select(t => string.Format("{0} {1}", t.Number, t.Name)).ToList());
            
            _mkbSubClasses = _mkbSubClasses ?? CodifiersHelper.GetMkbSubClasses();

            te_chosen.Text = string.Empty;

            if (mkb !=null)
            {
                using (var db = new DbWorker())
                {
                    var q = new DbQuery("InitForm");
                    q.Sql = "SELECT c.class_id, d.kodname " +
                            "FROM diagn_kod_mkb_tab d " +
                            "LEFT JOIN classmkb_tab c ON d.kodname BETWEEN c.kod1 AND c.kod2 " +
                            "WHERE kodname = substring(@mkbCode from 1 for 3);";
                    q.AddParamWithValue("@mkbCode", mkb.Code);

                    var result = db.GetResult(q);
                    if (result != null && result.Fields.Count > 0)
                    {
                        var id = DbResult.GetNumeric(result.GetByName("class_id"), -1);
                        var code = DbResult.GetString(result.GetByName("kodname"), "");

                        var mkbClass = _mkbClasses.FirstOrDefault(t => t.Id == id);
                        if (mkbClass!=null)
                            cmb_class.Text = string.Format("{0} {1}", mkbClass.Number, mkbClass.Name);

                        var mkbSubClass = _mkbSubClasses.FirstOrDefault(t => t.Code.Equals(code));
                        if (mkbSubClass != null)
                            cmb_subclass.Text = mkbSubClass.Name;
                    }
                }

                cmb_diagn.Text = string.Format("{0} {1}", mkb.Code, mkb.Name);

                /*QSqlSelectCursor cursor("",DBase);
	            QString queryStr = "SELECT c.class_id, d.kodname FROM diagn_kod_mkb_tab d ";
	            queryStr += "LEFT JOIN classmkb_tab c ON d.kodname BETWEEN c.kod1 AND c.kod2 ";
	            queryStr += "WHERE kodname = substring('%1' from 1 for 3);";
	            cursor.exec(queryStr.arg(code));
	            if (cursor.next())
	            {
		            classCombo->setData(cursor.value("class_id"));
		            fillSubclassCombo();
		            subclassCombo->setData(cursor.value("kodname"));
		            fillDiagnCombo();
		            diagnCombo->setData(code);
		            setDiagn();
	            }*/
            }
        }

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cmb_class.Text;
            if (string.IsNullOrEmpty(selected)) return;
            
            var mkbClass = _mkbClasses.FirstOrDefault(t => selected.Equals(string.Format("{0} {1}", t.Number, t.Name)));
            if (mkbClass != null)
            {
                var items =
                    _mkbSubClasses.Where(
                        t =>
                            String.CompareOrdinal(t.Code, mkbClass.CodeFrom) >= 0 &&
                            String.CompareOrdinal(t.Code, mkbClass.CodeTo) <= 0);
                
                cmb_subclass.Properties.Items.Clear();
                cmb_subclass.Properties.Items.AddRange(items.Select(t => t.Name).ToList());
                cmb_subclass.Text = string.Empty;
                cmb_diagn.Text = string.Empty;
                te_chosen.Text = string.Empty;
            }
        }

        private void cmb_subclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cmb_subclass.Text;
            if (string.IsNullOrEmpty(selected))
            {
                te_chosen.Text = string.Empty;
                return;
            }

            var mkbSubClass = _mkbSubClasses.FirstOrDefault(t => t.Name.Equals(selected));
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDiagnoses");
                q.Sql = "SELECT kod_d, mkb_id, mkb FROM mkb_tab WHERE kod_d LIKE @kod order by 1;";
                q.AddParamWithValue("@kod", string.Format("{0}%", mkbSubClass.Code));
                var results = db.GetResults(q);
                if (results != null && results.Count > 0)
                {
                    cmb_diagn.Properties.Items.Clear();

                    _filteredMkb = new List<Mkb>();

                    foreach (var result in results)
                    {
                        var id = DbResult.GetNumeric(result.Fields[1], -1);
                        var name = DbResult.GetString(result.Fields[2], "");
                        var code = DbResult.GetString(result.Fields[0], "");
                        if (id != -1)
                        {
                            var mkb = new Mkb();
                            mkb.Id = id;
                            mkb.Code = code;
                            mkb.Name = name;
                            _filteredMkb.Add(mkb);
                        }
                    }

                    cmb_diagn.Properties.Items.AddRange(_filteredMkb.Select(t=>string.Format("{0} {1}", t.Code, t.Name)).ToList());

                    cmb_diagn.Text = string.Empty;
                    te_chosen.Text = string.Empty;
                }
            }
        }

        private void cmb_diagn_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cmb_diagn.Text;
            if (string.IsNullOrEmpty(selected))
            {
                te_chosen.Text = string.Empty;
                return;
            }

            te_chosen.Text = selected;
        }
        private void te_chosen_TextChanged(object sender, EventArgs e)
        {
            btn_select.Enabled = te_chosen.Text.Length > 0;
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            var filter = te_chosen.Text;

            SelectedMkb = _filteredMkb.FirstOrDefault(t => filter.StartsWith(t.Code));

            if (SelectedMkb != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("Диагноз не выбран!");
            }
        }
    }
}
