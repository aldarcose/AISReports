using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Documents
{
    public class PolicyType : BaseDocumentType, ILoadData
    {
        public string Code { get; set; }
        public override string GetSerialRegexMask()
        {
            var mask = string.Empty;
            switch (Code)
            {
                // ОМС старого образца
                // опеределить маску
                case "1":
                    mask = @"\w*";
                    break;
                // Временное свидетельство
                // Нет серии
                case "2": break;
                // ОМС единого образца
                // Нет серии
                case "3": break;
                default:
                    break;
            }

            return mask; 
        }

        public override string GetNumberRegexMask()
        {
            var mask = string.Empty;
            switch (Code)
            {
                // ОМС старого образца
                case "1":
                    mask = DocumentMaskTemplate.GetRegexTemplate("000000"); // 6 цифр
                    break;
                // Временное свидетельство
                case "2":
                    mask = DocumentMaskTemplate.GetRegexTemplate("000000000"); // 9 цифр
                    break;
                // ОМС единого образца
                case "3":
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999999999999999");
                    break;
                default:
                    break;
            }

            return mask;
        }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            IsLoaded = false;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            IsLoaded = true;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            Id = id;
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetPolicyType");
                query.Sql = "select id, name, code from codifiers.policy_type_tab where id=@id;";
                query.AddParamWithValue("@id", id);

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    this.Name = DbResult.GetString(result.Fields[1], "");
                    this.Code = DbResult.GetString(result.Fields[2], "");
                }
            }
            return true;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }
    }
}
