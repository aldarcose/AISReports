using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using SharedDbWorker;

namespace Model.Classes.Reestr
{
    public class Converter
    {
        public void UpdateReestr()
        {
            using(var db=new DbWorker())
            {
                var result = db.Connection.Execute("select usl_reestr_update_all()");
            }
        }
    }
}
