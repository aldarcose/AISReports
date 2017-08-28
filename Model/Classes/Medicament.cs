using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes
{
    public class Medicament : ILoadData, ISaveable
    {
        public long Id { get; set; }


        public string Name { get; set; }
        
        public string Code { get; set; }

        /// <summary>
        /// Ид торгового названия
        /// </summary>
        public long TrnmId { get; set; }

        /// <summary>
        /// Торговое название (лат)
        /// </summary>
        public string Trnm { get; set; }

        /// <summary>
        /// Торговое название (рус)
        /// </summary>
        public string TrnmRus { get; set; }

        /// <summary>
        /// Ид категории
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// Ид международного непатентованного названия
        /// </summary>
        public long MnnId { get; set; }
        /// <summary>
        /// Международное непатентованное название (лат)
        /// </summary>
        public string Mnn { get; set; }
        /// <summary>
        /// Международное непатентованное название (рус)
        /// </summary>
        public string MnnRus { get; set; }

        /// <summary>
        /// Ид лекарственной формы
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// Название лекарственной формы
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Ид фармокологической группы
        /// </summary>
        public long FarmGroupId { get; set; }

        /// <summary>
        /// Доза в текстовом представлении
        /// </summary>
        public string Doze { get; set; }

        /// <summary>
        /// Идентификатор дозы
        /// </summary>
        public long DozeId { get; set; }

        /// <summary>
        /// Объем дозы
        /// </summary>
        public string VDoze { get; set; }

        /// <summary>
        /// Способ применения препарата
        /// </summary>
        public string Signa { get; set; }

        /// <summary>
        /// Цена препарата
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string Producer { get; set; }
        /// <summary>
        /// Страна-производитель
        /// </summary>
        public string ProduceCountry { get; set; }

        /// <summary>
        /// Дата начала действия по фед. льготе
        /// </summary>
        public DateTime FedBenefitDateBeg { get; set; }
        /// <summary>
        /// Дата окончания действия по фед. льготе
        /// </summary>
        public DateTime FedBenefitDateEnd { get; set; }

        public bool IsDiabet { get { return DiabetCode.HasValue; } }

        /// <summary>
        /// Код ЛС для РИАС (Сахарынй диабет)
        /// </summary>
        public long? DiabetCode { get; set; } 


        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMedicaments");
                query.Sql = "SELECT p.id, p.name, p.code, " +
                            "p.sp_trnm_id, tr.name_lat AS nam_lat, tr.name AS nam_rus, " +
                            "p.sp_mnn_id, mnn.name_lat AS mnn_lat, mnn.name AS mnn_rus, " +
                            "p.sp_lf_id, lf.name as dtd, p.name_fct, p.name_cnf, p.doza, p.signa, p.price, p.sp_doza_id, p.d_ls, p.kod_rias " +
                            "FROM codifiers.preparat_tab p " +
                            "LEFT JOIN codifiers.sp_lf_tab lf ON lf.id = p.sp_lf_id " +
                            "LEFT JOIN codifiers.sp_trnm_tab tr ON tr.id = p.sp_trnm_id " +
                            "LEFT JOIN codifiers.sp_mnn_tab mnn ON mnn.id = p.sp_mnn_id where p.id = :id;";
                query.AddParamWithValue("id", id);
                var results = dbWorker.GetResult(query);
                if (results != null)
                {
                    this.LoadData(results);

                    return true;
                }
            }
            return false;
        }

        public bool LoadData(DbResult result)
        {
            if (result == null)
                return false;

            this.Id = DbResult.GetNumeric(result.GetByName("id"), -1);
            this.Name = DbResult.GetString(result.GetByName("name"), "");
            this.Code = DbResult.GetString(result.GetByName("code"), "");
            this.TrnmId = DbResult.GetNumeric(result.GetByName("sp_trnm_id"), -1);
            this.Trnm = DbResult.GetString(result.GetByName("nam_lat"), "");
            this.TrnmRus = DbResult.GetString(result.GetByName("nam_rus"), "");
            this.MnnId = DbResult.GetNumeric(result.GetByName("sp_mnn_id"), -1);
            this.Mnn = DbResult.GetString(result.GetByName("mnn_lat"), "");
            this.MnnRus = DbResult.GetString(result.GetByName("mnn_rus"), "");
            this.FormId = DbResult.GetNumeric(result.GetByName("sp_lf_id"), -1);
            this.FormName = DbResult.GetString(result.GetByName("dtd"), "");
            this.Producer = DbResult.GetString(result.GetByName("name_fct"), "");
            this.ProduceCountry = DbResult.GetString(result.GetByName("name_cnf"), "");
            this.Doze = DbResult.GetString(result.GetByName("doza"), "");
            this.VDoze = DbResult.GetString(result.GetByName("d_ls"), "");
            this.Signa = DbResult.GetString(result.GetByName("signa"), "");
            this.Price = DbResult.GetDecimal(result.GetByName("price"), 0);
            this.DozeId = DbResult.GetNumeric(result.GetByName("sp_doza_id"), -1);
            this.DiabetCode = DbResult.GetNumeric(result.GetByName("kod_rias"), 0);
            return true;
        }

        public event EventHandler Saved;
        public event EventHandler Saving;
        public bool IsSaved { get; set; }
        public void OnSaving()
        {
            if (Saving != null)
            {
                Saving(this, null);
            }
        }

        public void OnSaved()
        {
            if (Saved != null)
            {
                Saved(this, null);
            }
        }

        public bool CanSave(Operator @operator, out string message)
        {
            // проверяем что все есть!
            message = string.Empty;
            return true;
        }

        public void Save(Operator @operator)
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("CreateRecipe");
                q.Sql = "Insert into public.rezept_tab ();";
                q.AddParamWithValue("", null);

                db.Execute(q);
            }
        }
    }
}
