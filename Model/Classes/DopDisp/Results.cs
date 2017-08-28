using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Model.Classes.DopDisp
{
    [Table("dop_disp_results_tab")]
    public class Results
    {
        [Column("id")]
        public long? Id { get; set; }
        [Column("operator_id")]
        public long? OperatorId {get;set;}
        [Column("dop_disp_main_id")]
        public long? TotalId { get; set; }
        [Column("smoker")]
        public bool? Smoker { get; set; }
        [Column("high_a_p")]
        public bool? HighAP { get; set; }
        [Column("weight_excess")]
        public bool? WeightExcess { get; set; }
        [Column("obesity")]
        public bool? Obesity { get; set; }
        [Column("hypercholesterinemia")]
        public bool? Hypercholesterinemia { get; set; }
        [Column("hyperglycemia")]
        public bool? Hyperglycemia { get; set; }
        [Column("insuff_phys_activity")]
        public bool? InsuffPhysActivity { get; set; }
        [Column("irrational_nutrition")]
        public bool? IrrationalNutrition { get; set; }
        [Column("alcohol")]
        public bool? Alcohol { get; set; }
        [Column("narcotics")]
        public bool? Narcotics { get; set; }
        [Column("sent_to_narkolog")]
        public bool? SentToNarkolog { get; set; }
        [Column("alcohol_addiction")]
        public bool? AlcoholAddiction { get; set; }
        [Column("inh_blood")]
        public string InhBlood { get; set; }
        [Column("inh_cancer")]
        public string InhCancer { get; set; }
        [Column("stenocardia_suspicion")]
        public bool? StenocardiaSuspicion { get; set; }
        [Column("recommended_duplex_scan")]
        public bool? Recommended_duplex_scan { get; set; }
        [Column("recommended_depth_counseling")]
        public string Recommended_depth_counseling { get; set; }
        [Column("lungs_suspicion")]
        public bool? Lungs_suspicion{ get; set; }
        [Column("recommended_ezofag")]
        public bool? Recommended_ezofag { get; set; }
        [Column("recommended_consultation_in")]
        public string Recommended_consultation_in { get; set; }
        [Column("recommended_consultation_out")]
        public string Recommended_consultation_out { get; set; }
        [Column("ad_s")]
        public int? ADs { get; set; }
        [Column("ad_d")]
        public int? ADd { get; set; }
        [Column("hypotensive_therapy")]
        public bool? Hypotensive_therapy { get; set; }
        [Column("weight_index")]
        public decimal? Weight_index { get; set; }
        [Column("weight")]
        public int? Weight { get; set; }
        [Column("height")]
        public int? Height { get; set; }
        [Column("waist")]
        public int? Waist { get; set; }
        [Column("cholesterol")]
        public decimal? Cholesterol { get; set; }
        [Column("glucose")]
        public decimal? Glucose { get; set; }
        [Column("hypoglycemic_therapy")]
        public bool? Hypoglycemic_therapy { get; set; }
        [Column("needs_treatment_amb")]
        public bool? Needs_treatment_amb { get; set; }
        [Column("needs_treatment_stac")]
        public bool? Needs_treatment_stac { get; set; }
        [Column("needs_treatment_spec")]
        public bool? Needs_treatment_spec { get; set; }
        [Column("needs_treatment_hi_tech")]
        public bool? Needs_treatment_hi_tech { get; set; }
        [Column("sssr_percentage")]
        public decimal? Sssr_percentage { get; set; }
        [Column("sssr_text")]
        public string Sssr_text { get; set; }
        [Column("needs_treatment_san_kur")]
        public bool? Needs_treatment_san_kur { get; set; }
        [Column("hypolipidemic_therapy")]
        public bool? Hypolipidemic_therapy { get; set; }
        [Column("testcol")]
        public int? Testcol { get; set; }
        [Column("dyslipidemia")]
        public bool? Dyslipidemia { get; set; }
        [Column("hereditary_tainted")]
        public bool? Hereditary_tainted { get; set; }
        [Column("stress")]
        public bool? Stress { get; set; }
    }


}
