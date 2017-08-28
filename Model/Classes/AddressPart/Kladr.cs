using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.AddressPart
{
    public abstract class Kladr : ICopyable<Kladr>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TypeShortCut { get; set; }
        public string FullName { get; set; }

        public int Level { get; protected set; } //уровень террирорий РФ (1 - регион, 2 - район, 3 - город, 4 - пункт)
        public string PostIndex { get; set; }

        public virtual void LoadByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return;

            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetRegion");
                query.Sql = "select id, socr, name, full_name, index from codifiers.sp_kladr_tab " +
                            "where code = @kladrcode and level_ter=@level";
                query.AddParamWithValue("@kladrcode", code);
                query.AddParamWithValue("@level", Level);

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    this.Id = DbResult.GetLong(result.Fields[0], -1);
                    this.TypeShortCut = DbResult.GetString(result.Fields[1], "");
                    this.Name = DbResult.GetString(result.Fields[2], "");
                    this.FullName = DbResult.GetString(result.Fields[3], "");
                    this.Code = code;
                    this.PostIndex = DbResult.GetString(result.Fields[4], "");
                }
            }
        }

        public virtual void LoadByName(string name)
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetRegion");
                query.Sql = "select id, socr, full_name, code, index from codifiers.sp_kladr_tab " +
                            "where Lower(name) = @name and level_ter=@level";
                query.AddParamWithValue("@name", name.ToLower());
                query.AddParamWithValue("@level", Level);

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    this.Id = DbResult.GetLong(result.Fields[0], -1);
                    this.TypeShortCut = DbResult.GetString(result.Fields[1], "");
                    this.FullName = DbResult.GetString(result.Fields[2], "");
                    this.Code = DbResult.GetString(result.Fields[3], "");
                    this.PostIndex = DbResult.GetString(result.Fields[4], "");
                    this.Name = name;
                }
            }
        }
         
        public void Copy(Kladr original)
        {
            this.Id = original.Id;
            this.Code = original.Code;
            this.Name = original.Name;
            this.FullName = original.FullName;
            this.TypeShortCut = original.TypeShortCut;
            this.Level = original.Level;
            this.PostIndex = original.PostIndex;
        }

    }
}
