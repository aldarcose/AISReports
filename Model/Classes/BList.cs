using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes
{
    /// <summary>
    /// Больничный лист (blist_tab)
    /// </summary>
    public class BList : ILoadData
    {
        public BList()
        {
            Cause = new BListCause();
        }

        /// <summary>
        /// Ид больничного листа
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Ид талона
        /// </summary>
        public long TalonId { get; set; }
        /// <summary>
        /// Дата открытия
        /// </summary>
        public DateTime DateBeg { get; set; }
        /// <summary>
        /// Дата закрытия
        /// </summary>
        public DateTime? DateEnd { get; set; }
        /// <summary>
        /// Ид доктора, закрывшего больничный лист
        /// </summary>
        public long CloseDoctorId { get; set; }

        /// <summary>
        /// Ид доктора, открывшего больничный лист
        /// </summary>
        public long OpenDoctorId { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public BListCause Cause { get; set; }


        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
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

            var loadResult = false;

            Id = id;
            var q = new DbQuery("GetBlist");
            q.Sql = @"
                      SELECT b.*, p.prichina_id as prich_id, p.nam_prichina as prich_name, p.code as prich_code 
                      FROM blist_tab b 
                      LEFT JOIN prichina_tab p ON b.prichina_id = p.prichina_id 
                      WHERE b.blist_id = @id limit 1;";
            q.AddParamWithValue("@id", id);
            using (var db = new DbWorker())
            {
                var x = db.GetResult(q);
                if (x != null && x.Fields.Count > 0)
                {
                    TalonId = DbResult.GetNumeric(x.GetByName("blist_id"), -1);
                    DateBeg = DbResult.GetDateTime(x.GetByName("data_begin"), DateTime.Now);
                    DateEnd = DbResult.GetNullableDateTime(x.GetByName("data_end"));
                    OpenDoctorId = DbResult.GetNumeric(x.GetByName("doctor_id"), -1);
                    CloseDoctorId = DbResult.GetNumeric(x.GetByName("doctor_id_closed"), -1);
                    Cause.Id = DbResult.GetNumeric(x.GetByName("prich_id"), -1);
                    Cause.Name = DbResult.GetString(x.GetByName("prich_name"), "");
                    Cause.Code = DbResult.GetString(x.GetByName("prich_code"), "");
                    Comment = DbResult.GetString(x.GetByName("comment"), "");
                    loadResult = true;
                }
            }
            
            IsLoaded = loadResult;
            OnLoaded();

            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }
    }
}
