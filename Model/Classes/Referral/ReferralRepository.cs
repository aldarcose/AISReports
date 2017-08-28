using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;

namespace Model.Classes.Referral
{
    public static class ReferralRepository
    {

        public static ReferralLpu GetReferralLpu(long? id)
        {
            using (var db = new DbWorker())
            {
                var query = "select id,full_name fullname,ogrn,okvd,name,code,address from codifiers.lpu_tab where id=@Id";
                var lpu= db.Connection.Query<ReferralLpu>(query, new {Id = id}).FirstOrDefault();
                return lpu;
            }
        }
        public static ReferralLpuDepartment GetReferralLpuDepartment(long? id)
        {
            using (var db = new DbWorker())
            {
                var query = "select id,code,name from codifiers.sp_lpu_otdels_tab where id=@Id";
                var dep = db.Connection.Query<ReferralLpuDepartment>(query, new { Id = id }).FirstOrDefault();
                return dep;
            }
        }

        public static ReferralReason GetReferralReason(long? id)
        {
            using (var db = new DbWorker())
            {
                var query = "select sp_napr_obosn_id id,trim(obosn_name) as name FROM sp_napr_obosn_tab where sp_napr_obosn_id=@Id";
                var result = db.Connection.Query<ReferralReason>(query, new { Id = id }).FirstOrDefault();
                return result;
            }
        }

        public static ReferralType GetReferralType(long? id)
        {
            using (var db = new DbWorker())
            {
                var query = "SELECT sp_type_napr_id id, sp_type_napr as name  FROM sp_type_napr_tab  where sp_type_napr_id=@Id";
                var result = db.Connection.Query<ReferralType>(query, new { Id = id }).FirstOrDefault();
                return result;
            }
        }

        public static MedSpeciality GetReferralSpecality(long? referralSpecialityId)
        {
            using (var db = new DbWorker())
            {
                var q = "SELECT id, code, name FROM codifiers.doctor_spec_tab WHERE id=@id";
                var result = db.Connection.Query<MedSpeciality>(q, new{id=referralSpecialityId}).FirstOrDefault();
                return result;
            }
        }

        public static ReferralMedForm GetReferralForm(long? id)
        {
            using (var db = new DbWorker())
            {
                return
                    db.Connection.Query<ReferralMedForm>("select id, \"name\" from codifiers.napr_form_okaz_tab where id=@Id", new {Id=id}).FirstOrDefault();
            }
        }


        public static ReferralCancel GetReferralCancel(long? id)
        {
            using (var db = new DbWorker())
            {
                return
                    db.Connection.Query<ReferralCancel>("select id, \"name\" from codifiers.napr_cancel_reasons_tab where id=@Id", new {Id=id}).FirstOrDefault();
            }
        }
        



        public static List<Referral> GetItems(long? patientID)
        {
            if (!patientID.HasValue) return null;

            var referralList = new List<Referral>();

            using (var db = new DbWorker())
            {
//                var query = @"select 
//                            id, 
//                            dan_id patientid, 
//                            sp_type_napr_id referraltypeid,
//                            sp_naprav_obosn_id referralreasonid,
//                            from_voenkomat isvoencomat,
//                            is_inogorod isnotlocal,
//                            opis_diagn descripton,
//                            pay_date paymentdate,
//                            date_destination referraldate,
//                            stacionar_date hospitaldate,
//                            doctor_id doctorid,
//                            sp_mkb_code mkbcode,
//                            talon_id talonid,
//                            lpy_from_id referrallpufromid,
//                            sp_naprav_lpu_id referrallpuid,
//                            otdel_code referrallpudepartmentid,
//                            spec_doctor_id referralspecialityid,
//                            naprav_number number,
//                            operator_id operatorid
//                            from naprav_tab nap 
//                            where dan_id=@patient_id";
                var query = @"select 
                            id, 
                            dan_id patientid, 
                            doctor_id doctorid,
                            sp_mkb_code mkbcode,
                            date_destination referraldate,
                            sp_naprav_lpu_id referrallpuid,
                            naprav_number number,
                            operator_id operatorid,
                            sp_type_napr_id referraltypeid
                            from naprav_tab nap 
                            where dan_id=@patient_id order by date_destination desc
                            ";
                var referrals =   db.Connection.Query<Referral>( query, new {patient_id = patientID});

                foreach (var item in referrals)
                {
                    InitRefs(item);
        
                }

                return referrals.ToList();

            }
        }

        private static void InitRefs(Referral item)
        {
            if (item.DoctorId!=null)
            {   
                var doctor = new Doctor();
                doctor.LoadData(item.DoctorId.Value);
                item.Doctor = doctor;
            }

            if (item.ReferralReasonId!=null)
                item.ReferralReason = GetReferralReason(item.ReferralReasonId);
            if (item.ReferralLpuId!=null)
                item.ReferralLpu = GetReferralLpu(item.ReferralLpuId);
            if (item.ReferralTypeId != null)
                item.ReferralType = GetReferralType(item.ReferralTypeId);
            if (item.ReferralLpuDepartmentId != null)
                item.ReferralLpuDep = GetReferralLpuDepartment(item.ReferralLpuDepartmentId);
            if (item.ReferralSpecialityId != null)
                item.ReferralSpeciality = GetReferralSpecality(item.ReferralSpecialityId);
            if (item.ReferralSpecialityId != null)
                item.ReferralSpeciality = GetReferralSpecality(item.ReferralSpecialityId);
            if (item.ReferralLpuFromId != null)
                item.ReferralLpuFrom = GetReferralLpu(item.ReferralLpuFromId);
            if (item.Patient == null && item.PatientId.HasValue)
            {
                var patient = new Patient();
                if (patient.LoadData(item.PatientId.Value))
                    item.Patient = patient;
            }

            if (item.MedFormId.HasValue)
                item.MedForm = GetReferralForm(item.MedFormId.Value);

            if (item.CancelId.HasValue)
                item.Cancel = GetReferralCancel(item.CancelId);

        }

        public static Referral GetItem(long? referralId)
        {
            if (!referralId.HasValue) return null;

            var referralList = new List<Referral>();

            using (var db = new DbWorker())
            {
                var query = @"select 
                            id, 
                            dan_id patientid, 
                            sp_type_napr_id referraltypeid,
                            sp_naprav_obosn_id referralreasonid,
                            from_voenkomat isvoencomat,
                            is_inogorod isnotlocal,
                            opis_diagn description,
                            pay_date paymentdate,
                            date_destination referraldate,
                            stacionar_date hospitaldate,
                            doctor_id doctorid,
                            sp_mkb_code mkbcode,
                            talon_id talonid,
                            sp_naprav_lpu_id referrallpuid,
                            otdel_code referrallpudepartmentid,
                            spec_doctor_id referralspecialityid,
                            naprav_number number,
                            operator_id operatorid,
                            lpy_from_id referrallpufromid,
                            agreeddate+agreedtime agreeddate,
                            aim,
                            sp_napr_obosn_id id, trim(obosn_name) ""name"" 
                            from naprav_tab nap
                            left join sp_napr_obosn_tab reason on reason.sp_napr_obosn_id=sp_naprav_obosn_id 
                            where id=@Id";
                var referrals = db.Connection.Query<Referral, ReferralReason, Referral>(query, 
                                (referral, reason)=>{referral.ReferralReason=reason; return referral;}, 
                                new { Id = referralId } );
                var item = referrals.FirstOrDefault();
                if (item !=null)
                    InitRefs(item);
                return item;
                
            }

        }

        public static void Delete(long? id)
        {
            var query = "delete from naprav_tab nap where id=@Id";
            using (var db = new DbWorker())
            {
                db.Connection.Execute(query, new {Id = @id});
            }
        }

        
    }
}
