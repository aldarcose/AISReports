using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;

namespace Model.Classes
{
    public class Visit : ILoadData, ICreateable, IDeleteable, IPrintable, ISaveable
    {
        public Visit()
        {
            IsLoading = false;
            IsLoaded = false;

            TextAnamnez = string.Empty;
            TextComplaint = string.Empty;
            TextObjStatus = string.Empty;
            //TextRecommend = string.Empty;
        }

        public Visit(long id)
            : this()
        {
            LoadData(id);
        }

        public long Id { get; set; }
        /// <summary>
        /// Талон
        /// </summary>
        public long TalonId { get; set; }

        /// <summary>
        /// Доктор
        /// </summary>
        public long DoctorId { get; set; }

        /// <summary>
        /// Медсестра
        /// </summary>
        public long NurseId { get; set; }

        /// <summary>
        /// Пациент
        /// </summary>
        public long PatientId { get; set; }

        /// <summary>
        /// Цель посещения (target_pos_tab.target_pos_id)
        /// </summary>
        public long PurposeId { get; set; }

        /// <summary>
        /// Тип оплаты (sp_kind_paid_tab.id)
        /// </summary>
        public long PaymentTypeId { get; set; }
        /// <summary>
        /// Место обслуживания
        /// </summary>
        public long ServicePlaceId { get; set; }
        /// <summary>
        /// Дата посещения
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// Флаг: визит первичный
        /// </summary>
        public bool IsFirst { get; set; }


        /// <summary>
        /// Флаг: визит завершен
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Дата и время внесения/изменения записи
        /// </summary>
        public DateTime CreateRecordDate { get; set; }
        /// <summary>
        /// Флаг: имеет сан.-курортную карту
        /// </summary>
        public bool HasSanCard { get; set; }

        public string TextAnamnez { get; set; }
        public string TextComplaint { get; set; }
        public string TextObjStatus { get; set; }
        //public string TextRecommend { get; set; }

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

            var loadResult = false;
            
            Id = id;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetVisitData");
                q.Sql = "select *, public.visit_tab.is_complete from public.visit_tab where visit_id = @id";
                q.AddParamWithValue("@id", id);
                var result = db.GetResult(q);
                if (result != null && result.Fields.Count > 0)
                {
                    DoctorId = DbResult.GetNumeric(result.GetByName("doctor_id"), -1);
                    NurseId = DbResult.GetNumeric(result.GetByName("nurse_id"), -1);
                    PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), -1);
                    ServicePlaceId = DbResult.GetNumeric(result.GetByName("place_service_id"), -1);
                    PaymentTypeId = DbResult.GetNumeric(result.GetByName("vid_oplat_id"), -1);
                    PurposeId = DbResult.GetNumeric(result.GetByName("target_pos_id"), -1);
                    VisitDate = DbResult.GetNullableDateTime(result.GetByName("data_pos"));
                    IsFirst = DbResult.GetBoolean(result.GetByName("is_primary"), false);
                    CreateRecordDate = DbResult.GetDateTime(result.GetByName("data_vnes_inf"), DateTime.MinValue);
                    IsFirst = DbResult.GetBoolean(result.GetByName("is_complete"), false);
                    HasSanCard = DbResult.GetBoolean(result.GetByName("skl_kard"), false);
                    loadResult = true;
                }
            }
            
            GetTextTemplates();
            IsLoaded = loadResult;

            OnLoaded();

            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        private void GetTextTemplates()
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetTemplates");
                query.Sql = "select a.text_description, c.text_description, os.text_description " +
                        "from visit_anamnez_tab a " +
                        "left join visit_complaint_tab c on c.visit_id = a.visit_id " +
                        "left join visit_obj_status_tab os on os.visit_id = a.visit_id " +
                        "where a.visit_id = @visit_id;";
                query.AddParamWithValue("@visit_id", Id);

                var result = dbWorker.GetResult(query);

                if (result != null && result.Fields.Count > 0)
                {
                    TextAnamnez = DbResult.GetString(result.Fields[0], "");
                    TextComplaint = DbResult.GetString(result.Fields[1], "");
                    TextObjStatus = DbResult.GetString(result.Fields[2], "");
                }

            }
        }

        

        public string[] PrintableTypes()
        {
            return new string[]
            {
                "Результат посещения"
            };
        }

        public void Print(string type, object[] args)
        {
            if (type == "Результат посещения")
            {
               /* var patient = args[0] as Patient;
                var talon = args[1] as Talon;
                var visit = args[2] as Visit;

                var path = Path.Combine(Environment.CurrentDirectory, @"print\результаты посещения.html");
                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);

                    var printContent = content.Replace("<patient>", string.Format("{0} {1} {2}", patient.LastName, patient.FirstName, patient.MidName));

                    printContent = printContent.Replace("<myCurrentDate>", visit.VisitDate.Value.ToShortDateString());
                    printContent = printContent.Replace("<myMestObs>", cmb_servicePlace.Text);
                    printContent = printContent.Replace("<myTargetPos>", cmb_serviceAim.Text);

                    printContent = printContent.Replace("<myAnamnez>", te_anamnez.Text);
                    printContent = printContent.Replace("<myZhalobi>", te_complains.Text);
                    printContent = printContent.Replace("<myObjStatus>", te_objStatus.Text);

                    if (_diagnoses.Count > 0)
                    {
                        printContent = printContent.Replace("<myDiagnoz>", _diagnoses[0].MkbName);
                        printContent = printContent.Replace("<myOpisDiag>", string.Empty);
                    }
                    printContent = printContent.Replace("<myFizio>", string.Empty);
                    printContent = printContent.Replace("<myMedicomentoz>", string.Empty);
                    printContent = printContent.Replace("<myRecept>", string.Empty);

                    printContent = printContent.Replace("<myResult>", cmb_caseState.Text);
                    printContent = printContent.Replace("<myBolList>", lbl_b_paper.Text);
                    printContent = printContent.Replace("<myDateNext>", de_nextVisitDate.Text);
                    printContent = printContent.Replace("<myFIOdoct>", te_serviceDoctor.Text);

                    IPrintWorker printer = new PrintWorker();
                    printer.PrintHtmlContent("");
                }*/
            }
        }
        /*
        private void PrintHtmlPage(string content)
        {
            // CreateInDb a WebBrowser instance. 
            WebBrowser webBrowserForPrinting = new WebBrowser();

            // Add an event handler that prints the document after it loads.
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);

            // Set the Url property to load the document.
            //webBrowserForPrinting.Url = new Uri(@"\\myshare\help.html");
            webBrowserForPrinting.DocumentText = content;
        }

        private void PrintDocument(object sender,
            WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }*/

        public event EventHandler Created;

        public void OnCreated()
        {
            if (Created != null)
            {
                Created(this, null);
            }
        }

        public bool CanCreateInDb(Operator @operator, out string message)
        {
            message = string.Empty;
            return true;
        }

        public void CreateInDb(Operator @operator)
        {
            using (var db = new DbWorker())
            {
                var qId = new DbQuery("GettingNewVisitId");
                qId.Sql = "Select nextval('visit_seq');";
                var id = DbResult.GetNumeric(db.GetScalarResult(qId), -1);
                if (id == -1)
                {
                    throw new DbWorkerException("Ошибка получения нового ID визита") { Sql = qId.Sql };
                }
                else
                {
                    Id = id;
                    CreateRecordDate = DateTime.Now;
                    //var qCreate = new DbQuery("CreateVisit");
                    var q =
                        "INSERT INTO visit_tab (visit_id,doctor_id, operat_id, data_pos, data_vnes_inf, talon_id, mesto_obs, target_pos_id) " +
                        "VALUES (@id, @docId, @operatorId, @datePos, @dateCreate, @talonId, @servicePlaceCode, @purposeId);";
                    var sqlParams = new Dictionary<string, object>();
                    sqlParams.Add("id", id);
                    sqlParams.Add("docId", DoctorId);
                    sqlParams.Add("operatorId", @operator.Id);
                    sqlParams.Add("datePos", VisitDate);
                    sqlParams.Add("dateCreate", CreateRecordDate);
                    sqlParams.Add("talonId", TalonId);
                    sqlParams.Add("servicePlaceCode", 'П'); // П - код поликлиники
                    sqlParams.Add("purposeId", PurposeId);
                    
                    //var r = db.Execute(qCreate);
                    var r = db.Connection.Execute(q, sqlParams);
                    if (r > 0)
                    {
                        CreateTextTemplatesInDb();
                        OnCreated();
                    }
                    else
                    {
                        new DbWorkerException("Запрос не создал строку в базе!") {Sql = q};
                    }
                }
            }
            
        }

        private void CreateTextTemplatesInDb()
        {
            using (var db = new DbWorker())
            {
                //var qA = new DbQuery("SetAnamnez");

                var qA = "INSERT INTO visit_anamnez_tab(visit_id) VALUES(@id);";
                //qA.AddParamWithValue("@id", Id);

                //var qC = new DbQuery("SetComplaint");
                var qC = "INSERT INTO visit_complaint_tab(visit_id) VALUES(@id);";
                //qC.AddParamWithValue("@id", Id);

                //var qOS = new DbQuery("SetObjStatus");
                //qOS.Sql = "INSERT INTO visit_obj_status_tab(visit_id) VALUES(@id);";
                //qOS.AddParamWithValue("@id", Id);
                var qOS = "INSERT INTO visit_obj_status_tab(visit_id) VALUES(@id);";

                var added = 0;
                added += db.Connection.Execute(qA, new {id=Id});

                added += db.Connection.Execute(qC, new {id=Id});

                added += db.Connection.Execute(qOS, new  {id=Id});
                if (added != 3)
                {
                    new DbWorkerException("Ошибка при добавлении ID визита в таблицу шаблонов!");
                }
            }
        }

        public event EventHandler<CancelableEventArgs> Deleting;
        public event EventHandler Deleted;
        private CancelableEventArgs _cancelEventArgs = null;
        public void OnDeleting()
        {
            _cancelEventArgs = new CancelableEventArgs();
            if (Deleting != null)
            {
                Deleting(this, _cancelEventArgs);
            }
        }

        public void OnDeleted()
        {
            if (Deleted != null)
            {
                Deleted(this, null);
            }
        }

        public bool CanDeleteFromDb(Operator @operator, out string message)
        {
            /*int daysToEdit = 7;
            if (CreateRecordDate < DateTime.Now.AddDays(-daysToEdit))
            {
                message = "Для данного визита превышен срок редактирования.";
                return false;
            }*/

            if (DoctorId != @operator.DoctorId)
            {
                message = "Данный визит создан другим доктором.";

                return false;
            }
            message = string.Empty;
            return true;
        }

        public void DeleteFromDb(Operator @operator)
        {
            OnDeleting();
            if (_cancelEventArgs!=null && _cancelEventArgs.Cancel)
                return;
            using (var db = new DbWorker())
            {
                //var q = new DbQuery("DeleteVisit");
                var q = "DELETE FROM visit_tab WHERE visit_id = @id;";
                //q.AddParamWithValue("@id", Id);

                var r = db.Connection.Execute(q, new {id=Id });
                if (r > 0)
                {
                    OnDeleted();
                }
            }
        }

        public event EventHandler Saved;
        public event EventHandler Saving;
        public bool IsSaved { get; set; }
        public void OnSaving()
        {
            if (Saving != null)
            {
                Saving(this, null);
            }
        }

        public void OnSaved()
        {
            IsSaved = true;
            if (Saved != null)
            {
                Saved(this, null);
            }
        }

        public bool CanSave(Operator @operator, out string message)
        {
            if (DoctorId != @operator.DoctorId)
            {
                message = string.Format("Визит от {0} создал другой врач. Изменения не сохранены!",
                    VisitDate.Value.ToShortDateString());
                return false;
            }


            message = "";
            return true;
        }

        public void Save(Operator @operator)
        {
            if (Id == -1)
            {
                CreateInDb(@operator);
            }

            OnSaving();

            using (var db = new DbWorker())
            {
                //var q = new DbQuery("UpdateVisit");
                var q = "UPDATE public.visit_tab  SET " +
                        "doctor_id = @doctor_id, " +
                        "nurse_id = @nurse_id, " +
                        "data_pos = @data_pos, " +
                        "target_pos_id = @target_pos_id, " +
                        "data_vnes_inf = @record_date, " +
                        "operat_id = @operator_id, " +
                        "place_service_id = @service_place, " +
                        "talon_id = @talon_id, " +
                        "vid_oplat_id = @payment_id, " +
                        "is_primary = @is_primary, " +
                        "skl_kard = @skl_kard, " +
                        "dan_id = @patient_id " +
                        "WHERE visit_id = @visit_id;";

                var sqlParams = new Dictionary<string,object>();
                
                sqlParams.Add("doctor_id", DoctorId);
                sqlParams.Add("nurse_id", NurseId);
                sqlParams.Add("data_pos", VisitDate.HasValue ? (DateTime?)VisitDate.Value.Date : null);
                sqlParams.Add("target_pos_id", PurposeId == -1 ? null : (long?)PurposeId);
                sqlParams.Add("record_date", CreateRecordDate);
                sqlParams.Add("operator_id", @operator.Id);
                sqlParams.Add("service_place", ServicePlaceId == -1 ? null : (long?) ServicePlaceId);
                sqlParams.Add("talon_id", TalonId);
                sqlParams.Add("payment_id", PaymentTypeId == -1 ? null : (long?)PaymentTypeId);
                sqlParams.Add("is_primary", IsFirst ? (bool?)IsFirst : null);
                sqlParams.Add("skl_kard", HasSanCard ? (bool?)HasSanCard : null);
                sqlParams.Add("patient_id", PatientId);
                sqlParams.Add("visit_id", Id);
                
                var r = db.Connection.Execute(q, sqlParams);
                if (r > 0)
                {
                    OnSaved();
                }

                
                //повторяем сохранение 2 раза
                //for (int i=0; i<2;i++)
                //{
                //    var qA = "UPDATE visit_anamnez_tab set text_description = @text where visit_id = @id";
                //    var qC = "UPDATE visit_complaint_tab set text_description = @text where visit_id = @id";
                //    var qOS = "UPDATE visit_obj_status_tab set text_description = @text where visit_id = @id";

                //    int result = 0;
                //    result += db.Connection.Execute(qA, new { id = Id, text = TextAnamnez });
                //    result += db.Connection.Execute(qC, new { id = Id, text = TextComplaint });
                //    result += db.Connection.Execute(qOS, new { id = Id, text = TextObjStatus });
                //}

                SaveVisitTextInfo("visit_anamnez_tab", TextAnamnez);
                SaveVisitTextInfo("visit_complaint_tab", TextComplaint);
                SaveVisitTextInfo("visit_obj_status_tab", TextObjStatus);
            
            }
            
        }

        
        public void SaveVisitTextInfo(string table, string infoText)
        {
            var saved=false;
            using(var db = new DbWorker())
            {
                while (!saved)
                {
                    var q = string.Format("UPDATE {0} set text_description = @text where visit_id = @id", table );
                    db.Connection.Execute(q, new { id = Id, text = infoText });
                    var qExist = string.Format("Select text_description from {0} where visit_id=@id", table);
                    var savedText = db.Connection.Query<string>(qExist, new { id = Id }).FirstOrDefault();
                    if (savedText!=null)
                    {
                        saved = savedText.Equals(infoText);
                    }else
                    {
                        saved = false;
                    }
                        
                }
            }
            
        }

    }
}
