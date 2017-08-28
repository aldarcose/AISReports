using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model;
using Model.Classes;
using Model.Classes.Codifiers;
using SharedDbWorker;
using SharedDbWorker.Classes;
using DevExpress.XtraTreeList.Columns;


namespace ARMPlugin.FormsAndControls
{
    public partial class EditDiagnForm : XtraForm
    {
        private List<Mkb> _mkbs = null;
        private List<DiagnoseType> _diagnoseTypes = null;
        private List<DiseaseStadia> _stadias = null;
        private List<DiseaseCharacter> _forms = null;
        private List<UnpaidDiagnose> _unpaidDiagnoses = null;
        private long PatientId { get; set; }
        private Operator Operator { get; set; }

        private List<Diagnose> _talonDiagnoses { get; set; }
        public Diagnose EditDiagnose { get; private set; }

        public bool IsDisp { get; set; }

        public EditDiagnForm(Operator @operator, Diagnose editDiagnose, List<Diagnose> talonDiagnoses)
        {
            InitializeComponent();
            
            IsDisp = false;

            _talonDiagnoses = talonDiagnoses;
            EditDiagnose = editDiagnose;

            PatientId = editDiagnose.PatientId;
            Operator = @operator;
            if (editDiagnose.Id == -1)
                InitForm();
            else
                InitForm(editDiagnose);
        }

        private void InitForm()
        {
            _mkbs = CodifiersHelper.GetMkbs();
            _stadias = CodifiersHelper.GetDiseaseStadias();
            _diagnoseTypes = CodifiersHelper.GetDiseaseTypes();
            _forms = CodifiersHelper.GetDiseaseCharacters();
            _unpaidDiagnoses = CodifiersHelper.GetUnpaidDiagnoses();

            cmb_type.Properties.Items.AddRange(_diagnoseTypes.Select(t => t.Name).ToList());

            var defaultId = 200001; // код основного диагноза
            cmb_type.Text = _diagnoseTypes.First(t => t.Id.Equals(defaultId)).Name;

            cmb_stady.Properties.Items.AddRange(_stadias.Select(t => t.Name).ToList());

            cmb_character.Properties.Items.AddRange(_forms.Select(t => t.Name).ToList());

            fillDiagnHistory();

            if (IsDisp )//&&(typeDiagnCombo->getData().toInt()==200001))
		    {
			    /*a = "SELECT diagn_id, diagn_id||' '||mkb FROM dispensary_registration_tab d ";
			    a +="LEFT JOIN mkb_tab m ON trim(m.kod_d) = trim(d.diagn_id) ";
			    a +="WHERE trim(diagn_id||' '||mkb) ilike '%:name:%' AND d.dan_id = "+QString::number(danID)+" ORDER BY 1";
			
			    mkbComboList->setStrQuery("SELECT diagn_id, diagn_id||' '||mkb FROM dispensary_registration_tab d "
				    "LEFT JOIN mkb_tab m ON trim(m.kod_d) = trim(d.diagn_id) "
				    "WHERE trim(diagn_id||' '||mkb) ilike '%:name:%' AND d.dan_id = "+QString::number(danID)+" ORDER BY 1");
			    mkbComboList->setCodeSql("SELECT kod_d , kod_d||' '||mkb  FROM mkb_tab WHERE kod_d = ':code:' ORDER BY 1");
			    mkbComboList->fill();*/
		    }
		    else
		    {
			    /*a = "SELECT kod_d, kod_d||' '||mkb FROM mkb_tab WHERE trim(kod_d||' '||mkb) ilike '%:name:%' ORDER BY 1";
			    mkbComboList->setStrQuery("SELECT kod_d, kod_d||' '||mkb FROM mkb_tab WHERE trim(kod_d||' '||mkb) ilike '%:name:%' ORDER BY 1");
			    mkbComboList->setCodeSql("SELECT kod_d , kod_d||' '||mkb  FROM mkb_tab WHERE kod_d = ':code:' ORDER BY 1");
			    mkbComboList->fill();*/
		    }

            
            
        }

        private void InitForm(Diagnose diagnose)
        {
            InitForm();

            var type = _diagnoseTypes.FirstOrDefault(t => t.Id.Equals(diagnose.DiagnoseTypeId));
            if (type != null)
                cmb_type.Text = type.Name;

            var stady = _stadias.FirstOrDefault(t => t.Id.Equals(diagnose.DiseaseStadiaId));
            if (stady != null)
                cmb_stady.Text = stady.Name;

            var character = _forms.FirstOrDefault(t => t.Id.Equals(diagnose.DiseaseCharacterId));
            if (character != null)
                cmb_character.Text = character.Name;

            cmb_mkb.Text = string.Format("{0} {1}", diagnose.MkbCode, diagnose.MkbName);

            chk_f12.Checked = diagnose.IsRegistrered;
            chk_approvedByLab.Checked = diagnose.LabConfirmed;
            memo_clinic.Text = diagnose.Description;
        }

        private void fillDiagnHistory()
        {
            
            var query = new DbQuery("FillHistory");

            query.Sql =
                    "SELECT DISTINCT diagn_id FROM dispan_uchet_tab WHERE dan_id = @patientId AND (date_sn IS NULL OR date_sn < current_date) ORDER BY 1;";
            query.AddParamWithValue("@patientId", PatientId);

            using (var dbWorker = new DbWorker())
            {
                var results = dbWorker.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    var title = "Дисп. учет";
                    var parentNode = tree_history.AppendNode(new object[] { title }, null);
                    foreach (var result in results)
                    {
                        var diagn = DbResult.GetString(result.Fields[0], "");
                        tree_history.AppendNode(new object[] { diagn }, parentNode);
                    }
                }
            }

            if (!IsDisp)
            {
                query.Sql =
                    "SELECT DISTINCT diagn_osn FROM diagn_tab WHERE talon_id IN (SELECT talon_id FROM talon_tab WHERE dan_id = @patientId) ORDER BY 1;";
                query.AddParamWithValue("@patientId", PatientId);

                using (var dbWorker = new DbWorker())
                {
                    var results = dbWorker.GetResults(query);
                    if (results != null && results.Count > 0)
                    {
                        var title = "Ранее зарег.";
                        var parentNode = tree_history.AppendNode(new object[] { title }, null);
                        foreach (var result in results)
                        {
                            var diagn = DbResult.GetString(result.Fields[0], "");
                            tree_history.AppendNode(new object[] { diagn }, parentNode);
                        }
                    }
                }
            }

            foreach(TreeListColumn col in tree_history.Columns)
            {
                col.OptionsColumn.AllowEdit = false;
                col.OptionsColumn.ReadOnly = true;
            }

        }

        private void btn_choose_Click(object sender, EventArgs e)
        {
            var selected = cmb_mkb.Text;
            var code = selected.Split(new[] {' '})[0];

            var mkb = _mkbs.FirstOrDefault(t => t.Code.Equals(code));

            var f = mkb == null ? new MkbSelectForm() : new MkbSelectForm(mkb);
            if (f.ShowDialog() == DialogResult.OK)
            {
                EditDiagnose.MkbCode = f.SelectedMkb.Code;
                EditDiagnose.MkbName = f.SelectedMkb.Name;

                cmb_mkb.Text = string.Format("{0} {1}", EditDiagnose.MkbCode, EditDiagnose.MkbName);
            }
        }

        private void cmb_mkb_TextChanged(object sender, EventArgs e)
        {
            var filter = cmb_mkb.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                var normalFilter = filter.ToLower();
                cmb_mkb.Properties.Items.Clear();
                cmb_mkb.Properties.Items.AddRange(_mkbs.Where(t => t.Code.ToLower().StartsWith(normalFilter) || t.Name.ToLower().StartsWith(filter)).Select(t=>string.Format("{0} {1}", t.Code, t.Name)).ToList());
            }
        }

        private void ReadDiagnoseData()
        {
            EditDiagnose.DoctorId = Operator.DoctorId;
            EditDiagnose.IsRegistrered = chk_f12.Checked;
            EditDiagnose.LabConfirmed = chk_approvedByLab.Checked;

            EditDiagnose.Description = memo_clinic.Text;

            var diagnoseType = _diagnoseTypes.FirstOrDefault(t => t.Name.Equals(cmb_type.Text));
            if (diagnoseType != null)
            {
                EditDiagnose.DiagnoseTypeId = diagnoseType.Id;
                EditDiagnose.DiagnoseType = diagnoseType.Name;
            }
            else EditDiagnose.DiagnoseTypeId = -1;

            
            var diseaseForm = _forms.FirstOrDefault(t => t.Name.Equals(cmb_character.Text));
            if (diseaseForm != null)
            {
                EditDiagnose.DiseaseCharacterId = diseaseForm.Id;
                EditDiagnose.DiseaseCharacter = diseaseForm.Name;
            }
            else
                EditDiagnose.DiseaseCharacterId = -1;

            var diseaseStadia = _stadias.FirstOrDefault(t => t.Name.Equals(cmb_stady.Text));
            if (diseaseStadia != null)
            {
                EditDiagnose.DiseaseStadiaId = diseaseStadia.Id;
                EditDiagnose.DiseaseStadia = diseaseStadia.Name;
            }
            else
            {
                EditDiagnose.DiseaseStadiaId = -1;
            }

            var firstSpace = cmb_mkb.Text.IndexOf(" ");
            if (firstSpace == -1)
                EditDiagnose.MkbCode = "";
            else
            {
                EditDiagnose.MkbCode = cmb_mkb.Text.Substring(0, firstSpace);
                EditDiagnose.MkbName = cmb_mkb.Text.Substring(firstSpace + 1);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ReadDiagnoseData();

            if (string.IsNullOrEmpty(EditDiagnose.MkbCode))
            {
                ShowWarningMessage("Не указан диагноз!");
                return;
            }
            else
            {
                if (!_mkbs.Any(t => t.Code.ToLower().Equals(EditDiagnose.MkbCode.ToLower())))
                {
                    ShowWarningMessage("Указан несуществующий диагноз!");
                    return;
                }
            }

            if (EditDiagnose.DiagnoseTypeId == -1)
            {
                ShowWarningMessage("Не указан тип диагноза!");
                return;
            }
            if (EditDiagnose.DiseaseCharacterId == -1)
            {
                ShowWarningMessage("Не указан характер заболевания!");
                return;
            }
            else
            {
                // если характер заболевания НЕ хронический (ID == 0, ID == 3);
                if (EditDiagnose.DiseaseCharacterId == 0 || EditDiagnose.DiseaseCharacterId == 3)
                {
                    var chronicOnlyMkb = new string[] {"J45.8", "J44.8", "Е10.7", "E11.7", "I11.9", "I20.8"};
                    if (chronicOnlyMkb.Any(t => EditDiagnose.MkbCode.ToUpper().Equals(t)))
                    {
                        ShowWarningMessage("Характер указанного диагноза может быть только хроническим.");
                        return;
                    }
                }
            }

            if (_talonDiagnoses.Any(t => t.Id != EditDiagnose.Id && t.MkbCode.Equals(EditDiagnose.MkbCode)))
            {
                ShowWarningMessage("Такой диагноз уже поставлен!");
                return;
            }

            var mainTypeDiagnId = 200001; // ид основного диагноза
            var mainTypeStr = _diagnoseTypes.First(t => t.Id == mainTypeDiagnId).Name;
            var sopTypeDiagnId = 2; // ид сопутствующего диагноза
            var sopTypeDiagnStr = _diagnoseTypes.First(t => t.Id == sopTypeDiagnId).Name;

            if (EditDiagnose.DiagnoseTypeId == mainTypeDiagnId && _talonDiagnoses.Any(t => t.Id != EditDiagnose.Id && t.DiagnoseTypeId.Equals(mainTypeDiagnId)))
            {
                var mainDiagn =
                    _talonDiagnoses.FirstOrDefault(t => t.Id != EditDiagnose.Id && t.DiagnoseTypeId == mainTypeDiagnId);
                if (mainDiagn != null)
                {
                    var mainDiagnStr = string.Format("{0} {1}", mainDiagn.MkbCode, mainDiagn.MkbName);
                    var res =
                        XtraMessageBox.Show("Для текущего талона основным диагнозом указан " + mainDiagnStr +
                                            ".\nНазначить новый основной диагноз?", "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                        return;

                    if (res == DialogResult.Yes)
                    {
                        mainDiagn.DiagnoseTypeId = sopTypeDiagnId;
                        mainDiagn.DiagnoseType = sopTypeDiagnStr;

                        mainDiagn.IsSaved = false;
                    }
                    else
                    {
                        EditDiagnose.DiagnoseType = sopTypeDiagnStr;
                        EditDiagnose.DiagnoseTypeId = sopTypeDiagnId;
                    }
                }
            }

            if (_unpaidDiagnoses.Any(t => t.MkbCode.Equals(EditDiagnose.MkbCode)))
            {
                var res =
                        XtraMessageBox.Show("Указанный диагноз не подлежит оплате в ФОМС.\n" +
                                            "Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                    return;
            }

            if (EditDiagnose.Id == -1)
            {
                if (!_talonDiagnoses.Contains(EditDiagnose))
                    _talonDiagnoses.Add(EditDiagnose);
            }

            EditDiagnose.IsSaved = false;
            this.DialogResult = DialogResult.OK;
        }

        private void ShowWarningMessage(string message)
        {
            XtraMessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmb_mkb_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
        }

        private void cmb_mkb_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-Ru"));
        }

        private void tree_history_DoubleClick(object sender, EventArgs e)
        {
            var info = tree_history.CalcHitInfo(tree_history.PointToClient(Control.MousePosition));
            if (info.Node!=null)
            {
                var mkbValue = info.Node.GetValue(0).ToString();
                //вставялем 
                var mkbItem = _mkbs.Where(c => c.Code == mkbValue).FirstOrDefault();
                cmb_mkb.Text = string.Format("{0} {1}", mkbItem.Code, mkbItem.Name);
            }
        }
    }
}
