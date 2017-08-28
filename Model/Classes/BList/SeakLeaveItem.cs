using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.SeakLeave
{
    public class SeakLeaveItem
    {
        //blist_id
        public long? Id { get; set; }

        //data_begin
        public DateTime? DateBegin { get; set; }

        //data_end
        public DateTime? DateEnd { get; set; }
        
        //diag_mkb_id
        public string Diagnos { get; set; }
        
        //dan_id
        public long? PatientId { get; set; }
        
        //talon_id
        public long? TalonId { get; set; }
        
        //doctor_id
        public long? DoctorId { get; set; }
        
        //prichina_id
        public long? CauseId { get; set; }

        //prichina_additional_id
        public long? CauseAdditionalId { get; set; }

        //prichina_change_id
        public long? CauseChangeId { get; set; }
        
        //ser
        public string Serial { get; set; }
        
        //nomer
        public string Number { get; set; }
        
        //prev_blist_num
        public string PrevItemId { get; set; }
        
        //doctor_id_closed
        public long? DoctorIdClosed { get; set; }

        //type_id
        public long? TypeId { get; set; }

        //lpu_id
        public long? LpuId { get; set; }

        //pregnancy_twelve_flag
        public bool? PregnancyTwelveFlag { get; set; }

        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }

        //date_start_work
        public DateTime? DateStartWork { get; set; }

        //mest_work_id
        public long? WorkId { get; set; }

        //comment
        public string Comment { get; set; }

        //date_issue
        public DateTime? DateIssue { get; set; }

        //san_number
        public string SanNumber { get;set; }

        //san_ogrn
        public string SanOgrn { get; set; }

        //regiment_violation_id
        public long? RegimentViolationId { get; set; }

        //regiment_violation_date
        public DateTime? RegimentViolationDate { get; set; }

        //mse_naprav_date
        public DateTime? MSENapravDate { get; set; }

        //mse_reg_date
        public DateTime? MSERegDate { get; set; }

        //mse_examine_date
        public DateTime? MSEExamineDate { get; set; }

        //inval_id
        public long? InvalId { get; set; }

        //other_id
        public long? OtherId { get; set; }

        //blist_other_date
        public DateTime? OtherDate { get; set; }

        //num_other_next
        public string OtherNumNext { get; set; }

        public IList<SeakLeaveExtend> Extends { get; set; }

        //jobless_flag
        public bool? Jobless { get; set; }

        //other_work_id
        public long? OtherWorkId { get; set; }

    }
}
