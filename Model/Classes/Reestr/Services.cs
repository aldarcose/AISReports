using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharedDbWorker;
using Dapper;
using System.IO;

namespace Model.Classes.Reestr
{
    
    public class LabOrder
    {
      public long? NHistory {get;set;}
      public string LPU {get;set;} 
      public string LPU_1 {get;set;}
      public DateTime?  Date_1 {get;set;}
      public DateTime?  Date_2 {get;set;}
      public DateTime?  DateIn {get;set;}
      public DateTime?  DateOut {get;set;}
      public string IDSP {get;set;}
      public string CodeUsl {get;set;}
      public decimal? Tarif {get;set;}
      public decimal? SumvUsl {get;set;}
      public string PRVS {get;set;}
      public string CodeMD {get;set;}
      public string CommentU {get;set;}
      public long?  TableId {get;set;}
      public string SMO {get;set;}
      public long? IdPac {get;set;}
      public string NPolis {get;set;}
      public string  SPolis {get;set;}
      public string VPolis {get;set;}
      public string PrNov {get;set;}
      public int? Vidpom {get;set;}
      public string Diagnos {get;set;}
      public long? ResultD {get;set;}
      public string NprMO { get; set; }
      public string Profil { get; set; }
      public int? Det { get; set; }
      public string Novor { get; set; }
    }

    public class OrderSluch
    {
        public long? TableId { get; set; }
        public string VidPom { get; set; }
        public string LPU { get; set; }
        public string LPU_1 { get; set; }
        public string VBR { get; set; }
        public string NHistory { get; set; }
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public string Diagnos { get; set; }
        public string ResultD { get; set; }
        public string IDSP { get; set; }
        public string NprMO { get; set; }
        public string Profil { get; set; }
        public int? Det {get;set;}
        public string PRVS { get; set; }
        public string CodeMD { get; set; }
        public IEnumerable<LabOrder> UslList { get; set; }
    }
    
    public class Services
    {
        protected string dmFilename;
        protected string lmFilename;
        protected string version;
        protected DateTime startDate;
        protected DateTime endDate;


        public Services(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public XDocument DMDoc { get; private set; }
        public XDocument LMDoc { get; private set; }

        public void InitFiles()
        {
            dmFilename = GetFilename(FileType.USL_FILE, DateTime.Now, "1");
            DMDoc = GetXML(startDate, endDate);
            //выставляем экстренность у пустых случаев
            SetExtrCases();
            DeleteNullINV();
            DeleteUslDuplicates();
            var zapCounter = Convert.ToInt32( DMDoc.Descendants("ZAP").Last().Element("N_ZAP").Value);
            var sluchCounter = Convert.ToInt32( DMDoc.Descendants("SLUCH").Last().Element("IDCASE").Value);
            var uslCounter = Convert.ToInt32(DMDoc.Descendants("USL").Last().Element("IDSERV").Value);

            var labOrders = GetLabOrders(startDate, endDate, ++zapCounter, ++sluchCounter, ++uslCounter);
            var lastZap = DMDoc.Descendants("ZAP").Last();
            lastZap.AddAfterSelf(labOrders);

            lmFilename = GetFilename(FileType.PERS_FILE, DateTime.Now, "1");
            LMDoc = GetPersXML(DMDoc);
        }

        private void DeleteUslDuplicates()
        {
            var uslList = DMDoc.Descendants("USL").GroupBy(c => new { c.Element("COMENTU").Value }).Where(c => c.Skip(1).Any()).ToList();
            uslList.ForEach(x=>x.Skip(1).Remove());
        }

        
        private void DeleteNullINV()
        {
            var emptyZapList = DMDoc.Descendants("PACIENT").Where(x => x.Element("INV")!=null &&  x.Element("INV").IsEmpty).ToList();
            emptyZapList.ForEach(x => x.Element("INV").Remove());
        }

        protected virtual XDocument GetXML(DateTime startDate, DateTime endDate)
        {
            using(var db = new DbWorker())
            {
                var sql = "select get_usl_reestr_bur_bez_dd(@StartDate::date, @EndDate::date, 0, null::varchar, null::varchar)";
                var result = db.Connection.Query<string>(sql, new { startDate=startDate, EndDate=endDate }).FirstOrDefault();
                if (result==null)
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

        protected XDocument GetPersXML(XDocument uslFile)
        {
            var persIds = uslFile.Descendants("ID_PAC").Select(p=>p.Value);

            if (persIds == null || persIds.Count() == 0) return null;
            
            using(var db = new DbWorker())
            {
                var sql = string.Format(@"
                select d.dan_id id, d.fam lastname, d.nam firstname, d.mid midname, 
                d.date_born birthdate, 
                case when d.sex!='м' then 2 else 1 end gender
                from dan_tab d
                where dan_id in ({0})", 
                                            string.Join(",",persIds));
                var result = db.Connection.Query<Person>(sql);
                if(result==null) return null;
                var xml = new XDocument(
                            new XElement("PERS_LIST", 
                                new XElement("ZGLV", 
                                    new XElement("VERSION",2.1),
                                    new XElement("DATA", DateTime.Now.ToShortDateString()),
                                    new XElement("FILENAME", lmFilename)
                                )
                            )
                            );
                var list = xml.Element("PERS_LIST");
                foreach(var p in result)
                {
                    var pXml =
                        new XElement("PERS",
                            new XElement("ID_PAC", p.Id),
                            new XElement("FAM", p.LastName),
                            new XElement("IM", p.FirstName),
                            new XElement("OT",p.MidName),
                            new XElement("W",(byte)p.Gender),
                            new XElement("DR",p.BirthDate.ToString("yyyy-MM-dd"))
                            );
                    list.Add(pXml);

                }
                return xml;
            }
        }

        protected string GetFilename(FileType type, DateTime date, string version)
        {
            if (type==FileType.USL_FILE)
                return "HM032021T03_" + date.ToString("yyMM") + version + ".xml";
            else
                return "LM032021T03_" + date.ToString("yyMM") + version + ".xml";
        }

        public void SetExtrCases()
        {
            var emptCases = DMDoc.Descendants("SLUCH").Where(x => x.Element("PRVS").IsEmpty || x.Element("PROFIL").IsEmpty);
            foreach(var c in emptCases)
            {
                if (c.Element("EXTR")!=null)
                    c.Element("EXTR").Value="1";
                if (c.Element("FOR_POM")!=null)
                    c.Element("FOR_POM").Value = "1";
                if (c.Element("PRVS")!=null)
                    c.Element("PRVS").Value = "27";
                //if (c.Element("PROFIL").IsEmpty)
                //    c.Element("PROFIL").Value = "";
                
                foreach(var u in c.Descendants("USL"))
                {
                    u.Element("PRVS").Value = "27";
                    //u.Element("PROFIL").Value = "160";
                }
            }

        }


        public void Save(string folder)
        {
            var path = Path.Combine(folder, dmFilename);
            DMDoc.Declaration = new XDeclaration(null, "windows-1251", null);
            DMDoc.Save(path);
            path = Path.Combine(folder, lmFilename);
            LMDoc.Declaration = new XDeclaration(null, "windows-1251", null); ;
            LMDoc.Save(path);
        }


        public List<XElement> GetLabOrders(DateTime startDate, DateTime endDate, int zapCounter=1, int sluchCounter=1, int uslCounter=1)
        {
            var zapList = new List<XElement>();
            using (var db=new DbWorker())
            {
                var result = db.Connection.Query<LabOrder>("select * from usl_reestr_lab(@StartDate::date, @EndDate::date)", 
                    new { StartDate=startDate.Date, EndDate=endDate.Date });
                if (result==null || result.Count()==0) return null;
                var pacientList = from order in result
                                  group order by new { order.IdPac, order.VPolis, order.SPolis, order.NPolis, order.PrNov, order.SMO, order.Novor } into g
                                  select new { Pacient = g.Key, SluchList = g.ToList() };

                foreach(var z in pacientList)
                {
                    XElement zap = new XElement("ZAP",
                                    new XElement("N_ZAP", zapCounter++),
                                    new XElement("PR_NOV", z.Pacient.PrNov),
                                    new XElement("PACIENT",
                                        new XElement("ID_PAC", z.Pacient.IdPac),
                                        new XElement("VPOLIS", z.Pacient.VPolis),
                                        new XElement("SPOLIS", z.Pacient.SPolis),
                                        new XElement("NPOLIS", z.Pacient.NPolis),
                                        new XElement("SMO", z.Pacient.SMO),
                                        new XElement("NOVOR", z.Pacient.Novor)
                                        )
                                );

                    var sluchItem = (from s in z.SluchList
                                     group s by
                                     new
                                     {
                                         s.LPU,
                                         s.LPU_1,
                                         s.Diagnos,
                                         s.ResultD,
                                         s.IDSP,
                                         s.TableId,
                                         s.CodeMD,
                                         s.Det,
                                         s.NprMO,
                                         s.Vidpom,
                                         s.Profil,
                                         s.PRVS,
                                         s.Date_1,
                                         s.Date_2
                                     } into g
                                     select new OrderSluch
                                     {
                                         LPU = g.Key.LPU,
                                         LPU_1 = g.Key.LPU_1,
                                         Diagnos = g.Key.Diagnos,
                                         ResultD = g.Key.ResultD.ToString(),
                                         IDSP = g.Key.IDSP,
                                         TableId = g.Key.TableId,
                                         Det = g.Key.Det,
                                         CodeMD = g.Key.CodeMD, 
                                         Profil = g.Key.Profil,
                                         UslList = g.ToList(),
                                         NprMO = g.Key.NprMO,
                                         PRVS = g.Key.PRVS,
                                         Date1=g.Key.Date_1,
                                         Date2=g.Key.Date_2
                                     }).Where(s => s.UslList.Count() > 0);

                    


                    foreach(var s in sluchItem)
                    {
                        var sluch = new XElement("SLUCH",
                                    new XElement("IDCASE", sluchCounter++),
                                    new XElement("USL_OK",3),
                                    new XElement("VIDPOM", 12),
                                    new XElement("FOR_POM", 3),
                                    new XElement("NPR_MO", s.NprMO),
                                    new XElement("EXTR",1),
                                    new XElement("LPU", s.LPU),
                                    new XElement("LPU_1", s.LPU_1),
                                    new XElement("PODR", s.LPU_1),
                                    new XElement("PROFIL", s.Profil),
                                    new XElement("DET", s.Det),
                                    new XElement("NHISTORY", s.UslList.First().NHistory.ToString()),
                                    new XElement("DATE_1", s.Date1.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DATE_2", s.Date2.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DS1", s.Diagnos),
                                    new XElement("RSLT", s.ResultD),
                                    new XElement("ISHOD", 304),
                                    new XElement("PRVS", s.PRVS),
                                    new XElement("VERS_SPEC", "V015"),
                                    new XElement("IDDOKT", s.CodeMD),
                                    new XElement("IDSP", s.IDSP),
                                    new XElement("ED_COL", s.UslList.Count()),
                                    new XElement("TARIF", s.UslList.Sum(c => c.Tarif).ToString().Replace(",", ".")),
                                    new XElement("SUMV", s.UslList.Sum(c => c.Tarif).ToString().Replace(",", ".")),
                                    new XElement("OPLATA", 0)
                                );

                        var sluchUslList = s.UslList.GroupBy(u => u.CodeUsl).Select(g => g.First());
                        foreach (var u in sluchUslList)
                        {
                            if (!u.DateIn.HasValue)
                                u.DateIn = u.Date_1;
                            if (!u.DateOut.HasValue)
                                u.DateOut = u.Date_2;

                            var usl = new XElement("USL",
                                        new XElement("IDSERV", uslCounter++),
                                        new XElement("LPU", u.LPU),
                                        new XElement("LPU_1", u.LPU_1),
                                        new XElement("PODR", u.LPU_1),
                                        new XElement("PROFIL", u.Profil),
                                        new XElement("DET", u.Det),
                                        new XElement("DATE_IN", u.DateIn.Value.ToString("yyyy-MM-dd")),
                                        new XElement("DATE_OUT", u.DateOut.Value.ToString("yyyy-MM-dd")),
                                        new XElement("DS", u.Diagnos),
                                        new XElement("CODE_USL", u.CodeUsl),
                                        new XElement("KOL_USL", 1),
                                        new XElement("TARIF", u.Tarif.ToString().Replace(",", ".")),
                                        new XElement("SUMV_USL", u.Tarif.ToString().Replace(",", ".")),
                                        new XElement("PRVS", u.PRVS),
                                        new XElement("CODE_MD", u.CodeMD),
                                        new XElement("COMENTU", u.CommentU)
                                );
                            sluch.Add(usl);
                        }
                        //Добавляем случай к записи
                        zap.Add(sluch);
                    }
                    zapList.Add(zap);
                }
            }
            return zapList;
        }
    }
}
