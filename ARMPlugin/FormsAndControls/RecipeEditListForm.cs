using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Model.Classes;
using SharedDbWorker;
using SharedDbWorker.Classes;
using ImageList = Model.Classes.ImageList;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ARMPlugin.FormsAndControls
{
    public partial class RecipeEditListForm : XtraForm
    {
        private List<RecipeInfo> recipeInfos;
        long PatientId { get; set; }
        public RecipeEditListForm(long patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            GetRecipes();
            gridView1.OptionsBehavior.Editable = false;
        }

        public long? SelectedRecipeId { get; private set; }

        private void GetRecipes()
        {
            recipeInfos = new List<RecipeInfo>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRecipeInfos");
                q.Sql =
                    "select r.rezept_id, r.rezept_ser, r.rezept_num, r.date_vipiski, r.date_otpusk, r.diagn_id, r.lekarstv_id, r.bad, d.fio " +
                    "from public.rezept_tab r inner join full_doctor_view2 d on r.doctor_id = d.full_doctor_id " +
                    "where r.dan_id = @id";
                q.AddParamWithValue("id", PatientId);
                var res = db.GetResults(q);
                foreach (var dbResult in res)
                {
                    var rec = new RecipeInfo(DbResult.GetBoolean(dbResult.GetByName("bad"), false));
                    rec.Id = DbResult.GetNumeric(dbResult.GetByName("rezept_id"), 0);
                    rec.Serial = DbResult.GetString(dbResult.GetByName("rezept_ser"), "");
                    rec.Number = DbResult.GetString(dbResult.GetByName("rezept_num"), "");
                    rec.DischargeDate = DbResult.GetDateTime(dbResult.GetByName("date_vipiski"), DateTime.Now);
                    rec.ReleaseDate = DbResult.GetNullableDateTime(dbResult.GetByName("date_otpusk"));
                    rec.MkbCode = DbResult.GetString(dbResult.GetByName("diagn_id"), "");
                    rec.MedicamentName = DbResult.GetString(dbResult.GetByName("lekarstv_id"), "");
                    rec.DoctorFio = DbResult.GetString(dbResult.GetByName("fio"), "");

                    recipeInfos.Add(rec);
                }
            }

            grid_recipes.DataSource = recipeInfos;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            var recipeInfo = gridView1.GetFocusedRow();

            if (recipeInfo is RecipeInfo)
            {
                (recipeInfo as RecipeInfo).DeleteRestore();
                gridView1.UpdateCurrentRow();
            }

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            var recipeInfo = gridView1.GetFocusedRow();

            if (recipeInfo is RecipeInfo)
            {
                var recipe = (recipeInfo as RecipeInfo).GetRecipe();
                RecipePrinter.Print(recipe);
                //var rrr = new RecipeReport_okud3108805(recipe);
                //rrr.CreateDocument();
                //Image image = null;
                //using (var stream = new MemoryStream())
                //{
                //    rrr.ExportToImage(stream, ImageFormat.Png);
                //    image = Image.FromStream(stream);
                //}

                //var cccc = new RecipePrintConfirmation();
                //if (cccc.ShowDialog() == DialogResult.OK)
                //{
                //    if (cccc.Count == 1)
                //    {
                //        rrr.ShowPreview();
                //    }
                //    else
                //    {
                //        Model.Classes.ImageList list = new ImageList();
                //        for (int i = 0; i < cccc.Count; i++)
                //        {
                //            list.Images.Add(new ImageContainer()
                //            {
                //                Image = image
                //            });
                //        }
                //        var zzz = new ImageReport(list);
                //        zzz.ShowPreview();
                //    }
                //}
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Point point = gridView1.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(gridView1, point);
            
        }

        public void DoRowDoubleClick(GridView view, Point point)
        {
            GridHitInfo info = view.CalcHitInfo(point);
            if (info.InRow || info.InRowCell)
            {
                var recipe = view.GetFocusedRow();
                this.SelectedRecipeId = ((RecipeInfo)recipe).Id;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
    public class RecipeInfo
    {
        public RecipeInfo(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
        [Display(AutoGenerateField = false)]
        public long Id { get; set; }
        [DisplayName("Серия")]
        public string Serial { get; set; }
        [DisplayName("Номер")]
        public string Number { get; set; }
        [DisplayName("Дата выписки")]
        public DateTime DischargeDate { get; set; }
        [DisplayName("Дата отпуска")]
        public DateTime? ReleaseDate { get; set; }
        [DisplayName("Мкб")]
        public string MkbCode { get; set; }
        [DisplayName("Медикамент")]
        public string MedicamentName { get; set; }
        [DisplayName("Доктор")]
        public string DoctorFio { get; set; }
        [DisplayName("Удален")]
        public bool IsDeleted { get; private set; }

        public void DeleteRestore()
        {
            IsDeleted = !IsDeleted;
            DeleteChanged();
        }

        private void DeleteChanged()
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("UpdateRecipe");
                q.Sql = "update rezept_tab Set bad = @isDeleted where rezept_ser = @serial and rezept_num = @number;";
                q.AddParamWithValue("isDeleted", IsDeleted);
                q.AddParamWithValue("serial", Serial);
                q.AddParamWithValue("number", Number);
                db.Execute(q);
            }
        }

        public Recipe GetRecipe()
        {
            var recipe = new Recipe();
            recipe.LoadData(Id);
            var medicament = new Medicament();
            medicament.LoadData(recipe.MedicamentId);
            var code = long.Parse(medicament.Code);
            recipe.MedicamentId = medicament.Id;
            recipe.MedicamentFedCode = code;
            recipe.MedicamentName = recipe.IsTradeName ? medicament.Name : medicament.Mnn;
            recipe.MedicamentDtd = medicament.FormName;
            recipe.Doze = medicament.Doze;
            return recipe;
        }
    }
}
