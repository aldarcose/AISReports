using Model.Interface;
using System;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;
using System.Linq;

namespace Model.Classes.Documents
{
    public class Policy : BaseDocument, IValidatable
    {
        private long _smoId;

        public Policy ()
        {
            this.Type = new PolicyType();
        }
        public PolicyType Type { get; set; }

        public DateTime? DateEnd { get; set; }

        public long SmoId
        {
            get { return _smoId; }
            set
            {
                _smoId = value;
                SmoRegionCode = GetSmoRegionCode();
            }
        }

        public string SmoRegionCode { get; private set; }

        public bool Validate(out string errorMessage)
        {
            errorMessage = "";

            return true;
        }

        private string GetSmoRegionCode()
        {
            var code = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRegionCode");
                q.Sql = "select region_id from codifiers.strcom_tab where id = @id;";
                q.AddParamWithValue("id", SmoId);

                code = DbResult.GetString(db.GetScalarResult(q), string.Empty);
            }
            return code;
        }

        public String SmoName
        {
            get
            {
                using(var db = new DbWorker())
                {
                    var result = db.Connection.Query<String>("select name from codifiers.strcom_tab where id = @Id", 
                        new { Id = SmoId }).FirstOrDefault();
                    return result;
                }
            }
        }

        public String SmoRegion
        {
            get
            {
                using (var db = new DbWorker())
                {
                    var sql = @"select k.full_name from codifiers.strcom_tab s inner join codifiers.sp_kladr_tab k on k.code=s.region_id where s.id = @Id";
                    var result = db.Connection.Query<String>(sql,
                        new { Id = SmoId }).FirstOrDefault();
                    return result;
                }
            }
        }
    }
}
