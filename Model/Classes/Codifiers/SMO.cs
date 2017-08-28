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
    /// Страховая мед. организация
    /// </summary>
    public class SMO : ILoadData
    {
        public SMO()
        {
            IsLoading = false;
            IsLoaded = false;
        }
        public SMO(long id)
        {
            Id = id;
        }
        public long Id { get; private set; }

        public string Name { get; set; }

        public string RegionId { get; set; }
        public string RegionName { get; set; }

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

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetSmo");
                query.Sql = "Select smo.name, smo.region_id, kladr.name " +
                            "from codifiers.strcom_tab smo " +
                            "left join codifiers.sp_kladr_tab kladr on smo.region_id = kladr.code " +
                            "where smo.id = @id and smo.name not like '*%'" +
                            "order by smo.name";
                query.AddParamWithValue("@id", Id);

                var result = dbWorker.GetResult(query);

                if (result != null && result.Fields.Count > 0)
                {
                    this.Name = DbResult.GetString(result.Fields[0], "");
                    this.RegionId = DbResult.GetString(result.Fields[1], "");
                    this.RegionName = DbResult.GetString(result.Fields[2], "");
                }

                loadResult = true;
            }
            
            IsLoaded = loadResult;
            OnLoaded();
            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        public string GetTFomsCode()
        {
            var code = string.Empty;
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetTFomsCode");
                query.Sql = "Select tfoms_code " +
                            "from codifiers.strcom_tab " +
                            "where id = @id and name not like '*%'" +
                            "order by name";
                query.AddParamWithValue("@id", Id);

                var result = dbWorker.GetResult(query);

                if (result != null && result.Fields.Count > 0)
                {
                    code = DbResult.GetString(result.Fields[0], "");
                }
            }

            return code;
        }
    }
}
