using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Model.Classes.DopDisp
{
    [Table("dop_disp_main_tab")]
    public class DopDispTotal
    {
        [Key]
        [Column("id")]
        public long? Id { get;set; }
        [Column("date_begin")]
        public DateTime? DateBegin { get; set; }
        [Column("date_end")]
        public DateTime? DateEnd { get; set; }
        [Column("result_id")]
        public long? ResultId { get; set; }
        [Column("dan_id")]
        public long? PatientId { get; set; }

        [Column("sp_health_group_id")]
        public long? HealthGroupId { get; set; }

        [Column("incompletion_reason_id")]
        public long? IncompletionReasonId { get; set; }

        [Column("result_stage_1")]
        public long? ResultStage1 { get; set; }
        [Column("result_stage_2")]
        public long? ResultStage2 { get; set; }
        [Column("doctor_id")]
        public long? DoctorId { get; set; }
        [Column("first_stage_date_end")]
        public DateTime? FirstStageDateEnd { get; set; }
        [Column("dop_disp_type_id")]
        public long? TypeId { get; set; }

        [Column("sent_to_disp")]
        public bool? SentToDisp { get; set; }

        [Column("disp_group_id")]
        public int? DispGroup { get; set; }

        public override string ToString()
        {
            return  string.Format("{0}-{1}({2})", 
                    DateBegin.Value.ToShortDateString(), 
                    (DateEnd.HasValue) ? DateEnd.Value.ToShortDateString() : string.Empty,
                    Id.Value
                    );
        }

        [NotMapped]
        public string Info
        {
            get
            {
                return this.ToString();
            }
        }

        [ReadOnly(true)]
        [Column("errors_common")]
        public string Errors { get; set; }

        public IList<DopDispDiagnos> DiagnosItems { get; set; }
    }


    

}
