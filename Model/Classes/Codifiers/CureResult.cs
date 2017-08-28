using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Результат лечения (result_tab)
    /// </summary>
    public class CureResult : ILoadData
    {
        public CureResult()
        {
            IsLoading = false;
            IsLoaded = false;
            Id = -1;
            Name = string.Empty;
            _End = 0;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public int Reestr { get; set; }
        public int _End { get; set; }
        public int PatientStateId { get; set; }
        public int Rslt { get; set; }
        public int Ishod { get; set; }

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
            Id = id;
            var loadResult = false;

            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetCureResult");
                q.Sql = "select * from public.result_tab where result_id = @id;";
                q.AddParamWithValue("@id", id);
                var result = db.GetResult(q);
                if (result != null)
                {
                    Name = DbResult.GetString(result.GetByName("result"), "");
                    Reestr = (int)DbResult.GetNumeric(result.GetByName("reestr"), -1);
                    _End = (int)DbResult.GetNumeric(result.GetByName("_end"), 0);
                    PatientStateId = (int)DbResult.GetNumeric(result.GetByName("patient_state_id"), -1);
                    Rslt = (int)DbResult.GetNumeric(result.GetByName("reestr_rslt"), -1);
                    Ishod = (int)DbResult.GetNumeric(result.GetByName("reestr_ishod"), -1);
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
