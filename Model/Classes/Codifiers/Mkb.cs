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
    public class Mkb : ILoadData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ReestrCode { get; set; }

        public string DisplayText
        {
            get
            {
                return string.Format("({0}) {1}",Code,Name);
            }
        }

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
                var q = new DbQuery("GetMkb");
                q.Sql = "SELECT * FROM mkb_tab WHERE mkb_id = @id;";
                q.AddParamWithValue("@id", id);

                var result = db.GetResult(q);
                if (result != null && result.Fields.Count > 0)
                {
                    Name = DbResult.GetString(result.GetByName("mkb"), "");
                    Code = DbResult.GetString(result.GetByName("kod_d"), "");
                    ReestrCode = DbResult.GetString(result.GetByName("reestr_code"), "");
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

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
