using System;
using System.Collections.Generic;
using System.Linq;
using Model.Classes.AddressPart;
using Model.Classes.Codifiers;
using Model.Classes.Codifiers.Emergency;
using Model.Classes.Documents;
using Model.Classes.Referral;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;

namespace Model.Classes
{
    public static class CodifiersHelper
    {

        public static List<ReferralMedForm> GetReferralForms()
        {
            using (var db = new DbWorker())
            {
                return
                    db.Connection.Query<ReferralMedForm>("select id, \"name\" from codifiers.napr_form_okaz_tab").ToList();
            }
        }


        public static List<ReferralCancel> GetReferralCancels()
        {
            using (var db = new DbWorker())
            {
                return
                    db.Connection.Query<ReferralCancel>("select id, \"name\" from codifiers.napr_cancel_reasons_tab").ToList();
            }
        }


        public static List<ReferralLpu> GetReferralLpus()
        {
            var list = new List<ReferralLpu>();
            using (var db = new DbWorker())
            {
                var query = new DbQuery("GetReferralLpus");

                query.Sql = "SELECT * " +
                            "FROM codifiers.lpu_tab order by name";

                var results = db.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var item = new ReferralLpu();
                        item.Id = DbResult.GetNumeric(result.GetByName("id"), -1);
                        item.Code = DbResult.GetString(result.GetByName("code"), "");
                        item.Name = DbResult.GetString(result.GetByName("name"), "");
                        item.FullName = DbResult.GetString(result.GetByName("full_name"), "");
                        item.Address = DbResult.GetString(result.GetByName("address"), "");
                        item.Ogrn = DbResult.GetString(result.GetByName("ogrn"), "");
                        item.Okvd = DbResult.GetString(result.GetByName("okvd"), "");

                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static List<ReferralReason> GetReferralReasons()
        {
            var list = new List<ReferralReason>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetReferralReasons");
                q.Sql = "select sp_napr_obosn_id,trim(obosn_name) as obosn_name FROM sp_napr_obosn_tab ORDER BY sp_napr_obosn_id;";
                var results = db.GetResults(q);
                foreach (var dbResult in results)
                {
                    var id = DbResult.GetInt(dbResult.GetByName("sp_napr_obosn_id"), 0);
                    var name = DbResult.GetString(dbResult.GetByName("obosn_name"), "");
                    var type = new Referral.ReferralReason()
                    {
                        Id = id,
                        Name = name
                    };
                    list.Add(type);
                }
            }
            return list;
        }

        public static List<ReferralType> GetReferralTypes()
        {
            var list = new List<ReferralType>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetReferralTypes");
                q.Sql = "SELECT sp_type_napr_id, sp_type_napr FROM sp_type_napr_tab where sp_type_napr_id!=1 ORDER BY 1;";
                var results = db.GetResults(q);
                foreach (var dbResult in results)
                {
                    var id = DbResult.GetLong(dbResult.GetByName("sp_type_napr_id"), 0);
                    var name = DbResult.GetString(dbResult.GetByName("sp_type_napr"), "");
                    var type = new Referral.ReferralType()
                    {
                        Id = id,
                        Name = name
                    };
                    list.Add(type);
                }
            }
            return list;
        }

        public static List<MedArea> GetUchaski()
        {
            var list = new List<MedArea>();

            using (var dbWorker = new DbWorker())
            {
                DbQuery query = new DbQuery("GetUchastki");
                query.Sql = "Select " +
                            "u.id, u.code, u.name, u.doctor_id, u.otdel_id, u.age_from, u.age_for, u.flag_default, u.uchactok_type_id, u.lpu_id, u.sort, u.sex " +
                            "from codifiers.uchactok_tab u " +
                            "order by u.sort, u.name";
                var results = dbWorker.GetResults(query);

                foreach (var dbResult in results)
                {
                    var id = DbResult.GetLong(dbResult.Fields[0], 0);
                    var code = DbResult.GetString(dbResult.Fields[1], "");
                    var name = DbResult.GetString(dbResult.Fields[2], "");
                    var doctorId = DbResult.GetInt(dbResult.Fields[3], 0);
                    var otdelId = DbResult.GetLong(dbResult.Fields[4], 0);

                    var ageFrom = DbResult.GetInt(dbResult.Fields[5], 0);
                    var ageTo = DbResult.GetInt(dbResult.Fields[6], 0);

                    var defaultFlag = DbResult.GetInt(dbResult.Fields[7], 0);

                    var typeId = DbResult.GetInt(dbResult.Fields[8], 0);

                    /*var lpuId = DbResult.GetString(dbResult.Fields[9], "");

                    var sort = DbResult.GetInt(dbResult.Fields[10], 0);

                    var gender = DbResult.GetChar(dbResult.Fields[11], '-');*/

                    var uchastok = new MedArea
                    {
                        AgeFrom = ageFrom,
                        AgeTo = ageTo,
                        Code = code,
                        DoctorId = doctorId,
                        Id = id,
                        IsDefault = defaultFlag == 1,
                        Name = name,
                        DivisionId = otdelId,
                        Type = typeId
                    };

                    list.Add(uchastok);
                }
            }

            return list;
        }


        public static List<DiseaseStadia> GetDiseaseStadias()
        {
            var list = new List<DiseaseStadia>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDiseaseType");

                query.Sql = "Select sp_stadiya_diagn_id, name_stadiya " +
                            "from public.sp_stadiya_diagn_tab order by sp_stadiya_diagn_id";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new DiseaseStadia();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    list.Add(item);
                }
            }

            return list;
        }


        public static DiseaseStadia GetDiseaseStadia(long id)
        {
            var list = new List<DiseaseStadia>();

            using (var dbWorker = new DbWorker())
            {
                
                var sql = @"Select sp_stadiya_diagn_id id, name_stadiya ""name"" 
                            from public.sp_stadiya_diagn_tab 
                            where sp_stadiya_diagn_id=@Id
                           ";

                var result = dbWorker.Connection.Query<DiseaseStadia>(sql, new { Id=id }).FirstOrDefault();

                return result;
            }

            
        }


        public static List<DiseaseCharacter> GetDiseaseCharacters()
        {
            var list = new List<DiseaseCharacter>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDiseaseCharacters");

                query.Sql = "Select id, name, code, sort " +
                            "from codifiers.diagn_type_tab order by sort";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new DiseaseCharacter();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<DiagnoseType> GetDiseaseTypes()
        {
            var list = new List<DiagnoseType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDiseaseType");

                query.Sql = "Select id, name, code, sort " +
                            "from codifiers.sp_disease_vid_tab order by sort";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new DiagnoseType();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    item.Sort = (int)DbResult.GetNumeric(result.Fields[3], -1);
                    list.Add(item);
                }
            }

            return list;
        }

        
        public static DiagnoseType GetDiseaseType(long id)
        {
            var list = new List<DiagnoseType>();

            using (var dbWorker = new DbWorker())
            {

                var result = dbWorker.Connection.Query<DiagnoseType>("Select id, name, code, sort " +
                            "from codifiers.sp_disease_vid_tab where id=@id", new { Id=id }).FirstOrDefault();
                return result;
                
            }

        }



        public static List<MkbSubclass> GetMkbSubClasses()
        {
            var list = new List<MkbSubclass>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMkbSubClasses");

                query.Sql = "Select diagn_kod_mkb_id, diagn_kod_mkb, kodname " +
                            "from public.diagn_kod_mkb_tab order by 1";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new MkbSubclass();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<MkbClass> GetMkbClasses()
        {
            var list = new List<MkbClass>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMkbClasses");

                query.Sql = "Select class_id, num_class, class, kod1, kod2 " +
                            "from public.classmkb_tab order by 1";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new MkbClass();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Number = DbResult.GetString(result.Fields[1], "");
                    item.Name = DbResult.GetString(result.Fields[2], "");
                    item.CodeFrom = DbResult.GetString(result.Fields[3], "");
                    item.CodeTo = DbResult.GetString(result.Fields[4], "");
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<Mkb> GetMkbs()
        {
            var list = new List<Mkb>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMkbClasses");

                query.Sql = "Select mkb_id, mkb, kod_d, reestr_code " +
                            "from public.mkb_tab order by 1";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new Mkb();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    item.ReestrCode = DbResult.GetString(result.Fields[3], "");
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<PaymentType> GetPaymentTypes()
        {
            var list = new List<PaymentType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPaymentTypes");

                query.Sql = "Select id, name, code, is_default, parent_id " +
                            "from codifiers.sp_kind_paid_tab order by id";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new PaymentType();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    item.IsDefault = DbResult.GetNumeric(result.Fields[3], 0) == 1;
                    var parent = result.Fields[4];
                    item.ParentId = parent == null ? null : (long?)DbResult.GetNumeric(parent, 0);
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<ServicePlace> GetServicePlaces()
        {
            var list = new List<ServicePlace>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPaymentTypes");

                query.Sql = "Select id, name, code " +
                            "from codifiers.place_service_tab order by id";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new ServicePlace();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<CureResult> GetCureResults()
        {
            var list = new List<CureResult>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPaymentTypes");

                query.Sql = "Select result_id, result, reestr, patient_state_id, reestr_rslt, reestr_ishod, _end " +
                            "from public.result_tab order by result_id";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new CureResult();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Reestr = (int)DbResult.GetNumeric(result.Fields[2], -1);
                    item.PatientStateId = (int)DbResult.GetNumeric(result.Fields[3], -1);
                    item.Rslt = (int)DbResult.GetNumeric(result.Fields[4], -1);
                    item.Ishod = (int)DbResult.GetNumeric(result.Fields[5], -1);
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<FirstVisitCondition> GetFirstVisitConditions()
        {
            var list = new List<FirstVisitCondition>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetFirstVisitConditions");

                query.Sql = "Select id, name, code " +
                            "from codifiers.first_visit_condition_tab order by id";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new FirstVisitCondition();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<VisitPurpose> GetVisitPurposes()
        {
            var list = new List<VisitPurpose>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetVisitPurposes");

                query.Sql = "Select id, name, code, reestr, idsp, hospital_type_id, is_cure, is_inspection " +
                            "from codifiers.visit_purpose_common_tab order by id";

                var results = dbWorker.GetResults(query);

                foreach (var result in results)
                {
                    var item = new VisitPurpose();
                    item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                    item.Name = DbResult.GetString(result.Fields[1], "");
                    item.Code = DbResult.GetString(result.Fields[2], "");
                    item.Reestr = (int)DbResult.GetNumeric(result.Fields[3], -1);
                    item.Idsp = (int)DbResult.GetNumeric(result.Fields[4], -1);
                    item.LpuTypeId = DbResult.GetNumeric(result.Fields[5], -1);
                    item.IsForCure = DbResult.GetBoolean(result.Fields[6], false);
                    item.IsForInspection = DbResult.GetBoolean(result.Fields[7], false);
                    list.Add(item);
                }
            }

            return list;
        }

        public static string GetLPUName(string code)
        {
            using(var db= new DbWorker())
            {
                var result = db.Connection.ExecuteScalar<String>("select full_name from codifiers.lpu_tab where code=@Code", 
                                new { Code=code });
                return result;
            }
        }

        public static string GetLPUName(long lpuId)
        {
            using (var db = new DbWorker())
            {
                var result = db.Connection.ExecuteScalar<String>("select full_name from codifiers.lpu_tab where id=@Id",
                                new { Id = lpuId });
                return result;
            }
        }

        public static List<SocStatus> GetSocStatuses()
        {
            var list = new List<SocStatus>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetSocStatus");
                query.Sql = "Select " +
                            "s.id, s.code, s.name " +
                            "from codifiers.sozstatus_tab s";
                var results = dbWorker.GetResults(query);

                foreach (var dbResult in results)
                {
                    var id = DbResult.GetLong(dbResult.Fields[0], 0);
                    var code = DbResult.GetString(dbResult.Fields[1], "");
                    var name = DbResult.GetString(dbResult.Fields[2], "");


                    var socStatus = new SocStatus
                    {
                        Id = id,
                        Code = code,
                        Name = name
                    };

                    list.Add(socStatus);
                }
            }

            return list;
        }

        public static List<Region> GetRegions()
        {
            var list = new List<Region>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetRegion");
                query.Sql = @"select id, socr, name, full_name, index, code 
                              from codifiers.sp_kladr_tab
                              where level_ter=@level order by name";
                query.AddParamWithValue("@level", 1);

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var kladr = new Region();
                        kladr.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        kladr.TypeShortCut = DbResult.GetString(dbResult.Fields[1], "");
                        kladr.Name = DbResult.GetString(dbResult.Fields[2], "");
                        kladr.FullName = DbResult.GetString(dbResult.Fields[3], "");
                        kladr.PostIndex = DbResult.GetString(dbResult.Fields[4], "");
                        kladr.Code = DbResult.GetString(dbResult.Fields[5], "");
                        Console.WriteLine("Code " + kladr.Code);
                        list.Add(kladr);
                    }
                }
            }

            return list;
        }

        public static List<SMO> GetRegionSmo(string regionId)
        {
            var list = new List<SMO>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetSmo");
                query.Sql = "Select smo.id, smo.name, kladr.name " +
                            "from codifiers.strcom_tab smo " +
                            "left join codifiers.sp_kladr_tab kladr on smo.region_id = kladr.code " +
                            "where smo.region_id = @id and smo.name not like '*%'" +
                            "order by smo.name";
                query.AddParamWithValue("@id", regionId);

                var results = dbWorker.GetResults(query);

                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var id = DbResult.GetLong(result.Fields[0], 0);

                        var smo = new SMO(id);
                        smo.Name = DbResult.GetString(result.Fields[1], "");
                        smo.RegionId = regionId;
                        smo.RegionName = DbResult.GetString(result.Fields[2], "");

                        list.Add(smo);
                    }
                }
            }
            return list;
        }

        public static List<Kladr> GetKladrPartList(string parentCode, int parentLevel)
        {
            var list = new List<Kladr>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetParts");
                query.Sql = "select id, socr, name, full_name, index, level_ter, code from codifiers.sp_kladr_tab " +
                            "where code like @kladrcode and level_ter > @level order by name";
                query.AddParamWithValue("@kladrcode", string.Format("{0}%", Address.GetLikeLevelCode(parentCode, parentLevel)));
                query.AddParamWithValue("@level", parentLevel);

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        Kladr kladr = null;
                        var level = DbResult.GetShort(dbResult.Fields[5], 0);
                        if (level > 0)
                        {
                            switch (level)
                            {
                                case 1:
                                    kladr = new Region();
                                    break;
                                case 2:
                                    kladr = new Area();
                                    break;
                                case 3:
                                    kladr = new City();
                                    break;
                                case 4:
                                    kladr = new Village();
                                    break;
                            }

                            if (kladr != null)
                            {
                                kladr.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                                kladr.TypeShortCut = DbResult.GetString(dbResult.Fields[1], "");
                                kladr.Name = DbResult.GetString(dbResult.Fields[2], "");
                                kladr.FullName = DbResult.GetString(dbResult.Fields[3], "");
                                kladr.PostIndex = DbResult.GetString(dbResult.Fields[4], "");
                                kladr.Code = DbResult.GetString(dbResult.Fields[6], "");
                                list.Add(kladr);
                            }
                        }
                    }
                }
            }

            return list;
        }

        public static List<Street> GetStreetPartList(string parentCode, int parentLevel)
        {
            var list = new List<Street>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetStreet");
                query.Sql = "select id, socr, name, index, code from codifiers.sp_street_tab " +
                            "where code like @strcode order by name";
                query.AddParamWithValue("@strcode", string.Format("{0}%", Address.GetLikeLevelCode(parentCode, parentLevel)));

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        Street street = new Street();
                        street.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        street.TypeShortCut = DbResult.GetString(dbResult.Fields[1], "");
                        street.Name = DbResult.GetString(dbResult.Fields[2], "");
                        street.PostIndex = DbResult.GetString(dbResult.Fields[3], "");
                        street.Code = DbResult.GetString(dbResult.Fields[4], "");
                        list.Add(street);

                    }
                }
            }

            return list;
        }

        public static List<MedSpeciality> GetMedSpecialities()
        {
            var list = new List<MedSpeciality>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMedSpecialities");
                query.Sql = "select id, code, name from codifiers.doctor_spec_tab order by 3,1;";
                
                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var medSpec = new MedSpeciality();
                        if (medSpec.LoadData(dbResult))
                        {
                            list.Add(medSpec);
                        }
                    }
                }
            }

            return list;
        }

        public static List<MedBenefit> GetBenefits()
        {
            var list = new List<MedBenefit>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetBenefits");
                query.Sql = "select id, name, code, diagn_id, vid_preparat_id from codifiers.lgot_tab;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        MedBenefit item = new MedBenefit();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        item.DiagnRange = DbResult.GetString(dbResult.Fields[3], "");
                        item.MedicamentTypeId = DbResult.GetNumeric(dbResult.Fields[4], -1);
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<MedBenefitCategory> GetBenefitCategories()
        {
            var list = new List<MedBenefitCategory>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetBenefitCategories");
                query.Sql = "select kateg_lgot_id, kateg_lgot from public.kateg_lgot_tab;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        MedBenefitCategory item = new MedBenefitCategory();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<PersonWithFedBenefit> GetPersonsWithFedBenefits()
        {
            var list = new List<PersonWithFedBenefit>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPersonsWithFedBenefits");
                query.Sql = "SELECT pens, ser_pol, num_pol, floga, fam, nam, mid, date_born, dan_id, adr, sex, is_hand, date_end, status " +
                            "FROM public.fed_lgota_tab " +
                            "WHERE dan_id is not null;";

                var results = dbWorker.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var item = new PersonWithFedBenefit();
                        item.Snils = DbResult.GetString(result.Fields[0], "");
                        item.PolicySerial = DbResult.GetString(result.Fields[1], "");
                        item.PolicyNumber = DbResult.GetString(result.Fields[2], "");
                        item.BenefitCode = DbResult.GetString(result.Fields[3], "");
                        item.LastName = DbResult.GetString(result.Fields[4], "");
                        item.FirstName = DbResult.GetString(result.Fields[5], "");
                        item.MiddleName = DbResult.GetString(result.Fields[6], "");
                        item.BirthDate = DbResult.GetDateTime(result.Fields[7], DateTime.MinValue);
                        item.PatientId = DbResult.GetNumeric(result.Fields[8], -1);
                        item.Address = DbResult.GetString(result.Fields[9], "");
                        item.Gender = DbResult.GetString(result.Fields[10], "м").Equals("м") ? Gender.Male : Gender.Female;

                        item.IsManualAdded = DbResult.GetNumeric(result.Fields[11], 0) == 1; // 1 - добавлен вручную

                        item.DateEnd = DbResult.GetNullableDateTime(result.Fields[12]);
                        item.Status = DbResult.GetString(result.Fields[13], "");

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static PersonWithFedBenefit GetPersonWithFedBenefitsById(long patientId)
        {
            PersonWithFedBenefit item = null;
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPersonsWithFedBenefits");
                query.Sql = "SELECT pens, ser_pol, num_pol, floga, fam, nam, mid, date_born, dan_id, adr, sex, is_hand, date_end, status " +
                            "FROM public.fed_lgota_tab " +
                            "WHERE dan_id = @id;";
                query.AddParamWithValue("@id", patientId);

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    item = new PersonWithFedBenefit();
                    item.Snils = DbResult.GetString(result.Fields[0], "");
                    item.PolicySerial = DbResult.GetString(result.Fields[1], "");
                    item.PolicyNumber = DbResult.GetString(result.Fields[2], "");
                    item.BenefitCode = DbResult.GetString(result.Fields[3], "");
                    item.LastName = DbResult.GetString(result.Fields[4], "");
                    item.FirstName = DbResult.GetString(result.Fields[5], "");
                    item.MiddleName = DbResult.GetString(result.Fields[6], "");
                    item.BirthDate = DbResult.GetDateTime(result.Fields[7], DateTime.MinValue);
                    item.PatientId = DbResult.GetNumeric(result.Fields[8], -1);
                    item.Address = DbResult.GetString(result.Fields[9], "");
                    item.Gender = DbResult.GetString(result.Fields[10], "м").Equals("м") ? Gender.Male : Gender.Female;

                    item.IsManualAdded = DbResult.GetNumeric(result.Fields[11], 0) == 1; // 1 - добавлен вручную

                    item.DateEnd = DbResult.GetNullableDateTime(result.Fields[12]);
                    item.Status = DbResult.GetString(result.Fields[13], "");

                }
            }

            return item;
        }

        public static List<Medicament> GetMedicaments()
        {
            var list = new List<Medicament>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMedicaments");
                query.Sql = "SELECT p.id, p.name, p.code, " +
                            "p.sp_trnm_id, tr.name_lat AS nam_lat, tr.name AS nam_rus, " +
                            "p.sp_mnn_id, mnn.name_lat AS mnn_lat, mnn.name AS mnn_rus, " +
                            "p.sp_lf_id, lf.name as dtd, p.name_fct, p.name_cnf, p.doza, p.signa, p.price, p.sp_doza_id, p.d_ls, p.kod_rias " +
                            "FROM codifiers.preparat_tab p " +
                            "LEFT JOIN codifiers.sp_lf_tab lf ON lf.id = p.sp_lf_id " +
                            "LEFT JOIN codifiers.sp_trnm_tab tr ON tr.id = p.sp_trnm_id " +
                            "LEFT JOIN codifiers.sp_mnn_tab mnn ON mnn.id = p.sp_mnn_id;";

                var results = dbWorker.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var item = new Medicament();
                        item.LoadData(result);

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<Medicament> GetDiabetMedicaments()
        {
            var list = new List<Medicament>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDiabetMedicaments");
                query.Sql = "SELECT p.id, p.name, p.code, " +
                            "p.sp_trnm_id, tr.name_lat AS nam_lat, tr.name AS nam_rus, " +
                            "p.sp_mnn_id, mnn.name_lat AS mnn_lat, mnn.name AS mnn_rus, " +
                            "p.sp_lf_id, lf.name as dtd, p.name_fct, p.name_cnf, p.doza, p.signa, p.price, p.sp_doza_id, p.d_ls " +
                            "FROM codifiers.preparat_tab p " +
                            "LEFT JOIN codifiers.sp_lf_tab lf ON lf.id = p.sp_lf_id " +
                            "LEFT JOIN codifiers.sp_trnm_tab tr ON tr.id = p.sp_trnm_id " +
                            "LEFT JOIN codifiers.sp_mnn_tab mnn ON mnn.id = p.sp_mnn_id "+
                            "WHERE kod_rias is not null"
                            ;

                var results = dbWorker.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var item = new Medicament();
                        item.LoadData(result);

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<UnpaidDiagnose> GetUnpaidDiagnoses()
        {
            var list = new List<UnpaidDiagnose>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetUnpaidDiagnoses");
                query.Sql = "SELECT id, code, diagn " +
                            "FROM codifiers.bur_unpaid_diagns";

                var results = dbWorker.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var item = new UnpaidDiagnose();
                        item.Id = DbResult.GetNumeric(result.Fields[0], -1);
                        item.Code = DbResult.GetString(result.Fields[1], "");
                        item.MkbCode = DbResult.GetString(result.Fields[2], "");

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<MO> GetMOs()
        {
            var list = new List<MO>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetMOs");

                query.Sql = "SELECT * " +
                            "FROM codifiers.lpu_tab";

                var results = dbWorker.GetResults(query);
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var item = new MO();
                        item.Id = DbResult.GetNumeric(result.GetByName("id"), -1);
                        item.Code = DbResult.GetString(result.GetByName("code"), "");
                        item.Name = DbResult.GetString(result.GetByName("name"), "");
                        item.FullName = DbResult.GetString(result.GetByName("full_name"), "");
                        item.Address = DbResult.GetString(result.GetByName("address"), "");
                        item.Ogrn = DbResult.GetString(result.GetByName("ogrn"), "");
                        item.Okvd = DbResult.GetString(result.GetByName("okvd"), "");

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static MO GetMO(long id)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id, name, address, ogrn FROM codifiers.lpu_tab where id=@Id";
                var result = db.Connection.Query<MO>(sql, new { Id=id}).FirstOrDefault();
                return result;
            }
        }


        public static List<TraumaType> GetTraumaTypes()
        {
            var list = new List<TraumaType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetTraumaTypes");
                query.Sql = "select id, name, code, bur_reestr_code from codifiers.travma_tab order by name;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new TraumaType();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        item.ReestrCode = DbResult.GetString(dbResult.Fields[3], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<CallReasonType> GetEmergencyCallReasons()
        {
            var list = new List<CallReasonType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetEmergencyCallReasons");
                query.Sql = "select id, name, code from emergency.sp_call_reason_tab order by 1;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new CallReasonType();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<CallResultType> GetEmergencyCallResults()
        {
            var list = new List<CallResultType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetEmergencyCallResults");
                query.Sql = "select id, name, code from emergency.sp_call_result_tab order by order_col;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new CallResultType();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<NoResultReason> GetEmergencyNoResultReasons()
        {
            var list = new List<NoResultReason>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetEmergencyNoResultReasons");
                query.Sql = "select id, name, code from emergency.sp_no_result_reason_tab order by name;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new NoResultReason();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<DispRegistration> GetEmergencyDispRegistrations()
        {
            var list = new List<DispRegistration>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetEmergencyDispRegistrations");
                query.Sql = "select id, name, code from emergency.sp_disp_registration_tab order by name;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new DispRegistration();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<AidResult> GetEmergencyAidResults()
        {
            var list = new List<AidResult>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetEmergencyAidResults");
                query.Sql = "select id, name, code from emergency.sp_aid_result_tab order by 1;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new AidResult();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<PersonDocumentType> GetDocumentTypes()
        {
            var list = new List<PersonDocumentType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDocumentTypes");
                query.Sql = "select vid_doc_id, name, ffoms_code from public.vid_doc_tab order by 1;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new PersonDocumentType();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.FomsCode = DbResult.GetInt(dbResult.Fields[2], -1);
                        list.Add(item);
                    }
                }
            }

            return list;
        }
        public static List<PolicyType> GetPolicyTypes()
        {
            var list = new List<PolicyType>();

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPolicyTypes");
                query.Sql = "select id, name, code from codifiers.policy_type_tab order by 1;";

                var result = dbWorker.GetResults(query);
                if (result != null && result.Count > 0)
                {
                    foreach (var dbResult in result)
                    {
                        var item = new PolicyType();
                        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                        item.Name = DbResult.GetString(dbResult.Fields[1], "");
                        item.Code = DbResult.GetString(dbResult.Fields[2], "");
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<Doctor> GetDoctors()
        {
            var list = new List<Doctor>();
            using (var dbWorker = new DbWorker())
            {
                //var query = new DbQuery("GetDoctors");
                var sql = @"select d.id, 
                                p.fam lastname, 
                                p.nam firstname, 
                                p.mid midname, 
                                d.inner_doctor_code innercode
                              from public.doctor_tab d
                              left join dan_tab p on p.dan_id=d.dan_id
                              where sp_spec_doctor_id <> 900;";
                list = dbWorker.Connection.Query<Doctor>(sql).ToList();

                //var result = dbWorker.GetResults(query);
                //if (result != null && result.Count > 0)
                //{
                //    foreach (var dbResult in result)
                //    {
                //        var item = new Doctor();
                //        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                //        item.LoadData(item.Id);
                //        list.Add(item);
                //    }
                //}
            }
            return list;
        }

        public static List<Doctor> GetAllDoctors()
        {
            var list = new List<Doctor>();
            using (var dbWorker = new DbWorker())
            {
                //var query = new DbQuery("GetDoctors");
                var sql = @"select d.id, p.fam lastname, p.nam firstname, p.mid midname, d.inner_doctor_code innercode
                              from public.doctor_tab d
                              left join dan_tab p on p.dan_id=d.dan_id
                           ";
                list = dbWorker.Connection.Query<Doctor>(sql).ToList();

                //var result = dbWorker.GetResults(query);
                //if (result != null && result.Count > 0)
                //{
                //    foreach (var dbResult in result)
                //    {
                //        var item = new Doctor();
                //        item.Id = DbResult.GetLong(dbResult.Fields[0], -1);
                //        item.LoadData(item.Id);
                //        list.Add(item);
                //    }
                //}
            }
            return list;
        }

        public static IEnumerable<InjectionType> GetInjectionTypes()
        {
            using(var db=new DbWorker())
            {
                var sql = "select id, name  from codifiers.injection_type_tab";
                var result = db.Connection.Query<InjectionType>(sql);
                return result;
            }
        }

        public static IEnumerable<Division> GetDivisions(string lpuCode)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id, name, lpu_id lpuid
                            from codifiers.otdel_tab 
                            where lpu_id=@Lpu
                            order by name";
                var result = db.Connection.Query<Division>(sql, new { Lpu=lpuCode });
                return result;
            }
        }

        public static IEnumerable<MedSpeciality> GetSpecialities()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, name, code 
                            from codifiers.doctor_spec_tab
                            order by name";
                var result = db.Connection.Query<MedSpeciality>(sql);
                return result;
            }
        }

        public static IEnumerable<Position> GetPositions()
        {
            using (var db = new DbWorker())
            {
                var sql = "select id, name from codifiers.doctor_dolg_tab order by name ";
                var result = db.Connection.Query<Position>(sql);
                return result;
            }
        }

        

    }
}
