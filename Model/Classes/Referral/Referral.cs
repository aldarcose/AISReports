using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using System.ComponentModel.DataAnnotations;
using NLog;
using Dapper;

namespace Model.Classes.Referral
{
    

    public class Referral : ISaveable
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Referral()
        {
            ReferralDate = DateTime.Now;
        }

        /// <summary>
        /// Id направдения в табл. public.naprav_tab
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Ид пациента. dan_id
        /// </summary>
        public long? PatientId { get; set; }

        /// <summary>
        /// Ид пациента. dan_id
        /// </summary>
        [Display(ShortName = "Пациент")]
        public Patient Patient { get; set; }

        /// <summary>
        /// Ид типа направления. sp_type_napr_id
        /// </summary>
        public long? ReferralTypeId { get; set; }


        /// <summary>
        /// Тип направления.
        /// </summary>
        [Display(ShortName = "Тип")]
        public ReferralType ReferralType { get; set; }

        /// <summary>
        /// Ид обоснования. sp_napr_obosn_id
        /// </summary>
        public long? ReferralReasonId { get; set; }

        /// <summary>
        /// Обоснование.
        /// </summary>
        [Display(ShortName = "Причина")]
        public ReferralReason ReferralReason { get; set; }
        
        /// <summary>
        /// Флаг военкомат. from_voenkomat.
        /// </summary>
        [Display(ShortName = "Военкомат")]
        public bool? IsVoenkomat { get; set; }
        
        /// <summary>
        /// Флаг иногородний. is_inogorod
        /// </summary>
        [Display(ShortName = "Иногородний")]
        public bool? IsNotLocal { get; set; }

        /// <summary>
        /// Описание. opis_diagn
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата оплаты. pay_date
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// Дата направления. date_destination
        /// </summary>
        public DateTime? ReferralDate { get; set; }
        
        /// <summary>
        /// Дата госпитализации. stacionar_date
        /// </summary>
        public DateTime? HospitalDate { get; set; }

        /// <summary>
        /// Ид доктора. doctor_id
        /// </summary>
        public long? DoctorId { get; set; }

        /// <summary>
        /// Врач
        /// </summary>
        [Display(ShortName = "Врач")]
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Диагноз. sp_mkb_code
        /// </summary>
        [Display(ShortName = "Код MKБ")]
        public string MkbCode { get; set; }

        /// <summary>
        /// Номер талона. talon_id
        /// </summary>
        public long? TalonId { get; set; }

        /// <summary>
        /// ид откуда отправили.
        /// </summary>
        public long? ReferralLpuFromId { get; set; }

        /// <summary>
        /// ид откуда отправили.
        /// </summary>
        public ReferralLpu ReferralLpuFrom { get; set; }

        ///<summary>
        /// Ид ЛПУ, куда направляют пациента. sp_naprav_lpu_id
        /// </summary>
        public long? ReferralLpuId { get;set; }

        /// <summary>
        /// Ид ЛПУ, куда направляют пациента.
        /// </summary>
        [Display(ShortName = "Направлен")]
        public ReferralLpu ReferralLpu { get; set; }


        /// <summary>
        /// Ид отдела ЛПУ. otdel_code
        /// </summary>
        public long? ReferralLpuDepartmentId { get; set; }

        /// <summary>
        /// Ид отдела ЛПУ. 
        /// </summary>
        [Display(ShortName = "Отдел")]
        public ReferralLpuDepartment ReferralLpuDep { get; set; }

        /// <summary>
        /// ид специальности. spec_doctor_id 
        /// </summary>
        public long? ReferralSpecialityId { get; set; }
        
        /// <summary>
        /// Специальность куда направляют.
        /// </summary>
        [Display(ShortName = "Специальность")]
        public MedSpeciality ReferralSpeciality { get; set; }

        /// <summary>
        /// Номер направления. naprav_number
        /// </summary>
        [Display(ShortName = "Номер")]
        public long? Number { get; set; }

        /// <summary>
        /// Ид оператора. operator_id
        /// </summary>
        public long? OperatorId { get; set; }

        [Display(ShortName = "Оператор")]
        public Operator Operator { get; set; }

        public long? MedFormId { get;set;}

        [Display(ShortName = "Форма медпомощи")]
        public ReferralMedForm MedForm { get; set; }

        public long? CancelId { get; set; }

        [Display(ShortName = "Аннулирование")]
        public ReferralCancel Cancel { get; set; }

        [Display(ShortName = "Согласованная дата")]
        public DateTime? AgreedDate { get; set; }

        [Display(ShortName = "Цель")]
        public string Aim { get; set; }

        
        public event EventHandler Saved;
        public event EventHandler Saving;
        public bool IsSaved { get; set; }
        public void OnSaving()
        {
            IsSaved = false;
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
            // проверяет на достаточность данных - что все указано и не противоречит ничему3.\\.
            if (ReferralLpu == null)
            {
                message = "Не указано учреждение";
                return false;
            }
            
            if (ReferralDate == default(DateTime))
            {
                message = "Не указана дата";
                return false;
            }

            if (string.IsNullOrEmpty(MkbCode) )
            {
                message = "Не указан код МКБ";
                return false;
            }

            if (ReferralTypeId == null)
            {
                message = "Не указан тип направления";
                return false;
            }

            if (DoctorId == default(long)){
                message = "Не указан врач";
                return false;
            }

            if (ReferralDate == default(DateTime))
            {
                message = "Не указана дата направления";
                return false;
            }

            message = null;
            return true;
        }

        private long? GetNextNumber()
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("get_naprav_next_number");
                q.Sql = @"select case when naprav_number is null then 1 else naprav_number end from 
                            (
                            select  
                            max(naprav_number)+1 naprav_number
                            from public.naprav_tab 
                            where naprav_year_counter is not null and naprav_year_counter = EXTRACT(year from current_date)
                            ) as q";
                var result = (long?)db.GetScalarResult(q);
                if (result.HasValue)
                    return result.Value;
                else
                    return null;

            }
        }

        public void Save(Operator @operator)
        {
            OnSaving();
            // здесь реализуем либо инсерт либо апдейт в зависимости от какого-нибудь флага (я использую Id , если не -1, то апдейт, иначе инсерт)
            //throw new NotImplementedException();
            string querytext = string.Empty;
            using (var db = new DbWorker())
            {
                var query = new DbQuery("add_update_referral");
                if (Id == null || Id == -1)
                {
                    querytext = @"insert into public.naprav_tab(
                            lpu_id,
                            operator_id,
                            dan_id, 
                            sp_naprav_lpu_id,
                            lpy_from_id,
                            sp_mkb_code,
                            talon_id,
                            date_destination,
                            diagn_id,
                            naprav_number, 
                            opis_diagn,
                            sp_type_napr_id,
                            is_inogorod,
                            from_voenkomat,
                            doctor_id,
                            otdel_code,
                            spec_doctor_id,
                            pay_date,
                            stacionar_date,
                            naprav_year_counter,
                            form_okaz_id,
                            napr_cancel_reason_id,
                            agreeddate,
                            agreedtime,
                            sp_naprav_obosn_id  
                            ) values(
                            @lpu_id,
                            @operator_id,
                            @dan_id,
                            @sp_naprav_lpu_id,
                            @lpu_id_from,
                            @mkb_code,
                            @talon_id,
                            @date_destination,
                            @diagn_id,
                            @naprav_number,
                            @opis_diagn,
                            @refferal_type,
                            @is_inogorod,
                            @from_voenkomat,
                            @doctor_id,
                            @otdel_code,
                            @spec_doctor_id,
                            @pay_date,
                            @stacinar_date,
                            @year_counter,
                            @medform,
                            @cancel,
                            @agreeddate,
                            @agreedtime,
                            @reasonid
                            ) RETURNING id";
                    Number = Number ?? GetNextNumber();
                }
                else //update
                {
                    querytext = @"update public.naprav_tab set 
                            lpu_id=@lpu_id,
                            operator_id=@operator_id,
                            dan_id=@dan_id, 
                            sp_naprav_lpu_id=@sp_naprav_lpu_id,
                            lpy_from_id=@lpu_id_from,
                            sp_mkb_code=@mkb_code,
                            talon_id=@talod_id,
                            date_destination=@date_destination,
                            diagn_id=@diagn_id,
                            naprav_number=@naprav_number, 
                            opis_diagn=@opis_diagn,
                            sp_type_napr_id=@refferal_type,
                            is_inogorod=@is_inogorod,
                            from_voenkomat=@from_voenkomat,
                            doctor_id=@doctor_id,
                            otdel_code = @otdel_code,
                            spec_doctor_id=@spec_doctor_id,
                            pay_date=@pay_date,
                            stacionar_date=@stacinar_date,
                            naprav_year_counter=@year_counter,
                            form_okaz_id=@medform,
                            napr_cancel_reason_id=@cancel,
                            agreeddate=@agreeddate,
                            agreedtime=@agreedtime,
                            sp_naprav_obosn_id=@reasonid
                            where id=@id";
                    query.AddParamWithValue("@id", Id);
                }
                
                query.Sql = querytext;
                logger.Debug(querytext);

                var args = new Dictionary<string, object>();

                args.Add("lpu_id", "03001");
                args.Add("operator_id", @operator.Id);
                args.Add("dan_id", Patient.Id);
                args.Add("sp_naprav_lpu_id", ReferralLpu.Id);
                args.Add("lpu_id_from", 2301001);
                args.Add("mkb_code", MkbCode);
                
                args.Add("date_destination", ReferralDate);
                args.Add("diagn_id", MkbCode);
                args.Add("naprav_number", Number);
                args.Add("opis_diagn", Description);
                args.Add("refferal_type", ReferralTypeId);
                args.Add("is_inogorod", (IsNotLocal.HasValue) ? Convert.ToInt16(IsNotLocal.Value): 0 );
                args.Add("from_voenkomat", (IsVoenkomat.HasValue) ? Convert.ToInt16(IsVoenkomat.Value) : 0);
                args.Add("doctor_id", DoctorId);
                
                if (ReferralLpuDepartmentId!=null)
                    args.Add("otdel_code", (object)ReferralLpuDepartmentId);
                else
                    args.Add("otdel_code", null);
                
                if(ReferralSpecialityId!=null)
                    args.Add("spec_doctor_id", ReferralSpecialityId);
                else
                    args.Add("spec_doctor_id", null);
                
                args.Add("talon_id", (object)TalonId ?? null);
                args.Add("pay_date", (object)PaymentDate ?? null);
                args.Add("stacinar_date", (object)HospitalDate ?? null);
                args.Add("year_counter", ReferralDate.Value.Year);

                if (Cancel!=null)
                    args.Add("cancel", (object)Cancel.Id ?? null);
                else
                    args.Add("cancel", null);
                

                if (MedForm != null)
                    args.Add("medform", (object)MedForm.Id ?? null);
                else
                    args.Add("medform", null);

                if (AgreedDate!=null)
                {
                    args.Add("agreeddate", AgreedDate.Value.ToShortDateString());
                    args.Add("agreedtime", AgreedDate.Value.TimeOfDay.ToString());
                }
                else
                {
                    args.Add("agreeddate", null);
                    args.Add("agreedtime", null);
                }
                
                if(ReferralReasonId.HasValue) 
                    args.Add("reasonid", ReferralReasonId.Value);
                else
                    args.Add("reasonid", null);
                
                

                if (Id == null)
                {
                    var result = db.Connection.ExecuteScalar(querytext, args);
                    if (result != DBNull.Value)
                    {
                        Id = Convert.ToInt64(result);
                        logger.Debug("Result new Id={0}", result);
                    }
                }
                else
                {
                    var result=db.Connection.Execute(querytext, args);
                    logger.Debug("Result updated {0}", result);
                }

                
                OnSaved();
    
                
            }
            
        }
    }
}
