using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Annotations;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Codifiers
{
    // Участок
    public class MedArea : ILoadData
    {
        public MedArea()
        {
            IsLoading = false;
            IsLoaded = false;
        }

        public MedArea(long id) : this()
        {
            LoadData(id);
        }

        public long Id { get; set; }

        /// <summary>
        /// Название участка
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Код участка
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Проставляется по умолчанию
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Ид доктора, ответ. за участок
        /// </summary>
        public long DoctorId { get; set; }
        /// <summary>
        /// Ид отделения
        /// </summary>
        public long DivisionId { get; set; }
        /// <summary>
        /// Фильтр возраста С
        /// </summary>
        public int AgeFrom { get; set; }
        /// <summary>
        /// Фильтр возраста По
        /// </summary>
        public int AgeTo { get; set; }

        /// <summary>
        /// Тип участка (1 - дополнительный, остальное - основной)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Только для мужчин/женщин/всех
        /// </summary>
        public Gender Gender { get; set; }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }
        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            OnLoading();

            Id = id;
            var loadResult = true;

            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetMedArea");
                q.Sql = "Select * from codifiers.uchactok_tab where id=@id";
                q.AddParamWithValue("@id", Id);
                var result = db.GetResult(q);
                if (result.Fields.Count > 0)
                {
                    var uch_id = result.GetByName("id");
                    var uch_name = result.GetByName("name");
                    var uch_code = result.GetByName("code");
                    var uch_default = result.GetByName("flag_default");
                    var uch_doctor = result.GetByName("doctor_id");
                    var uch_division = result.GetByName("otdel_id");
                    var uch_age_from = result.GetByName("age_from");
                    var uch_age_to = result.GetByName("age_for");
                    var uch_type = result.GetByName("uchactok_type_id");
                    var uch_gender = result.GetByName("sex");

                    Id = DbResult.GetNumeric(uch_id, -1);
                    Name = DbResult.GetString(uch_name, "");
                    Code = DbResult.GetString(uch_code, "");
                    IsDefault = DbResult.GetNumeric(uch_default, 0) == 1;
                    DoctorId = DbResult.GetNumeric(uch_doctor, -1);
                    DivisionId = DbResult.GetNumeric(uch_division, -1);
                    AgeFrom = (int)DbResult.GetNumeric(uch_age_from, 0);
                    AgeTo = (int) DbResult.GetNumeric(uch_age_to, 200);
                    Type = (int)DbResult.GetNumeric(uch_type, 0);
                    if (uch_gender == null)
                    {
                        Gender = Gender.Unknown;
                    }
                    else
                    {
                        Gender = DbResult.GetString(uch_gender, "м").Equals("м") ? Gender.Male : Gender.Female;
                    }
                }
            }

            IsLoaded = loadResult;
            OnLoaded();

            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        public static List<MedArea> GetMedAreasByAddress(Address address)
        {
            // для получения участка требуется улица и номер дома
            if (address.Street == null || string.IsNullOrEmpty(address.Street.Code))
                return null;
            if (address.House < 1)
                return null;

            var streetCode = address.Street.Code;
            var houseNum = address.House;
            // флаг - номер дома четный
            var isHouseNumEven = houseNum % 2 == 0;

            // флаг - указан корпус
            var hasBuilding = !string.IsNullOrEmpty(address.Building);

            var list = new List<MedArea>();

            using (var db = new DbWorker())
            {
                var domQuery = isHouseNumEven
                    ? "(@house BETWEEN m.even_from AND m.even_for)"
                    : "(@house BETWEEN m.odd_from AND m.odd_for)";

                var q = new DbQuery("GetMedAreaByAddress");
                q.Sql = "select u.* from codifiers.uchactok_tab u " +
                        "left join public.mapping_uchactok_tab m on m.uchactok_id = u.id " +
                        "where m.street_id=@street_id and " + domQuery +";";
                q.AddParamWithValue("@house", houseNum);
                q.AddParamWithValue("@street_id", streetCode);

                var results = db.GetResults(q);

                foreach (var result in results)
                {
                    var uch_id = result.GetByName("id");
                    var uch_name = result.GetByName("name");
                    var uch_code = result.GetByName("code");
                    var uch_default = result.GetByName("flag_default");
                    var uch_doctor = result.GetByName("doctor_id");
                    var uch_division = result.GetByName("otdel_id");
                    var uch_age_from = result.GetByName("age_from");
                    var uch_age_to = result.GetByName("age_for");
                    var uch_type = result.GetByName("uchactok_type_id");
                    var uch_gender = result.GetByName("sex");

                    var uchastok = new MedArea
                    {
                        Id = DbResult.GetNumeric(uch_id, -1),
                        Name = DbResult.GetString(uch_name, ""),
                        Code = DbResult.GetString(uch_code, ""),
                        IsDefault = DbResult.GetNumeric(uch_default, 0) == 1,
                        DoctorId = DbResult.GetNumeric(uch_doctor, -1),
                        DivisionId = DbResult.GetNumeric(uch_division, -1),
                        AgeFrom = (int)DbResult.GetNumeric(uch_age_from, 0),
                        AgeTo = (int) DbResult.GetNumeric(uch_age_to, 200),
                        Type = (int)DbResult.GetNumeric(uch_type, 0),
                        Gender = (uch_gender == null) 
                                ? Gender.Unknown 
                                : (DbResult.GetString(uch_gender, "м").Equals("м") 
                                    ? Gender.Male 
                                    : Gender.Female)
                    };

                    list.Add(uchastok);
                }

            }

            return list;
        }
    }
}
