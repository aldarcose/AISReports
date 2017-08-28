using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes
{
    public class Settings
    {
        public static string GetSettingValue(string settingName)
        {
            var value = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetSettingValue");
                q.Sql = "select value from settings_tab where name = @name;";
                q.AddParamWithValue("name", settingName);
                var result = db.GetScalarResult(q);
                if (result != null)
                {
                    value = DbResult.GetString(result, string.Empty);
                }
            }
            return value;
        }
    }
}
