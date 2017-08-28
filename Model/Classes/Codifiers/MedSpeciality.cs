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
    public class MedSpeciality : ILoadData
    {
        public MedSpeciality()
        {
            
        }

        public MedSpeciality(long id)
        {
            
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

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
            var loadResult = true;
            Id = id;

            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDoctorSpeciality");
                q.Sql = "Select * from public.sp_spec_doctor_tab wher id = @id";
                q.AddParamWithValue("@id", id);
                var result = db.GetResult(q);
                if (result != null && result.Fields.Count > 0)
                {
                    Name = DbResult.GetString(result.GetByName("name"), "");
                    Code = DbResult.GetString(result.GetByName("code"), "");
                }
            }

            IsLoaded = loadResult;
            OnLoaded();
            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            OnLoading();
            Id = DbResult.GetNumeric(result.GetByName("id"), -1);
            Name = DbResult.GetString(result.GetByName("name"), "");
            Code = DbResult.GetString(result.GetByName("code"), "");

            if (Id != -1)
            {
                OnLoaded();
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
