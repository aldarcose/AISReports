using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.Classes.Codifiers;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Referral
{
    public class ReferralLpu : MO
    {
        private List<ReferralLpuDepartment> _departments;
        
        //public List<ReferralService> Services { get; private set; }

        public List<ReferralLpuDepartment> Departments
        {
            get
            {
                if (_departments == null)
                {
                    GetDepartments();
                }
                return _departments;
            }
        }

        

        private void GetDepartments()
        {
            _departments = new List<ReferralLpuDepartment>();

            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetLpuDeps");
                q.Sql =
                    "select id, code, name from codifiers.sp_lpu_otdels_tab where pricpol = @code and name not like '*%' order by 3;";
                q.AddParamWithValue("code", Code);

                var results = db.GetResults(q);
                foreach (var dbResult in results)
                {
                    var dep = new ReferralLpuDepartment()
                    {
                        Id = DbResult.GetNumeric(dbResult.GetByName("id"), -1),
                        Code = DbResult.GetString(dbResult.GetByName("code"), string.Empty),
                        Name = DbResult.GetString(dbResult.GetByName("name"), string.Empty)
                    };
                    if (dep.Id != -1)
                    {
                        _departments.Add(dep);
                    }
                }
            }
        }

        public override string ToString()
        {
            var name = string.Format("{0}-{1}", Code, string.IsNullOrEmpty(FullName) ? Name : FullName);
            return name;
        }
    }

    public class ReferralLpuDepartment
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    
}
