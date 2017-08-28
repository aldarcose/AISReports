using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.ComponentModel.DataAnnotations;
using Model.Classes.Codifiers;

namespace Model.Classes.DopDisp
{
    [Table("dop_disp_diagns_tab")]
    public class DopDispDiagnos
    {
        
        [Dapper.Key]
        [Column("id")]
        public long? Id { get; set; }
        
        [Column("dop_disp_main_id")]
        public long? DopDispTotalId { get; set; }

        [Display(Name = "Диагноз")]
        [Column("mkb_code")]
        public string MKBCode { get; set; }

        [Column("sp_disease_vid_id")]
        public long? DiseaseVidId { get; set; }

        [NotMapped]
        public DiagnoseType DiseaseVid { 
            get 
            {
                if (DiseaseVidId.HasValue)
                    return CodifiersHelper.GetDiseaseType(DiseaseVidId.Value);
                else
                    return null;
            } 
        }

        [Column("diagn_stage_id")]
        public long? StageId { get; set; }

        [NotMapped]
        public DiseaseStadia Stage
        {
            get {
                if (StageId.HasValue)
                    return CodifiersHelper.GetDiseaseStadia(StageId.Value);
                else
                    return null;
            }
        }

        [Column("doctor_id")]
        public long? DoctorId { get; set; }

        [NotMapped]
        public Doctor Doctor
        {
            get {
                var doc = new Doctor();
                doc.LoadData(DoctorId.Value);
                return doc;
            }
        }

        [Column("etap")]
        public string Etap { get; set; }

        [Display(Name = "Дата")]
        [Column("date_vnes_inf")]
        public DateTime? DateVnesInf { get; set; }
    }


    public class DopDispDiagnosView
    {
        public long? Id { get; set; }
        public string MKB { get; set; }
        public string Stage { get; set; }
        public string Etap { get; set; }
        public string DiseaseVid { get; set; }
    }


}
