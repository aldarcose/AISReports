using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Classes.SeakLeave
{
    public class SeakLeaveExtend
    {
        
        [Display(AutoGenerateField=false)]
        public long? Id { get; set; }
        //blist_id
        [Display(AutoGenerateField = false)]
        public long? SeakLeaveId { get;set;}
        [Display(Name="С какого числа")]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "По какое число")]
        public DateTime? DateFor { get; set; }
        [Display(AutoGenerateField = false)]
        public long? DoctorId { get; set; }
        [Display(AutoGenerateField = false)]
        public Doctor Doctor { get; set; }
        
        [Display(Name="ФИО врача")]
        public string DoctorFIO { get { return (Doctor!=null) ? Doctor.FIO : string.Empty; } }
        
        [Display(Name="Должность")]
        public string DoctorPosition { get { return (Doctor!=null) ? Doctor.PositionName: string.Empty; } }
        
        //public string  DoctorFIO { get; set; }
        //public string DoctorPosition { get; set; }
        [Display(AutoGenerateField = false)]
        public long? ChiefDoctorId { get; set; }
        [Display(AutoGenerateField = false)]
        public Doctor ChiefDoctor { get; set; }
        [Display(Name="ФИО председателя")]
        public string ChiefDoctorFIO { get { return (ChiefDoctor!=null) ? ChiefDoctor.FIO : string.Empty; } }
        
        [Display(Name = "Председатель ВК")]
        public string ChiefDoctorPosition { get { return (ChiefDoctor!=null) ? ChiefDoctor.PositionName : string.Empty; } }
        [Display(AutoGenerateField = false)]
        public int? Editable { get; set; }
        [Display(AutoGenerateField = false)]
        public int? TypeId { get; set; }
        [Display(AutoGenerateField = false)]
        public int? Printed { get; set; }

    }
}
