using Model.Classes.AddressPart;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using System.Text;

namespace Model.Classes
{
    public class Address : IValidatable, ICopyable<Address>
    {
        public Address()
        {
        }
        public Region Region { get; set; }
        public Area Area { get; set; }
        public City City { get; set; }
        public Village Village { get; set; }
        public Street Street { get; set; }
        public int House { get; set; }
        public string Building { get; set; }
        public int Flat { get; set; }
        public string FlatLit { get; set; }

        public string Additional { get; set; }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public void UpdateText()
        {
            var sb = new StringBuilder();
            if (Region != null)
            {
                sb.AppendFormat("{0} {1} ", Region.Name, Region.TypeShortCut);

                if (Area != null)
                {
                    sb.AppendFormat("{0} {1} ", Area.Name, Area.TypeShortCut);
                }

                if (City != null)
                {
                    sb.AppendFormat("{0} {1} ", City.Name, City.TypeShortCut);
                }

                if (Village != null)
                {
                    sb.AppendFormat("{0} {1} ", Village.Name, Village.TypeShortCut);
                }

                if (Street != null)
                {
                    sb.AppendFormat("{0} {1} ", Street.Name, Street.TypeShortCut);
                }

                if (House > 0)
                {
                    sb.AppendFormat("д. {0} ", House);
                }

                if (!string.IsNullOrEmpty(Building))
                {
                    sb.AppendFormat("корп. {0} ", Building);
                }

                if (Flat > 0)
                {
                    sb.AppendFormat("кв. {0} ", Flat);
                }

                if (!string.IsNullOrEmpty(FlatLit))
                {
                    sb.AppendFormat("буква {0} ", FlatLit);
                }

                if (!string.IsNullOrEmpty(Additional))
                {
                    sb.AppendFormat("доп. {0} ", Additional);
                }
            }
            _text = sb.ToString();
        }

        public void LoadAddressByPatientId(long patientId, AddressType type)
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetStreetCode");
                query.Sql = "Select sp_street_code, house_number, building, flat_number, flat_litera, additional_data, street_id from address_tab where dan_id=@id and sp_addr_type_id=@type";
                query.AddParamWithValue("@id", patientId);
                query.AddParamWithValue("@type", (int)type);

                this.Region = new Region();
                this.Area = new Area();
                this.City = new City();
                this.Village = new Village();
                this.Street = new Street();

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    var streetCode = DbResult.GetString(result.Fields[0], "");
                    var streetId = DbResult.GetString(result.Fields[6], "");
                    if (!string.IsNullOrEmpty(streetCode))
                    {
                        this.Region.LoadByCode(GetLevelCode(streetCode, 1));
                        this.Area.LoadByCode(GetLevelCode(streetCode, 2));
                        this.City.LoadByCode(GetLevelCode(streetCode, 3));
                        this.Village.LoadByCode(GetLevelCode(streetCode, 4));
                        this.Street.LoadByCode(streetId);
                    }
                    this.House = DbResult.GetInt(result.Fields[1], -1);
                    this.Building = DbResult.GetString(result.Fields[2], "");
                    this.Flat = DbResult.GetInt(result.Fields[3], -1);
                    this.FlatLit = DbResult.GetString(result.Fields[4], "");
                    this.Additional = DbResult.GetString(result.Fields[5], "");
                }
            }
        }

        public static string GetLevelCode(string code, int level)
        {
            switch (level)
            {
                // Регион
                case 1:
                    int start = 2;
                    int codelen = 13;
                    var fillChar = '0';
                    return string.Format("{0}{1}", code.Substring(0, start), new string(fillChar, codelen - start));
                // Район
                case 2:
                    start = 5;
                    codelen = 13;
                    fillChar = '0';
                    return string.Format("{0}{1}", code.Substring(0, start), new string(fillChar, codelen - start));
                // Город
                case 3:
                    start = 8;
                    codelen = 13;
                    fillChar = '0';
                    return string.Format("{0}{1}", code.Substring(0, start), new string(fillChar, codelen - start));
                    
                // Деревня
                case 4:
                    start = 12;
                    codelen = 13;
                    fillChar = '0';
                    return string.Format("{0}{1}", code.Substring(0, start), new string(fillChar, codelen - start));
                // Улица
                case 5:
                    return code;
            }
            return code;
        }

        public static string GetLikeLevelCode(string code, int level)
        {
            switch (level)
            {
                // Регион
                case 1:
                    int start = 2;
                    if (code.Length < start)
                        return code;
                    return code.Substring(0, start);
                // Район
                case 2:
                    start = 5;
                    if (code.Length < start)
                        return code;
                    return code.Substring(0, start);
                // Город
                case 3:
                    start = 8;
                    if (code.Length < start)
                        return code;
                    return code.Substring(0, start);

                // Деревня
                case 4:
                    start = 12;
                    if (code.Length < start)
                        return code;
                    return code.Substring(0, start);
            }
            return code;
        }

        public bool Validate(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public void Copy(Address original)
        {
            if (original.Region != null)
            {
                if (this.Region == null)
                    this.Region = new Region();

                this.Region.Copy(original.Region);
            }
            else
            {
                this.Region = null;
            }

            if (original.Area != null)
            {
                if (this.Area == null)
                    this.Area = new Area();

                this.Area.Copy(original.Area);
            }
            else
            {
                this.Area = null;
            }

            if (original.City != null)
            {
                if (this.City == null)
                    this.City = new City();

                this.City.Copy(original.City);
            }
            else
            {
                this.City = null;
            }

            if (original.Village != null)
            {
                if (this.Village == null)
                    this.Village = new Village();

                this.Village.Copy(original.Village);
            }
            else
            {
                this.Village = null;
            }

            if (original.Street != null)
            {
                if (this.Street == null)
                    this.Street = new Street();

                this.Street.Copy(original.Street);
            }
            else
            {
                this.Street = null;
            }

            this.House = original.House;
            this.Building = original.Building;
            this.Flat = original.Flat;
            this.FlatLit = original.FlatLit;
            this.Additional = original.Additional;

            this.Text = original.Text;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Text))
                UpdateText();
            return Text;
        }
    }

    public enum AddressType
    {
        Reg = 1,
        Fact = 0
    }
}
