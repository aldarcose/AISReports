using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes
{
    public abstract class Lgota
    {
        private string _lgotaCode;

        protected Lgota()
        {
            PatientId = -1;
            LgotaCode = string.Empty;
            DateStart = null;
            DateEnd = null;
        }

        /// <summary>
        /// Идентификатор пациента
        /// </summary>
        [Display(AutoGenerateField = false)]
        public long PatientId { get; set; }

        /// <summary>
        /// Код льготы
        /// </summary>
        [Display(Name = "Код льготы")]
        public string LgotaCode
        {
            get { return _lgotaCode; }
            set
            {
                _lgotaCode = value;
                _lgotaName = GetLgotaName();
            }
        }

        private string _lgotaName;
        [Display(Name = "Название")]
        public string LgotaName
        {
            get { return _lgotaName; }
        }

        /// <summary>
        /// Дата начала действия льготы
        /// </summary>
        [Display(Name = "Дата начала")]
        public DateTime? DateStart { get; set; }
        [Display(Name = "Дата окончания")]
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Проверяет является ли льгота действительной на момент date
        /// </summary>
        public bool IsValid(DateTime date)
        {
            return DateEnd.HasValue && DateEnd.Value <= date;
        }

        private string GetLgotaName()
        {
            var lgota = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRiskName");
                q.Sql = "select name from codifiers.lgota_tab where code = @code;";
                q.AddParamWithValue("code", LgotaCode);

                lgota = DbResult.GetString(db.GetScalarResult(q), string.Empty);
            }
            return lgota;
        }
    }

    /// <summary>
    /// Федеральная льгота
    /// </summary>
    public class FederalLgota : Lgota, ILoadData
    {
        public FederalLgota() : base()
        {
        }
        public string Snils { get; set; }
        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            IsLoaded = false;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            IsLoaded = true;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            OnLoading();

            throw new NotImplementedException();

            OnLoaded();
        }

        public bool LoadData(DbResult result)
        {
            OnLoading();
            var loadResult = false;

            try
            {
                if (PatientId == -1)
                {
                    PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), -1);
                }

                LgotaCode = DbResult.GetString(result.GetByName("floga"), string.Empty);
                DateEnd = DbResult.GetNullableDateTime(result.GetByName("date_end"));
                Snils = DbResult.GetString(result.GetByName("pens"), string.Empty);

                loadResult = true;
            }
            catch (Exception)
            {
                loadResult = false;
            }

            if (loadResult)
                OnLoaded();
            else
            {
                IsLoading = false;
            }

            return loadResult;
        }
    }

    /// <summary>
    /// Местная (субъект РФ) льгота
    /// </summary>
    public class LocalLgota : Lgota, ILoadData
    {
        public LocalLgota()
            : base()
        {
        }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            IsLoaded = false;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            IsLoaded = true;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            OnLoading();

            throw new NotImplementedException();

            OnLoaded();
        }

        public bool LoadData(DbResult result)
        {
            OnLoading();
            var loadResult = false;

            try
            {
                if (PatientId == -1)
                {
                    PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), -1);
                }

                LgotaCode = DbResult.GetString(result.GetByName("lgota"), string.Empty);

                loadResult = true;

            }
            catch (Exception)
            {
                loadResult = false;
            }

            if (loadResult)
                OnLoaded();
            else
            {
                IsLoading = false;
            }

            return loadResult;
        }
    }
}
