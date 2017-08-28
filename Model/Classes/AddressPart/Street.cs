using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.AddressPart
{
    public class Street : ICopyable<Street>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TypeShortCut { get; set; }
        public string PostIndex { get; set; }

        public void LoadByCode(string code)
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetRegion");
                query.Sql = "select id, socr, name, index from codifiers.sp_street_tab " +
                            "where code = @strcode;";
                query.AddParamWithValue("@strcode", code);

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    this.Id = DbResult.GetLong(result.Fields[0], -1);
                    this.TypeShortCut = DbResult.GetString(result.Fields[1], "");
                    this.Name = DbResult.GetString(result.Fields[2], "");
                    this.PostIndex = DbResult.GetString(result.Fields[3], "");
                    this.Code = code;
                }
            }
        }

        public void LoadByName(string name)
        {
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetRegion");
                query.Sql = "select id, socr, code, index from codifiers.sp_street_tab " +
                            "where Lower(name) = @name";
                query.AddParamWithValue("@name", name.ToLower());

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    this.Id = DbResult.GetLong(result.Fields[0], -1);
                    this.TypeShortCut = DbResult.GetString(result.Fields[1], "");
                    this.Code = DbResult.GetString(result.Fields[2], "");
                    this.PostIndex = DbResult.GetString(result.Fields[3], "");
                }
            }
        }

        public void Copy(Street original)
        {
            this.Name = original.Name;
            this.TypeShortCut = original.TypeShortCut;
            this.Code = original.Code;
            this.Id = original.Id;
            this.PostIndex = original.PostIndex;
        }
    }
}
