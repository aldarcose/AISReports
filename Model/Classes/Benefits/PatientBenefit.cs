using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Classes.Benefits
{
    public class PatientBenefit
    {
        public long? Id { get; set; }

        public long? PatientId { get; set; }

        //lgota_id
        public string BenefitId { get; set; }
        public string BenefitName { get; set; }

        //entry_reason_id
        public long? EntryReasonId {get; set;}

        public string EntryReason { get; set; }
        
        //entry_date
        public DateTime? EntryDate { get; set; }
        
        //diagn_id
        public string Diagnosis { get; set; }
        
        //doctor_id
        public long? DoctorId { get; set; }
        
        //
        public string Doctor { get; set; }
        
        //entry_lpu
        public string EntryLPUId {get; set;}
        
        
        //leave_reason_id
        public long? RemovalReasonId { get; set; }

        //leave_date
        public DateTime? RemovalDate { get; set; }
        
        public string RemovalReason { get; set; }
        
        //next_date
        public DateTime? NextVisit { get; set; }

        //sp_lgot_doc_type_id
        public long? DocumentId { get; set; }
        
        public string Document { get; set; }

        //ser_lgot_doc
        public string Series { get; set; }

        //num_lgot_doc
        public string Number { get; set; }

        //lkk_date
        public DateTime? LKKDate {get;set;}

        //epikriz_lkk_id
        public long? EpicrisId { get; set; }

        //operator_id
        public long? OperatorId { get; set; }

    }



    
}
