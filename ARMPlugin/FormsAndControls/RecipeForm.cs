using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Export.Web;
using DevExpress.XtraReports.UI;
using Model;
using Model.Classes;
using PrintFormTesting.Classes;
using SharedDbWorker;
using SharedDbWorker.Classes;
using SharedUtils.Classes;
using ImageList = Model.Classes.ImageList;
using ARMPlugin.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class RecipeForm : XtraForm
    {
        private List<Medicament> _medicaments = null;
        private List<Medicament> _diabet_medicaments = null;
        private Patient _patient = null;
        private Visit _visit = null;
        private List<Diagnose> _talonDiagnoses = null;

        //private Recipe _recipe = null;

        private const string FederalSource = "1. Федеральный";
        private const string RegionSource = "2. Субьект РФ";
        private const string MunicipalSource = "3. Муниципальный";

        private const string FreePaymentPercentage = "1. Бесплатно";
        private const string HalfPaymentPercentage = "2. 50%";

        private List<string> _signas = null;

        private long? _recipeId=null;
        private string _selectedLSId = null;
        private long recipeDoctorId = 0;

        public RecipeForm()
        {
            InitializeComponent();
            Init();
        }

        
        private void InitForm(Recipe recipe)
        {
            //InitializeComponent();
            //Init();
            var patient = new Patient();
            patient.LoadData(recipe.PatientId);
            //InitForm(patient);
            de_out_date.Text = recipe.ReleaseDate.ToString();
            InitDiagnoses(recipe.TalonId);
            
            #region init components
            te_serial.Text = recipe.Serial;
            te_number.Text = recipe.Number;

            //recipe.DoctorId = Operator.DoctorId;
            //recipe.PatientId = _visit.PatientId;
            //recipe.TalonId = _visit.TalonId;

            de_insert_date.DateTime=recipe.InsertInfoDate;

            cmb_mkb.Text = recipe.MkbCode;
            cmb_revenue_type.SelectedItem = recipe.RevenueSource;
            cmb_revenue_type.SelectedItem = recipe.PayPercent;

            chk_order_by_trade.Checked = recipe.IsTradeName;

            cmb_medicaments.Text = _medicaments.Where(t => t.Code == recipe.MedicamentId.ToString()).Select(t => string.Format("{0} ({3}), {1}, {2}", t.TrnmRus, t.Doze, t.Producer, t.Code)).FirstOrDefault();

            
            //var medicament = SelectedMedicament;
            //if (medicament != null)
            //{
            //    var code = long.Parse(medicament.Code);
            //    recipe.MedicamentId = medicament.Id;
            //    recipe.MedicamentFedCode = code;
            //    recipe.MedicamentName = chk_order_by_trade.Checked ? medicament.Name : medicament.Mnn;
            //    recipe.MedicamentDtd = medicament.FormName;
            //    recipe.Doze = medicament.Doze;
            //}

            cmb_signa.Text=recipe.Signa;

            spin_count.Value = recipe.Count;

            cmb_lgot_category.Text=recipe.BenefitCode;


            int index = 0;
            switch (recipe.Validity)
            {
                case 5:
                    index = 0;
                    break;
                case 10:
                    index = 1;
                    break;
                case 15:
                    index = 0;
                    break;
                case 30:
                    index = 1;
                    break;
                case 90:
                    index = 2;
                    break;
            }
            cmb_valid_date.SelectedIndex = index;
            de_out_date.DateTime=recipe.DischargeDate;
            chk_order_through_VK.Checked=recipe.IsVK;

            if (recipe.PayPercent!=null)
            {
                switch(recipe.PayPercent)
                {
                    case PayPercent.Free: cmb_payment_percentage.SelectedIndex = 1; break;
                    case PayPercent.P50: cmb_payment_percentage.SelectedIndex = 2; break;
                }
            }
            #endregion
            _selectedLSId = SelectedMedicament.Code;
        }
        
        /// <summary>
        /// Загрузка справочников формы
        /// </summary>
        private void Init()
        {
            cmb_payment_percentage.Properties.Items.AddRange(new object[] { "", FreePaymentPercentage, HalfPaymentPercentage });

            cmb_revenue_type.Properties.Items.AddRange(new object[] { "", FederalSource, RegionSource, MunicipalSource });

            cmb_valid_date.Properties.Items.AddRange(new object[] { "15 дней", "30 дней", "90 дней" });

            de_insert_date.DateTime = DateTime.Now;
            
            _medicaments = CodifiersHelper.GetMedicaments();
            _diabet_medicaments = _medicaments.Where(m => m.DiabetCode.HasValue && m.DiabetCode.Value != 0).Select(m => m).ToList();

            LoadSignas();
            cmb_signa.Properties.Items.AddRange(_signas);

            this.Closing += OnClosing;

            te_serial.Text = Settings.GetSettingValue("REC_SER");
        }


        private void LoadSignas()
        {
            _signas = new List<string>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetSignas");
                q.Sql = "select name from codifiers.signa_tab order by name;";
                var signas = db.GetResults(q);
                foreach (var result in signas)
                {
                    var signa = DbResult.GetString(result.GetByName("name"), string.Empty);
                    _signas.Add(signa);
                }
            }
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _medicaments = null;
            _patient = null;
            //_visit = null;

            GC.Collect();
        }

        private void InitForm(Patient patient)
        {
            te_fio.Text = string.Format("{0} {1} {2}", patient.LastName, patient.FirstName, patient.MidName);
            te_policy.Text = string.Format("{0}{1}",
                string.IsNullOrEmpty(patient.Policy.Serial) ? string.Empty : patient.Policy.Serial,
                patient.Policy.Number);
            de_birthdate.DateTime = patient.BirthDate;
            te_address.Text = patient.RegAddress.Text;
            te_medcard.Text = patient.MedCardNum;
            te_snils.Text = patient.Snils;

            _patient = patient;

            if (patient != null)
                if (patient.Lgotas != null)
                {
                    cmb_lgot_category.Properties.Items.AddRange(patient.Lgotas.Select(t => t.LgotaCode).ToArray());

                    if (cmb_lgot_category.Properties.Items.Count > 0)
                    {
                        cmb_lgot_category.SelectedIndex = 0;
                    }
                }

        }


        private void InitDiagnoses(long? talonId)
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDiagnose");
                q.Sql = "SELECT d.*," +
                        "c.name as character_name, v.name AS type_name, m.mkb as mkb_name, s.name_stadiya as stadia_name, v.sort as diagn_type_sort " +
                        "FROM diagn_tab d " +
                        "LEFT JOIN public.sp_stadiya_diagn_tab s ON d.stage_of_disease = s.sp_stadiya_diagn_id " +
                        "LEFT JOIN codifiers.sp_disease_vid_tab v ON v.id = d.sp_disease_vid_id " +
                        "LEFT JOIN codifiers.diagn_type_tab c ON d.diagn_type_id = c.id " +
                        "LEFT JOIN mkb_tab m ON m.kod_d = d.diagn_osn " +
                        "WHERE d.talon_id = @id";
                q.AddParamWithValue("@id", talonId);
                var results = db.GetResults(q);
                if (results != null && results.Count > 0)
                {
                    _talonDiagnoses = new List<Diagnose>();
                    foreach (var result in results)
                    {
                        var diagn = new Diagnose();
                        if (diagn.LoadData(result))
                        {
                            _talonDiagnoses.Add(diagn);
                        }
                    }
                }
            }
            if (_talonDiagnoses != null)
            {
                var list = new List<string>();
                foreach (Diagnose t in _talonDiagnoses)
                    list.Add(t.MkbCode ?? null);
                cmb_mkb.Properties.Items.AddRange(items: list.ToArray());

                if (cmb_mkb.Properties.Items.Count > 0)
                {
                    cmb_mkb.SelectedIndex = 0;
                }
            }
        }

        public void InitForm(Patient patient, Visit v)
        {
            InitForm(patient);
            _visit = v;

            de_out_date.DateTime = _visit.VisitDate.HasValue ? _visit.VisitDate.Value : DateTime.Now;

            InitDiagnoses(v.TalonId);
            
        }

        private Medicament SelectedMedicament
        {
            get
            {
                var filter = cmb_medicaments.Text;
                if (string.IsNullOrEmpty(filter))
                    return null;

                var m = Regex.Match(filter, @".*?\((\d+)\)");
                if (m.Success)
                {
                    var code = m.Groups[1].Value;
                    var medicament =
                        _medicaments.FirstOrDefault(
                            t =>
                                t.Code.Equals(code));
                    return medicament;
                }
                return null;
            }
        }

        public Operator Operator { get; set; }

        private void cmb_medicaments_TextChanged(object sender, EventArgs e)
        {
            var filter = cmb_medicaments.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                var normalFilter = filter.ToLower();
                cmb_medicaments.Properties.Items.Clear();

                if (cmb_mkb.Text.Contains("E1") || cmb_mkb.Text.Contains("O24.4"))
                {
                    cmb_medicaments.Properties.Items.AddRange(
                        _diabet_medicaments.Where(
                            t => t.TrnmRus.ToLower().StartsWith(normalFilter) || t.TrnmRus.ToLower().StartsWith(filter) )
                            .Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName))
                            .ToList());
                }
                else
                {
                    cmb_medicaments.Properties.Items.AddRange(
                        _medicaments.Where(
                            t => t.TrnmRus.ToLower().StartsWith(normalFilter) || t.TrnmRus.ToLower().StartsWith(filter))
                            .Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName))
                            .ToList());
                }

                
                
                if (cmb_medicaments.Properties.Items.Count == 0 && SelectedMedicament!=null)
                {
                    cmb_signa.Text = SelectedMedicament.Signa;
                }
                
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Register())
            {
                Print();
            }
                
        }

        private void Print()
        {
            var filter = cmb_medicaments.Text;

            if (!string.IsNullOrEmpty(filter))
            {
                var medicament = SelectedMedicament;
                if (medicament != null)
                {
                    #region oldcode
                    /*var path = Path.Combine(Environment.CurrentDirectory, @"blank\recept_06_a4_pave.xml");
                    if (File.Exists(path))
                    {
                        BlankDocument document = new BlankDocument();
                        document.LoadPattern(path);

                        var dict = new Dictionary<string, string>();

                        // огрн
                        dict.Add("#:print_ogrn:", "");

                        // код категории
                        dict.Add("#:print_lgota:", cmb_lgot_category.Text);
                        // диагноз
                        dict.Add("#:print_diagn:", cmb_mkb.Text);


                        // источник финансирования
                        dict.Add("#:print_fed:", "");
                        dict.Add("#:print_sub_rf:", "");
                        dict.Add("#:print_mun:", "");
                        var selectedRevenue = cmb_revenue_type.SelectedIndex;
                        switch (selectedRevenue)
                        {
                            case 1:
                                dict["#:print_fed:"] = "_____________";
                                break;
                            case 2:
                                dict["#:print_sub_rf:"] = "_____________";
                                break;
                            case 3:
                                dict["#:print_mun:"] = "_____________";
                                break;
                        }

                        // способ оплаты
                        dict.Add("#:print_no_cost:", "");
                        dict.Add("#:print_half_cost:", "");
                        var selectedPaid = cmb_payment_percentage.SelectedIndex;
                        switch (selectedPaid)
                        {
                            case 1:
                                dict["#:print_no_cost:"] = "_______";
                                break;
                            case 2:
                                dict["#:print_half_cost:"] = "_______";
                                break;
                        }

                        // срок действия
                        dict.Add("#:cross_5d:", "");
                        dict.Add("#:cross_10d:", "");
                        dict.Add("#:cross_1m:", "");
                        dict.Add("#:cross_3m:", "");
                        var selectedTime = cmb_valid_date.SelectedIndex;
                        switch (selectedTime)
                        {
                            case 0:
                                dict["#:cross_5d:"] = "_________";
                                break;
                            case 1:
                                dict["#:cross_10d:"] = "_________";
                                break;
                            case 2:
                                dict["#:cross_1m:"] = "_________";
                                break;
                            case 3:
                                dict["#:cross_3m:"] = "_________";
                                break;
                        }

                        // серия и номер
                        dict.Add("#:print_ser:", te_serial.Text);
                        dict.Add("#:print_num:", te_number.Text);

                        // дата от
                        dict.Add("#:print_date_vp:",
                            string.Format("{0} {1} {2}", de_out_date.DateTime.Date, de_out_date.DateTime.Month,
                                de_out_date.DateTime.Year));
                        dict.Add("#:print_date_vp_ss:", de_out_date.Text);

                        // серия и номер полиса
                        dict.Add("#:print_ser_pol:", _patient.Policy.Serial);
                        dict.Add("#:print_num_pol:", _patient.Policy.Number);

                        // снилс
                        dict.Add("#:print_snils:", _patient.Snils);

                        // адрес (номер карты?)
                        dict.Add("#:print_patient_addr:", _patient.FactAddress.Text);

                        // данные о враче
                        dict.Add("#:print_doc_fio:", "");
                        dict.Add("#:print_doc_kod:", "");

                        // данные о препарате
                        dict.Add("#:print_rp:", medicament.Mnn);
                        dict.Add("#:print_dtd:", medicament.FormName);
                        dict.Add("#:print_doza:", medicament.Doze);
                        dict.Add("#:print_kol_vo:", spin_count.Text);

                        dict.Add("#:print_signa:", cmb_signa.Text);

                        // данные о пациенте
                        dict.Add("#:print_patient_fio:",
                            string.Format("{0} {1} {2}", _patient.LastName, _patient.FirstName, _patient.MidName));
                        dict.Add("#:print_patient_db:",
                            string.Format("{0} {1} {2}", _patient.BirthDate.Date, _patient.BirthDate.Month,
                                _patient.BirthDate.Year));

                        var recipt = GetRecipe();

                        //var image = f.GetBarCodeAsBitmap();
                        //document.AddImage(0, image);
                        document.FillDocument(dict);

                        var pdfPath = Path.Combine(Environment.CurrentDirectory, "temp.pdf");
                        PdfGenerator.Generate(document, pdfPath);

                        //SendToPrinter(path);
                    }*/
                    #endregion
                    
                    var recipe = GetRecipe();
                    if (recipe != null)
                    {
                        var message = string.Empty;
                        if (!recipe.CanSave(Operator, out message))
                        {
                            XtraMessageBox.Show(message, "Валидация", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(recipe.Number))
                            {
                                //TODO: 
                                RecipePrinter.Print(recipe);
                            }
                            else
                                XtraMessageBox.Show("Для печати рецепта, сначала зарегистрируйте его!");
                        }
                    }
                }
            }
        }

        private void SendToPrinter(string path)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = path;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }

        private void getMainVisitGroupInfo(long visitId)
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetTemplates");
                query.Sql = "select d.fam, d.nam, d.mid, v.target_pos_id, v.mesto_obs, skl_kard " +
                        "from visit_tab v " +
                        "left join doctor_tab dd on dd.doctor_id = v.doctor_id " +
                        "left join dan_tab d on dd.dan_id = d.dan_id " +
                        "where v.visit_id = @visit_id;";
                query.AddParamWithValue("@visit_id", visitId);

                var result = dbWorker.GetResult(query);

                if (result != null && result.Fields.Count > 0)
                {
                    
                }

            }
        }

        private Recipe GetRecipe()
        {
            var recipe = new Recipe();

            var encoding = new ASCIIEncoding();

            var ogrn = 1020300979071;
            var filter = cmb_medicaments.Text;
            var sb = new StringBuilder();
            
            //var doctor = new Doctor();
            //doctor.LoadData(_visit.DoctorId);

            recipe.Serial = te_serial.Text;

            if (!string.IsNullOrEmpty(te_number.Text) && te_number.Text.Equals("0"))
            {
                recipe.Number = string.Empty;
            }
            else
            {
                recipe.Number = te_number.Text;
            }

            if (recipeDoctorId!=0)
                recipe.DoctorId = recipeDoctorId;
            else
                recipe.DoctorId = Operator.DoctorId;

            //TODO: выяснить откуда брать врача который работает в проге
            recipe.PatientId = _visit.PatientId;
            recipe.TalonId = _visit.TalonId;

            recipe.InsertInfoDate = de_insert_date.DateTime;

            recipe.MkbCode = cmb_mkb.Text;

            if (cmb_revenue_type.SelectedIndex > 0)
                recipe.RevenueSource = (RevenueType)cmb_revenue_type.SelectedIndex;

            if (cmb_payment_percentage.SelectedIndex > 0)
                recipe.PayPercent = (PayPercent)cmb_payment_percentage.SelectedIndex - 1;

            recipe.IsTradeName = chk_order_by_trade.Checked;

            var medicament =
                    SelectedMedicament;
            if (medicament != null)
            {
                var code = long.Parse(medicament.Code);
                recipe.MedicamentId = medicament.Id;
                recipe.MedicamentFedCode = code;
                recipe.MedicamentName = chk_order_by_trade.Checked ? medicament.Name : medicament.Mnn;
                recipe.MedicamentDtd = medicament.FormName;
                recipe.Doze = medicament.Doze;
            }

            recipe.Signa = cmb_signa.Text;
            
            var count = (int)spin_count.Value;
            recipe.Count = count;

            recipe.BenefitCode = cmb_lgot_category.Text;

            var selectedTime = cmb_valid_date.SelectedIndex;

            switch (selectedTime)
            {

                case 0:
                    recipe.Validity = 15;
                    break;
                case 1:
                    recipe.Validity = 30;
                    break;
                case 2:
                    recipe.Validity = 90;
                    break;
            }

            recipe.DischargeDate = de_out_date.DateTime;
            recipe.IsVK = chk_order_through_VK.Checked;
            return recipe;
        }

        private void cmb_lgot_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lgota = _patient.Lgotas.FirstOrDefault(t => t.LgotaCode.Equals(cmb_lgot_category.Text));

            if (lgota != null)
            {
                if (lgota is FederalLgota)
                {
                    cmb_revenue_type.Text = FederalSource;
                }
                if (lgota is LocalLgota)
                {
                    cmb_revenue_type.Text = RegionSource;
                }
            }
        }

        private void cmb_medicaments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var medicament = SelectedMedicament;
            if (medicament != null)
            {
                cmb_signa.Text = medicament.Signa;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Register();
        }

        
        private bool IsLSChanged()
        {
            return  _selectedLSId != SelectedMedicament.Code;
        }
        
        private bool Register()
        {
            var recipe = GetRecipe();
            if (_recipeId.HasValue)
                recipe.Id = _recipeId.Value;
            
            //Если поменялось ЛС то зааодим новый рецепт
            if (IsLSChanged())
            {
                recipe.Id = default(long);
                recipe.Number = null;
            }
            
            if (recipe.DoctorId==0)
                recipe.DoctorId = Operator.DoctorId;

            var message = string.Empty;
            if (recipe.CanSave(Operator, out message))
            {
                
                if (recipe.Id!=default(long) && !IsLSChanged())
                {
                    var result = XtraMessageBox.Show("Создать новый рецепт?", "Подтверждение", MessageBoxButtons.YesNo);
                    if (result==DialogResult.Yes)
                    {
                        recipe.Id = default(long);
                        recipe.Number = null;
                    }
                }
                
                if (recipe.IsVK)
                {
                    var result = XtraMessageBox.Show("Вы регистрируете рецепт через ВК! Продолжить?", "Подтверждение",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                }

                var dayCount = recipe.GetPatientRecipeCount(0);
                var monthCount = recipe.GetPatientRecipeCount(1);
                if (dayCount >= 4)
                {
                    var result = XtraMessageBox.Show("Этому пациенту уже было выписано 4 рецепта в указанный день.\n" +
                                        "Количество выписанных рецептов за день: " + dayCount +
                                        "\nВы действительно хотите выписать ему еще один рецепт?", "",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                }
                if (monthCount >= 10)
                {
                    var result = XtraMessageBox.Show("Этому пациенту уже было выписано 4 рецепта в указанный месяц.\n" +
                                        "Количество выписанных рецептов за месяц: " + dayCount +
                                        "\nВы действительно хотите выписать ему еще один рецепт?", "",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                }

                //if (!string.IsNullOrEmpty(recipe.Number))
                //{
                //    var result = XtraMessageBox.Show("Выписать новый рецепт?", "",
                //        MessageBoxButtons.YesNo);
                //    if (result == DialogResult.No)
                //    {
                //        return false;
                //    }
                //    else
                //    {
                //        recipe.Number = null;
                //    }
                //}

                recipe.Save(Operator);
                te_number.Text = recipe.Number;
            }
            else
            {
                XtraMessageBox.Show(message);
                return false;
            }

            return true;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            long? recipeId = null;
            using (var f = new RecipeEditListForm(this._patient.Id))
            {
                var result = f.ShowDialog();
                if (result == DialogResult.OK)
                {
                    recipeId = f.SelectedRecipeId;
                }
            }
            if (!recipeId.HasValue)
                return;
            _recipeId = recipeId.Value; 
            var recipe = new Recipe();
            recipe.LoadData(recipeId.Value);
            recipeDoctorId = recipe.DoctorId;
            InitForm(recipe);
            
            //this.Refresh();
            //Application.DoEvents();
            
        }

        private void cmb_mkb_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (cmb_mkb.Text.Contains("E1") || cmb_mkb.Text.Contains("O24.4"))
            {
                var diabetLS = _medicaments.Where(m=>m.DiabetCode.HasValue && m.DiabetCode.Value!=0)
                               .Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName))
                               .ToList();
                cmb_medicaments.Properties.Items.Clear();
                cmb_medicaments.Properties.Items.AddRange(diabetLS);
            }
            else if (cmb_medicaments.Properties.Items.Count!=_medicaments.Count)
            {
                cmb_medicaments.Properties.Items.Clear();
                cmb_medicaments.Properties.Items.AddRange(
                    _medicaments
                    .Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName))
                    .ToList()
                    );
            }
        }
    }

    
}
