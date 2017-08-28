using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Laboratory
{
    public class LabRequest
    {
        public long Id { get; set; }
        public List<RequestFilter> Filters { get; set; }
        public Patient Patient { get; set; }

        public DateTime CreateTime { get; set; }
        public LabRecordStatus Status { get; private set; }
        [Browsable(false)]
        public bool HasResults
        {
            get
            {
                var hasResultsStatus = 2;
                if (Status != null)
                    return Status.Id == hasResultsStatus;
                return false;
            }
        }
        public void Save()
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("InsertData");
                q.Sql = "INSERT INTO laboratory.request (dan_id) VALUES (@dan_id) returning id;";
                q.AddParamWithValue("@dan_id", Patient.PatientId);
                var id = db.GetScalarResult(q);
                if (id != null)
                {
                    this.Id = DbResult.GetNumeric(id, -1);
                }

                if (Id != -1)
                {
                    foreach (var filter in Filters)
                    {
                        var qq = new DbQuery("InsertFilterData");
                        qq.Sql =
                            "INSERT INTO laboratory.request_filter_data (request_id, filter_type, filter_value) VALUES (@request_id, @filter_type, @filter_value);";
                        qq.AddParamWithValue("@request_id", Id);
                        qq.AddParamWithValue("@filter_type", GetFilterTypeName(filter.Type));
                        qq.AddParamWithValue("@filter_value", filter.Value);

                        db.Execute(qq);
                    }
                    /*
                    var qqq = new DbQuery("UpdateStatus");
                    qqq.Sql = "UPDATE laboratory.\"order\" set status_id = @status where id = @id;";
                    qqq.AddParamWithValue("@id", Id);
                    qqq.AddParamWithValue("@status", 1); // меняем с 0 (создание) на 1 (в очередь). другая программа отправляет данные при изменении этого поля
                    db.Execute(qqq);*/
                }
            }

        }
        public void SetStatus(int status)
        {
            using (var db = new DbWorker())
            {
                var qqq = new DbQuery("UpdateStatus");
                qqq.Sql = "UPDATE laboratory.request set status_id = @status where id = @id;";
                qqq.AddParamWithValue("@id", Id);
                qqq.AddParamWithValue("@status", status); // меняем с 0 (создание) на 1 (в очередь). другая программа отправляет данные при изменении этого поля
                db.Execute(qqq);
            }
        }
        private string GetFilterTypeName(FilterType type)
        {
            switch (type)
            {
                case FilterType.Exam:
                    return "exam";
                case FilterType.DateFrom:
                    return "date_from";
                case FilterType.DateTo:
                    return "date_to";
                case FilterType.ExactDate:
                    return "date_exact";
                default:
                    break;
            }
            return null;
        }

        private FilterType GetFilterTypeValue(string value)
        {
            switch (value)
            {
                case "exam":
                    return FilterType.Exam;
                case "date_from":
                    return FilterType.DateFrom;
                case "date_to":
                    return FilterType.DateTo;
                case "date_exact":
                    return FilterType.ExactDate;
                default:
                    throw new ArgumentException(value);
            }
            return FilterType.Exam;
        }
        public void GetLabResults()
        {

        }
        public void Load()
        {

        }

        public void Load(long id)
        {
            this.Id = id;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetData");
                q.Sql = "select r.*, rfd.filter_type, rfd.filter_value, s.name as status from laboratory.request r inner join laboratory.request_filter_data rfd on rfd.request_id = r.id inner join laboratory.sp_order_status s on s.id = r.status_id where r.id = @id;";
                q.AddParamWithValue("@id", id);

                var results = db.GetResults(q);

                if (results != null && results.Count > 0)
                {
                    Filters = new List<RequestFilter>();

                    var dan_id = DbResult.GetNumeric(results[0].GetByName("dan_id"), -1);
                    this.Patient = new Patient();
                    this.Patient.LoadData(dan_id);

                    var date = DbResult.GetDateTime(results[0].GetByName("create_date"), DateTime.MinValue);
                    this.CreateTime = date;
                    Status = new LabRecordStatus()
                    {
                        Id = DbResult.GetNumeric(results[0].GetByName("status_id"), -1),
                        Status = DbResult.GetString(results[0].GetByName("status"), string.Empty)
                    };
                    foreach (var result in results)
                    {
                        var requestFilter = new RequestFilter();
                        var filterType = DbResult.GetString(result.GetByName("filter_type"), string.Empty);
                        if (!string.IsNullOrEmpty(filterType))
                        {
                            requestFilter.Type = GetFilterTypeValue(filterType);
                            requestFilter.Value = DbResult.GetString(result.GetByName("filter_value"), string.Empty);
                            Filters.Add(requestFilter);
                        }
                    }
                }
            }
        }

        public XElement GetXmlElement()
        {
            var root = new XElement("request");
            root.Add(new XAttribute("id", Id));

            var patient = new XElement("patient");
            patient.Add(new XElement("dan_id", Patient.PatientId));
            patient.Add(new XElement("last_name", Patient.LastName));
            patient.Add(new XElement("first_name", Patient.FirstName));
            if (!string.IsNullOrEmpty(Patient.MidName))
                patient.Add(new XElement("mid_name", Patient.MidName));
            patient.Add(new XElement("birthdate", Patient.BirthDate.ToString("yyyy-MM-dd")));
            patient.Add(new XElement("gender", Patient.Gender == Gender.Male ? "male" : "female"));

            root.Add(patient);

            foreach (var filter in Filters)
            {
                var filterElem = new XElement("filter");
                var typeAttr = new XAttribute("type", GetFilterTypeName(filter.Type));
                typeAttr.SetValue(filter.Value);
                filterElem.Add(typeAttr);
                root.Add(filterElem);
            }

            return root;
        }
    }
}
