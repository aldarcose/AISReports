using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.Classes;
using Model;
using DbCaching;



namespace ARMPlugin.FormsAndControls
{
    public partial class VisitRecipesControl : UserControl
    {

        public long? TalonId { get; set; }
        private Visit _visit =null;
        public Visit Visit { 
            get{
                return _visit;
            }
            set {
                _visit = value;
                if (_visit!=null)
                {
                    ReloadGridRecipes();
                    ReloadGridLS();
                }
                    
               
            }
        }
        public long? OperatorId { get; set; }

        public VisitRecipesControl()
        {
            InitializeComponent();
        }

        public void InitForm(long? operatorId, Visit visit)
        {
            Visit = visit;
            OperatorId = operatorId;
        }


        public void ReloadGridRecipes()
        {
            var _medicaments = new List<Medicament>();
            
            if (DbCache.GetItem("medicaments") == null)
            {
                _medicaments = CodifiersHelper.GetMedicaments();
                DbCache.SetItem("medicaments", _medicaments, 3600);
            }
            else
            {
                _medicaments = DbCache.GetItem("medicaments") as List<Medicament>;
            }

            var repo = new RecipeRepository();
            var recipes = repo.GetRecipesByVisit(Visit);
            foreach (var r in recipes)
            {
                r.MedicamentName = _medicaments.Where(s=>s.Code==r.MedicamentFedCode.ToString()).Select(s => s.Mnn).FirstOrDefault();
            }

            gridRecipes.DataSource = recipes;
        }

        public void ReloadGridLS()
        {
            var repo = new DrugRepository();
            var drugs = repo.GetByPatient(Visit.PatientId, Visit.Id);
            gridLS.DataSource = drugs;
        }

        private void btnAddLS_Click(object sender, EventArgs e)
        {
            if (Visit==null)
            {
                MessageBox.Show("Выберите посещение", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            using(var form = new VisitDrugForm(OperatorId, Visit))
            {
                var result=form.ShowDialog();
            }
            ReloadGridLS();
        }

        private void btnEditLS_Click(object sender, EventArgs e)
        {

            if (SelectedDrug == null) return;
            
            using (var form = new VisitDrugForm(OperatorId,Visit,SelectedDrug))
            {
                var result = form.ShowDialog();
            }

        }

        private void btnDeleteLS_Click(object sender, EventArgs e)
        {
            if (SelectedDrug == null) return;

            var repo = new DrugRepository();
            repo.Delete(SelectedDrug.Id);
            ReloadGridLS();
        }

        private void gridViewLS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var drug = gridViewLS.GetFocusedRow() as Drug;
            SelectedDrug = drug;
        }

        private Drug SelectedDrug { get; set; }

        
    }
}
