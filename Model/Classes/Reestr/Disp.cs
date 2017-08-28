using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharedDbWorker;
using Dapper;
using System.IO;
using NLog;

namespace Model.Classes.Reestr
{
    public class Disp
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        string dmFilename;
        string lmFilename;
        string version;
        DateTime startDate;
        DateTime endDate;


        public Disp(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            //SetCulture();
        }

        public Disp()
        {
            var today = DateTime.Now;
            this.startDate = new DateTime(today.Year, today.Month, 1,0,0,0);
            this.endDate = DateTime.Now;
            //SetCulture();
        }

        private void SetCulture()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        public XDocument DMDoc { get; private set; }
        public XDocument LMDoc { get; private set; }

        public void InitFiles()
        {
            dmFilename = GetFilename(FileType.USL_FILE, DateTime.Now, "1");
            DMDoc = GetXML(startDate, endDate);
            lmFilename = GetFilename(FileType.PERS_FILE, DateTime.Now, "1");
            LMDoc = GetPersXML(DMDoc);
        }

        private XDocument GetXML(DateTime startDate, DateTime endDate)
        {
            //using(var db = new DbWorker())
            //{
            //    var sql = "select usl_reestr_dd(@StartDate::date, @EndDate::date, 0, null::varchar, null::varchar)";
            //    var result = db.Connection.Query<string>(sql, new { startDate=startDate, EndDate=endDate }).FirstOrDefault();
            //    if (result==null)
            //        return null;
            var uslList = GetUslList(startDate, endDate);
            var doc = CreateXml(uslList);
            //var doc = XDocument.Parse(result);
            var sluchCount = doc.Descendants("SLUCH").Count();
            var filename = doc.Descendants("FILENAME").First();
            filename.Value = dmFilename;
            var caseCount = doc.Descendants("SLUCH").Count();
            filename.AddAfterSelf(new XElement("SD_Z", caseCount));
            return doc;
            //}
        }

        private XDocument GetPersXML(XDocument uslFile)
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

        private string GetFilename(FileType type, DateTime date, string version)
        {
            if (type==FileType.USL_FILE)
                return "DM032021T03_" + date.ToString("yyMM") + version + ".xml";
            else
                return "LM032021T03_" + date.ToString("yyMM") + version + ".xml";
        }

        

        public void Save(string folder)
        {
            var path = Path.Combine(folder, dmFilename);

            DMDoc.Declaration = new XDeclaration(null, "windows-1251", null);
            DeleteUslFromProf(DMDoc);
            ChangeWrongUsl(DMDoc);
            //SetDispStage2(DMDoc);
            DeleteDuplZAP(DMDoc);
            DeleteFirstStage(DMDoc);
            DeleteSecondStage(DMDoc);
            DeletePaid(DMDoc);
            CountSLUCH(DMDoc);
            DMDoc = CountUSL(DMDoc);
            //SetEmptyDoctors(DMDoc);
            DMDoc.Save(path);

            path = Path.Combine(folder, lmFilename);
            LMDoc.Declaration = new XDeclaration(null, "windows-1251", null);
            //LMDoc = GetWin1251(LMDoc);
            LMDoc.Save(path);
        }

        
        public void CountSLUCH(XDocument doc)
        {
            var sluchCount = doc.Descendants("SLUCH").Count();
            var cases =doc.Descendants("SLUCH");
            var counter = 1;
            foreach (var c in cases)
            {
                c.Element("IDCASE").Value = counter.ToString();
                counter += 1;
            }
        }

        public void DeleteDuplZAP(XDocument doc)
        {
            var dupl = doc.Descendants("ZAP")
                .GroupBy(x => x.Element("PACIENT").Element("ID_PAC").Value)
                .SelectMany(x => x.Skip(1)).ToList();
            dupl.ForEach(x => _logger.Debug("Удален дубликат ZAP {0} PACID {1}", x.Element("N_ZAP"), x.Element("PACIENT").Element("ID_PAC")));
            dupl.ForEach(x => x.Remove());
        }

        
        public void DeleteFirstStage(XDocument doc)
        {
            //выбираем
            var cases = doc.Descendants("SLUCH")
                        .Where(x=> x.Element("COMENTSL").Value.Contains("_3_2_d")
                        && x.Elements("USL").Where(u=>u.Element("COMENTU").Value.Contains("основная услуга")).Any()         
                        ).ToList();

            cases.ForEach(x => x.Remove());
        }

        
        public void DeleteSecondStage(XDocument doc)
        {
            var cases = doc.Descendants("SLUCH")
                        .Where(x => x.Element("COMENTSL").Value.Contains("_3_1_d")
                        && x.Elements("USL").Where(u => u.Element("CODE_USL").Value.StartsWith("0633")).Any()
                        ).ToList();

            cases.ForEach(x => x.Remove());
        }

        
        public void DeletePaid(XDocument doc)
        {
            var casesFirstStage = doc.Descendants("SLUCH").Where(x => x.Element("COMENTSL").Value.Contains("_3_1_d")).ToList();
            var toDelete = new List<XElement>();
            foreach(var item in casesFirstStage)
            {
                var sluch = item.Element("COMENTSL").Value.Split(new char[]{'_'})[0];
                var ispaid=IsPaid(sluch, null, 1);
                
                if(ispaid)
                {
                    toDelete.Add(item);
                }
            }

            var uslSecondStage = doc.Descendants("SLUCH")
                        .Where(x => x.Element("COMENTSL").Value.Contains("_3_2_d"))
                        .SelectMany(x=>x.Elements("USL")).ToList();
            foreach(var item in uslSecondStage)
            {
                var sluch = item.Parent.Element("COMENTSL").Value.Split(new char[] { '_' })[0];
                var usl = item.Element("COMENTU").Value.Split(new char[] { '_' })[0];
                var ispaid = IsPaid(sluch, usl, 2);

                if (ispaid)
                {
                    toDelete.Add(item);
                }
            }

            toDelete.ForEach(x => x.Remove());

        }

        
        public bool IsPaid(string caseNumber, string uslNumber, int stage)
        {
            using(var db=new DbWorker())
            {
                var parameters = new DynamicParameters();
                var sql = string.Empty;
                if (stage==1)//первый этап
                {
                    sql = @"select coalesce(paid, false) 
                            from okaz_uslug_tab 
                            where bur_dd_magic_number=1 and table_link_id=@CaseNumber
                            and table_link_name='dop_disp_main_tab'
                           ";
                    parameters.Add("CaseNumber", caseNumber);
                }
                else
                {
                    sql = @"select coalesce(paid, false) 
                            from okaz_uslug_tab 
                            where bur_dd_magic_number=@UslNumber and table_link_id=@CaseNumber
                            and table_link_name='dop_disp_main_tab'
                           ";
                    parameters.Add("CaseNumber", caseNumber);
                    parameters.Add("UslNumber", uslNumber);
                }

                var result = db.Connection.Query<bool>(sql, parameters).FirstOrDefault();
                return result;
            }
        }

        
        public XDocument CountUSL(XDocument uslDoc)
        {
            var uslCount = uslDoc.Descendants("USL").Count();
            var usls = uslDoc.Descendants("USL");
            var counter=1;
            foreach(var usl in usls)
            {
                usl.Element("IDSERV").Value = counter.ToString();
                counter += 1;
            }
            return uslDoc;
        }


        public void DeleteUslFromProf(XDocument uslDoc)
        {
            var cases = uslDoc.Descendants("USL").
                Where(u => u.Element("COMENTU").Value == "основная услуга"
                      && u.Element("CODE_USL").Value.StartsWith("0634"))
                      .Select(u => u.Parent);
            foreach(var c in cases)
            {
                var uslToDelete = c.Descendants("USL")
                                .Where(u => u.Element("COMENTU").Value != "основная услуга"
                                    && u.Element("CODE_USL").Value != "063438"
                                ).ToList();
                _logger.Debug("Удаление услуг профосмотра");
                foreach(var u in uslToDelete)
                {
                    _logger.Debug("Удяляем услугу {0}", u.ToString());
                }
                
                uslToDelete.ForEach(u => u.Remove());

                var uslReplace = c.Descendants("USL").Where(u=>u.Element("CODE_USL").Value == "063438").ToList();
                foreach(var u in uslReplace)
                {
                    u.Element("CODE_USL").Value = "061057";
                }
            }
            
        }

        /// <summary>
        /// Изменяет неправильные коды услуг
        /// </summary>
        /// <param name="uslDoc"></param>
        public void ChangeWrongUsl(XDocument uslDoc)
        {
            var childCodes = GetChildCodes();
            var cases = uslDoc.Descendants("USL").
                Where(u => u.Element("COMENTU").Value=="основная услуга"
                      && u.Element("CODE_USL").Value.StartsWith("163")).Select(u=>u.Parent);
            foreach(var c in cases)
            {
                //_logger.Debug("Основная услуга {0} ", c.Element("CODE_USL").Value);
                var adultUSL = c.Descendants("USL").Where(u => u.Element("CODE_USL").Value.StartsWith("0"));
                
                if (adultUSL!=null)
                {
                    _logger.Debug("Найдено {0} услуг c неправильным кодом", adultUSL.Count());
                    foreach(var u in adultUSL)
                    {
                        var childCode = childCodes.Where(cod => cod.Code == u.Element("CODE_USL").Value).FirstOrDefault();
                        if (childCode!=null)
                        {
                            _logger.Debug("Code {0} изменен на {1}", u.Element("CODE_USL").Value, childCode.ChildCode);
                            u.Element("CODE_USL").Value = childCode.ChildCode;
                        }
                    }
                }
            }
        }

        
        public void SetEmptyDoctors(XDocument uslDoc)
        {
            var emptyDocUsls = uslDoc.Descendants("USL").Where(u => u.Element("CODE_MD").Value=="-- ");
            foreach (var usl in emptyDocUsls)
            {
                var valueList = usl.Element("COMENTU").Value.Split('_').ToList();
                usl.Element("CODE_MD").Value = valueList.Last();
                _logger.Debug("Замена {1} CODE_MD на {0}", valueList.Last(), usl.Element("CODE_MD").Value);
            }
        }



        private IEnumerable<ChildUslCodes> GetChildCodes()
        {
            using(var db = new DbWorker())
            {
                var codes = db.Connection.Query<ChildUslCodes>("select id, code,childcode from codifiers.dop_disp_measures_child_codes_tab");
                foreach (var c in codes)
                {
                    c.Code = "0" + c.Code;
                }
                return codes;
            }
        }


        /// <summary>
        /// Выделение услуг второго этапа
        /// </summary>
        public void  SetDispStage2(XDocument doc)
        {
            List<string> stage2Usls = null;
            using(var db=new DbWorker())
            {
                stage2Usls = db.Connection.Query<string>(
                    @"select  '0' || auto_usl_code2::varchar 
                    from codifiers.dop_disp_measures_tab 
                    where auto_usl_code2 is not null ").ToList();
            }

            var casesWithStage2 = doc
                .Descendants("USL")
                .Where(x => stage2Usls.Contains(x.Element("CODE_USL").Value))
                .Select(x=> x.Parent)
                .Distinct()
                .ToList();

            if (!casesWithStage2.Any())
            {
                _logger.Debug("Не обнаружены случаи для второго этапа ДОГВН");
                return;
            }

            for (int i = 0; i < casesWithStage2.Count; i++ )
            {
                var c = casesWithStage2[i];
                var stage2Case = new XElement(c);
                
                var uslsStage2 = c.Descendants("USL").Where(x => stage2Usls.Contains(x.Element("CODE_USL").Value)).ToList();
                if (uslsStage2.Any())
                    foreach (var u in uslsStage2)
                    {
                        u.Remove();
                    }
                
                //var uslsStage2 = c.Descendants("USL")
                //                  .Where(x => stage2Usls.Contains(x.Element("CODE_USL").Value))
                //                  .ToList();
                //if (uslsStage2.Any())
                //    foreach (var u in uslsStage2)
                //    {
                //        u.Remove();
                //    }

                
                var usls = stage2Case.Descendants("USL").Where(x => stage2Usls.Contains(x.Element("CODE_USL").Value)).GroupBy(x => x.Element("CODE_USL").Value).Select(x => x.First()).ToList();
                var oldusls = stage2Case.Elements("USL").Select(x => x).ToList();
                foreach(var u in oldusls)
                {
                    u.Remove();
                }

                var commentsl = stage2Case.Element("COMENTSL");
                foreach(var u in usls)
                {
                    commentsl.AddBeforeSelf(u);
                    _logger.Debug("Добавлена услуга для второго этапа {0}", u);
                }

                var tarif = stage2Case.Descendants("USL").Select(x => x.Element("SUMV_USL").Value).ToList();
                var sum = tarif.Sum(x => Convert.ToDecimal(x.Replace('.', ','))).ToString();
                stage2Case.Element("TARIF").Value = sum.ToString().Replace(",",".");
                stage2Case.Element("SUMV").Value = sum.ToString().Replace(",", ".");
                stage2Case.Element("ED_COL").Value = tarif.Count.ToString();
                
                
                if (stage2Case.Descendants("USL").Any())
                {
                    c.AddAfterSelf(stage2Case);
                    _logger.Debug("Добавлен случай для второго этапа {0}", stage2Case);
                }
                else
                {
                    _logger.Debug("Не добавлен случай для второго этапа из-за отсутствия услуг");
                }

            }

            var noUSL = doc.Descendants("SLUCH").Where(x => !x.Elements("USL").Any()).ToList();
            foreach(var s in noUSL)
            {
                s.Remove();
            }

            
        }

        public IEnumerable<Usl> GetUslList(DateTime startDate, DateTime endDate)
        {
            using(var db =new DbWorker())
            {
                var sql = @"select * from  usl_reestr_dd_getusl(@StartDate::date, @EndDate::date)";
                var result = db.Connection.Query<Usl>(sql, new { StartDate = startDate.Date.ToString("yyyy-MM-dd"),
                                                                 EndDate = endDate.Date.ToString("yyyy-MM-dd")
                                                                });
                return result;
            }

        }

        public XDocument CreateXml(IEnumerable<Usl> uslList)
        {
            XDocument xdoc = new XDocument(
                                new XElement("ZL_LIST",
                                           new XElement("ZGLV",
                                               new XElement("VERSION", "2.1"),
                                               new XElement("DATA", DateTime.Now.ToString("yyyy-MM-dd")),
                                               new XElement("FILENAME", GetFilename(FileType.USL_FILE, DateTime.Now, "1"))
                                               ),
                                            new XElement("SCHET",
                                                new XElement("CODE", 1),
                                                new XElement("CODE_MO", "032021"),
                                                new XElement("YEAR", DateTime.Now.Year),
                                                new XElement("MONTH", DateTime.Now.Month.ToString("D2")),
                                                new XElement("NSCHET", 1),
                                                new XElement("DSCHET", DateTime.Now.ToString("yyyy-MM-dd")),
                                                new XElement("SUMMAV"),
                                                new XElement("COMENTS")
                                                )
                               )
                );
            var zapList = from z in uslList
                          group z by new { z.IdPac, z.VPolis, z.SPolis, z.NPolis, z.PrNov,z.SMO } into g
                          select new { Pacient=g.Key, SluchList=g.ToList() };
            var zapCounter = 1;
            foreach(var z in zapList)
            {
                XElement zap = new XElement("ZAP", 
                                    new XElement("N_ZAP", zapCounter++),
                                    new XElement("PR_NOV", z.Pacient.PrNov),
                                    new XElement("PACIENT",
                                        new XElement("ID_PAC", z.Pacient.IdPac),
                                        new XElement("VPOLIS", z.Pacient.VPolis),
                                        new XElement("SPOLIS",z.Pacient.SPolis),
                                        new XElement("NPOLIS", z.Pacient.NPolis),
                                        new XElement("SMO", z.Pacient.SMO)
                                        )
                );

                _logger.Debug("Пациент {1} {0}", z.Pacient.NPolis, z.Pacient.IdPac);

                var sluchItem = (from s in z.SluchList
                                group s by
                                new
                                {
                                    s.DDType,
                                    s.LPU,
                                    s.VBR,
                                    s.POtk,
                                    s.Diagnos,
                                    s.ResultD,
                                    s.IDSP,
                                    s.MakedProc,
                                    s.TableId
                                } into g
                                select new Sluch{ 
                                    DDType= g.Key.DDType,
                                    LPU= g.Key.LPU,
                                    VBR = g.Key.VBR,
                                    POtk= g.Key.POtk,
                                    Diagnos= g.Key.Diagnos,
                                    ResultD= g.Key.ResultD,
                                    IDSP= g.Key.IDSP,
                                    MakedProc =  g.Key.MakedProc,
                                    TableId = g.Key.TableId,
                                    UslList = g.ToList() 
                                }).Where(s=>s.UslList.Count()>0).FirstOrDefault();
                
                int sluchCounter=1;

                _logger.Debug("Услуги пациента:");
                foreach(var item in sluchItem.UslList)
                {
                    _logger.Debug(" -->USL {0} {1} {2}", item.CodeUsl, item.IsSecondStage, item.CodeUsl);
                }
                
                //Разделяем на случаи первого и второго этапа
                var groupedSluchList = new List<Sluch>();

                var stage2Sluch = new Sluch()
                {
                    DDType = sluchItem.DDType,
                    LPU = sluchItem.LPU,
                    //LPU_1 = sluchItem.LPU_1,
                    VBR = sluchItem.VBR,
                    POtk = sluchItem.POtk,
                    Diagnos = sluchItem.Diagnos,
                    ResultD = sluchItem.ResultD,
                    IDSP = sluchItem.IDSP,
                    MakedProc = sluchItem.MakedProc,
                    MainPRVS = sluchItem.MainPRVS,
                    NHistory = sluchItem.NHistory
                };

                
                
                var stage1Usls = sluchItem.UslList.Where(u => u.Stage == 1).Select(u=>u).ToList();
                
                if (sluchItem.DDType==3)
                {
                    stage1Usls = sluchItem.UslList.Where(u => u.Stage == 1 && u.CodeUsl.StartsWith("0630")).Select(u => u).ToList();
                }

                var stage2Usls = sluchItem.UslList.Where(s => s.Stage != 1 && s.CodeUsl.StartsWith("0633") && s.IsSecondStage).Select(u => u).ToList();

                if (stage1Usls.Count>0)
                {
                    sluchItem.UslList = stage1Usls;
                    sluchItem.NHistory = sluchItem.UslList.Select(u => u.NHistory).FirstOrDefault();
                    sluchItem.Stage = 1;
                    sluchItem.Date1 = sluchItem.UslList.Min(u => u.Date_1);
                    sluchItem.Date2 = sluchItem.UslList.Max(u => u.Date_2);
                    sluchItem.MainCode = sluchItem.UslList.Select(u=>u.MainCode).First();
                    sluchItem.MainPRVS = sluchItem.UslList.Select(u => u.MainPRVS).First();
                    sluchItem.MainCodeMD = sluchItem.UslList.Select(u => u.MainCodeMD).First();
                    sluchItem.VidPom = sluchItem.UslList.Select(u => u.VidPom).First();
                    sluchItem.LPU_1 = sluchItem.UslList.Select(u => u.LPU_1).FirstOrDefault();
                    groupedSluchList.Add(sluchItem);
                }
                if (stage2Usls.Count>0)
                {
                    stage2Sluch.UslList = stage2Usls;
                    stage2Sluch.Stage = 2;
                    stage2Sluch.NHistory = stage2Sluch.UslList.Select(u => u.NHistory).FirstOrDefault();
                    stage2Sluch.Date1 = stage2Sluch.UslList.Min(u => u.Date_1);
                    stage2Sluch.Date2 = stage2Sluch.UslList.Max(u => u.Date_2);
                    stage2Sluch.MainCode = stage2Sluch.UslList.Select(u => u.MainCode).First();
                    stage2Sluch.MainPRVS = stage2Sluch.UslList.Select(u => u.MainPRVS).First();
                    stage2Sluch.MainCodeMD = stage2Sluch.UslList.Select(u => u.MainCodeMD).First();
                    stage2Sluch.VidPom = sluchItem.UslList.Select(u => u.VidPom).First();
                    stage2Sluch.LPU_1 = sluchItem.UslList.Select(u => u.LPU_1).FirstOrDefault();
                    groupedSluchList.Add(stage2Sluch);
                }

                foreach (var s in groupedSluchList)
                {
                    var sluch = new XElement("SLUCH",
                                    new XElement("IDCASE", sluchCounter++),
                                    new XElement("VIDPOM", s.VidPom),
                                    new XElement("LPU", s.LPU),
                                    new XElement("LPU_1", s.LPU_1),
                                    new XElement("VBR", s.VBR),
                                    new XElement("NHISTORY", s.NHistory),
                                    new XElement("P_OTK", s.POtk),
                                    new XElement("DATE_1", s.Date1.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DATE_2", s.Date2.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DS1", s.Diagnos),
                                    new XElement("RSLT_D", s.ResultD),
                                    new XElement("IDSP", s.IDSP),
                                    new XElement("ED_COL", ""),
                                    new XElement("TARIF", ""),
                                    new XElement("SUMV", ""),
                                    new XElement("OPLATA", 0)
                                );
                    var uslCounter = 1;
                    var sluchUslList = s.UslList.GroupBy(u=>u.CodeUsl).Select(g=>g.First());
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
                                    new XElement("DATE_IN", u.DateIn.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DATE_OUT", u.DateOut.Value.ToString("yyyy-MM-dd")),
                                    new XElement("P_OTK", u.POtk),
                                    new XElement("CODE_USL", u.CodeUsl),
                                    new XElement("TARIF", u.Tarif.ToString().Replace(",",".")),
                                    new XElement("SUMV_USL", u.Tarif.ToString().Replace(",",".")),
                                    new XElement("PRVS", u.PRVS),
                                    new XElement("CODE_MD", string.IsNullOrEmpty(u.CodeMD) ? s.MainCodeMD : u.CodeMD),
                                    new XElement("COMENTU", u.CommentU)
                            );
                        sluch.Add(usl);
                    }

                    XElement mainUsl = null;
                    //Добавляем главные услуги
                    if (s.DDType==3 && s.Stage==1 && s.MakedProc>85)
                    {
                        var mainTarif = s.UslList.Select(c => c.MainTarif).FirstOrDefault();
                        mainUsl = new XElement("USL",
                                    new XElement("IDSERV", uslCounter++),
                                    new XElement("LPU", s.LPU),
                                    new XElement("LPU_1", s.LPU_1),
                                    new XElement("DATE_IN", s.Date1.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DATE_OUT", s.Date2.Value.ToString("yyyy-MM-dd")),
                                    new XElement("P_OTK", s.POtk),
                                    new XElement("CODE_USL", s.MainCode ),
                                    new XElement("TARIF", mainTarif.ToString().Replace(",", ".")),
                                    new XElement("SUMV_USL", mainTarif.ToString().Replace(",", ".")),
                                    new XElement("PRVS", s.MainPRVS),
                                    new XElement("CODE_MD", s.MainCodeMD),
                                    new XElement("COMENTU", "основная услуга")
                            );
                        sluch.Element("TARIF").Value = mainTarif.ToString().Replace(",", ".");
                        sluch.Element("SUMV").Value = mainTarif.ToString().Replace(",", ".");
                        sluch.Element("ED_COL").Value = s.UslList.Count().ToString();

                    }
                    else if (s.DDType!=3 && s.Stage==1)
                    {
                        var mainTarif2 = s.UslList.Select(c => c.MainTarif2).FirstOrDefault();
                        mainUsl = new XElement("USL",
                                    new XElement("IDSERV", uslCounter++),
                                    new XElement("LPU", s.LPU),
                                    new XElement("LPU_1", s.LPU_1),
                                    new XElement("DATE_IN", s.Date1.Value.ToString("yyyy-MM-dd")),
                                    new XElement("DATE_OUT", s.Date2.Value.ToString("yyyy-MM-dd")),
                                    new XElement("P_OTK", s.POtk),
                                    new XElement("CODE_USL", s.MainCode),
                                    new XElement("TARIF", mainTarif2.ToString().Replace(",", ".")),
                                    new XElement("SUMV_USL", mainTarif2.ToString().Replace(",", ".")),
                                    new XElement("PRVS", s.MainPRVS),
                                    new XElement("CODE_MD", s.MainCodeMD),
                                    new XElement("COMENTU", "основная услуга")
                            );
                        sluch.Element("TARIF").Value = mainTarif2.ToString().Replace(",", ".");
                        sluch.Element("SUMV").Value = mainTarif2.ToString().Replace(",", ".");
                        sluch.Element("ED_COL").Value = s.UslList.Count().ToString();
                    }
                    else
                    {
                        var sum = s.UslList.Sum(x => x.Tarif).ToString();
                        sluch.Element("TARIF").Value = sum.ToString().Replace(",", ".");
                        sluch.Element("SUMV").Value = sum.ToString().Replace(",", ".");
                        sluch.Element("ED_COL").Value = s.UslList.Count().ToString();
                    }
                    sluch.Add(mainUsl);
                    sluch.Add(new XElement("COMENTSL", s.NHistory+"_"+s.DDType+"_"+s.MakedProc));

                    //Добавляем случай к записи
                    zap.Add(sluch);
                }
                xdoc.Root.Add(zap);
            }
            return xdoc;
        }

//        public IEnumerable<Sluch> GetSluchList(IEnumerable<OkazUsl> okazUslList)
//        {
//            foreach (var o in okazUslList)
//            {
//                using(var db = new DbWorker())
//                {
//                    var sql = @"SELECT  
//	                            dm.id id,
//                                dm.maked_proc makedproc,
//                                dm.dop_disp_type_id ddtype,
//                                case when tds.vid_med_pom_id is not null then tds.vid_med_pom_id else 1 end vidpom,
// 	                            '032021' lpu,
//                                '032021' lpu_1,
//                                '0' vbr,
//                                '0' potk,
//                                case when dm.result_stage_2 is not null then dm.result_stage_2 else dm.result_stage_1 end as reestr_rslt,
//                                '11' idsp,
//                                ddm.id id,
//                                ddm.measure_date datein,
//                                ddm.measure_date dateout,
//	                            to_char(case when dm.dop_disp_type_id=4 then cddm.auto_usl_code_for_prof else cddm.auto_usl_code end, 'FM099999') codeusl,
//                                ec.cost tarif,
//                                case when mds.xprvs_new is null then '27' else mds.xprvs_new end prvs,
//                                case when trim(from mdocdan.pens)!='' then mdocdan.pens else mdoc.id::varchar end codemd
//                            FROM dop_disp_main_tab dm 
//                            left join dan_tab pat on pat.dan_id=dm.dan_id
//                            LEFT JOIN doctor_tab doc ON doc.id  = dm.doctor_id
//                            LEFT JOIN dan_tab docdan ON docdan.dan_id = doc.dan_id
//                            LEFT JOIN codifiers.otdel_tab o ON o.id  = doc.sp_otdel_id
//                            LEFT JOIN codifiers.doctor_spec_tab tds ON tds.id=doc.sp_spec_doctor_id
//                            left join dop_disp_measures_tab ddm on dm.id=ddm.dop_disp_main_id
//                            left join codifiers.dop_disp_measures_tab cddm on cddm.id = ddm.dop_disp_measure_id
//                            LEFT JOIN codifiers.sp_uslug_tab cu ON cu.code =  to_char( cddm.auto_usl_code,'FM099999')
//                            LEFT JOIN eq_uslug_cost_tab ec on ec.sp_uslug_id=cu.id
//                            left join doctor_tab mdoc on mdoc.id=ddm.doctor_id
//                            left join dan_tab mdocdan on mdocdan.dan_id=mdoc.dan_id
//                            LEFT JOIN codifiers.doctor_spec_tab mds ON mdoc.sp_spec_doctor_id=mds.id
//                            where dm.id=@SluchId and ddm.neprov_flag is DISTINCT from true and cu.code is not null
//                                 ";
//                    var result = db.Connection.Query<Usl>(sql, new { SluchId =  });
//                }
//            }
//        }


        public IEnumerable<OkazUsl> GetOkazUSL(DateTime dateStart, DateTime dateEnd)
        {
            using (var db = new DbWorker())
            {
                var sql = @"SELECT 
                            ok.id,
                            ok.table_link_id tableid, 
                            ok.bur_dd_magic_number stage, 
                            ok.sluch_date_end enddate, 
                            ok.dan_id pacientid,
                            ok.doctor_id doctorid,
                            bur_convert_diagn(ok.inference_diagnosis) as diagnos,
                            d.dan_id id,
                            CASE WHEN d.policy_type > 3 THEN '3'::VARCHAR ELSE d.policy_type::VARCHAR END vpolis,
                            d.ser_pol spolis,
                            d.num_pol npolis,
                            s.tfoms_code smo
                            FROM okaz_uslug_tab ok
                            LEFT JOIN dan_tab d on d.dan_id=ok.dan_id
                            LEFT JOIN codifiers.strcom_tab s ON s.id = d.strcom_id
                            WHERE 
                            table_link_name='dop_disp_main_tab' and sluch_date_end BETWEEN @StartDate and @EndDate 
                            and paid is distinct from true AND dont_upload_for_paid is distinct from true";
                var result = db.Connection.Query<OkazUsl, Pacient, OkazUsl>(sql, (ok, pac) => { ok.Pacient = pac; return ok; }, new { StartDate = dateStart, EndDate = dateEnd });
                return result;
            }
        }
        
        
    }//Reest.Disp

    
    public class OkazUsl
    {
        public long Id { get; set; }
        public long PacientId { get; set; }
        public long DoctorId { get; set; }
        public long TableId { get; set; }
        public DateTime? EndDate {get;set;}
        public long Stage {get;set;}
        public string Diagnos { get; set; }
        public Pacient Pacient { get; set; }

    }

    public class Usl
    {
        public long Id { get; set; }
        public int? IdServ { get; set; }
        public string LPU { get; set; }
        public string LPU_1 { get;set; }
        public DateTime? Date_1 { get; set; }
        public DateTime? Date_2 { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string VBR { get; set; }
        public string POtk { get; set; }
        public string CodeUsl { get; set; }
        public bool IsSecondStage { get; set; }
        public double Tarif { get; set; }
        public double SumvUsl { get; set; }
        public string PRVS { get; set; }
        public string CodeMD { get; set; }
        public string CommentU { get; set; }
        public long TableId { get; set; }
        public long? Stage { get; set; }
        public long? UslId { get; set; }
        public int DDType { get; set; }
        public int MakedProc { get; set; }
        public string MainCode { get; set; }
        public string MainTarif { get; set; }
        public string MainTarif2 { get; set; }
        public string MainPRVS { get; set; }
        public string MainCodeMD { get; set; }
        public string VPolis { get; set; }
        public string SPolis { get; set; }
        public string NPolis { get; set; }
        public string SMO { get; set; }
        public string IdPac { get; set; }
        public string PrNov { get; set; }
        public string NHistory { get; set; }
        public string VidPom { get; set; }
        public string Diagnos { get; set; }
        public string ResultD { get; set; }
        public string IDSP { get; set; }
        
    }

    public class Sluch
    {
        public long? TableId { get; set; }
        public string VidPom {get;set;}
        public int DDType{get;set;}
        public string LPU { get; set; }
        public string LPU_1 { get; set; }
        public string VBR { get; set; }
        public string NHistory { get; set; }
        public string POtk { get; set; }
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public string Diagnos { get; set; }
        public string ResultD { get; set; }
        public string IDSP { get; set; }
        public long? Stage { get; set; }
        public string MainCode {get;set;}
        public int? MakedProc {get;set;}
        public string MainPRVS {get;set;}
        public string MainCodeMD {get;set;}
        public IEnumerable<Usl> UslList {get;set;}
    }

    public class Zap
    {
        public int NZap { get; set; }
        public int PRNov { get; set; }
        public Pacient Pacient {get;set;}
        public IEnumerable<Sluch> SluchList { get; set; }
    }

    public class Pacient
    {
        public long? IdPac { get; set; }
        public string VPolis { get; set; }
        public string SPolis { get;set; }
        public string NPolis { get; set; }
        public string SMO { get; set; }
    }

    public enum FileType
    {
        USL_FILE,
        PERS_FILE
    }

    public class ChildUslCodes
    {
        public int Id {get;set;}
        public String Code {get;set;}
        public String ChildCode {get;set;}
    }
}
