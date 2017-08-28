using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Research
{
    public abstract class BaseResearch
    {
        private long _lpuId;

        protected BaseResearch()
        {
        }
        /// <summary>
        /// Название исследования
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Дата исследования
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Код лпу владельца записи
        /// </summary>
        public string RecordOwnerLpuId { get; set; }

        /// <summary>
        /// Идентификатор лпу, проводившего исследование
        /// </summary>
        public long LpuId
        {
            get { return _lpuId; }
            set
            {
                _lpuId = value;
                _lpuName = GetLpuName();
            }
        }

        private string _lpuName;
        public string LpuName
        {
            get { return _lpuName; }
        }

        /// <summary>
        /// Идентификатор пациента
        /// </summary>
        public long PatientId { get; set; }

        public string GetLpuName()
        {
            var name = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRiskName");
                q.Sql = "select name from codifiers.lpu_tab where id = @id;";
                q.AddParamWithValue("id", LpuId);

                name = DbResult.GetString(db.GetScalarResult(q), string.Empty);
            }
            return name;
        }
    }
}
