using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Classes.Reestr;
using System.Xml.Linq;
using SharedDbWorker;
using Dapper;


namespace Model.Classes.Reestr
{
    public class Nonresident : Services
    {

        public Nonresident(DateTime startDate, DateTime endDate): base (startDate, endDate)
        {

        }

        protected override XDocument GetXML(DateTime startDate, DateTime endDate)
        {
            using (var db = new DbWorker())
            {
                var sql = "select usl_reestr_inogorod(@StartDate::date, @EndDate::date, 0, null::varchar)";
                var result = db.Connection.Query<string>(sql, new { startDate = startDate, EndDate = endDate }).FirstOrDefault();
                if (result == null)
                    return null;
                var doc = XDocument.Parse(result);
                var sluchCount = doc.Descendants("SLUCH").Count();
                var filename = doc.Descendants("FILENAME").First();
                filename.Value = dmFilename;
                var caseCount = doc.Descendants("SLUCH").Count();
                filename.AddAfterSelf(new XElement("SD_Z", caseCount));
                return doc;
            }
        }


    }
}
