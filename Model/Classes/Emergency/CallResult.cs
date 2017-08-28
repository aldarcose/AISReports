using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers.Emergency;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Emergency
{
    public class CallResult
    {
        public CallResult()
        {
            Type = new CallResultType();
            NoResultReasons = new List<NoResultReason>();
        }

        public CallResultType Type { get; set; }

        public bool IsNoResult
        {
            get
            {
                var noResultCode = "8"; // безрезультатаный выезд
                return !string.IsNullOrEmpty(Type.Code) && Type.Code.Equals(noResultCode);
            }
        }

        public List<NoResultReason> NoResultReasons { get; set; }

        public string GetNoResults()
        {
            var sb = new StringBuilder();
            foreach (var noResultReason in NoResultReasons)
            {
                sb.AppendFormat("{0};", noResultReason.Id);
            }
            return sb.ToString();
        }

        public string GetNoTextResults()
        {
            var sb = new StringBuilder();
            foreach (var noResultReason in NoResultReasons)
            {
                sb.AppendFormat("{0};", noResultReason.Name);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            if (!IsNoResult)
                return this.Type.ToString();

            return string.Format("{0}: {1}", this.Type.ToString(), GetNoTextResults());
        }

        public int GetRsltCode()
        {
            var rslt = 0;

            if (IsNoResult)
            {
                if (NoResultReasons.Count > 0)
                {
                    using (var db = new DbWorker())
                    {
                        var q = new DbQuery("GetRsltCode");
                        q.Sql = "select rslt_code from emergency.sp_call_result_tab where id = @id;";
                        q.AddParamWithValue("id", NoResultReasons[0].Id);
                        var code = db.GetScalarResult(q);
                        rslt = DbResult.GetInt(code, 0);
                    }

                }
            }
            else
            {
                using (var db = new DbWorker())
                {
                    var q = new DbQuery("GetRsltCode");
                    q.Sql = "select rslt_code from emergency.sp_call_result_tab where id = @id;";
                    q.AddParamWithValue("id", this.Type.Id);
                    var code = db.GetScalarResult(q);
                    rslt = DbResult.GetInt(code, 0);
                }
            }
            return rslt;
        }
    }
}
