using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Model.Classes.DopDisp
{
    [Table("codifiers.dop_disp_type_tab")]
    public class DopDispType
    {
        [Column("id")]
        public long? Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        
    }


}
