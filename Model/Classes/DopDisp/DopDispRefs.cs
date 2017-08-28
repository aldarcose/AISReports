using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using SharedDbWorker;

namespace Model.Classes.DopDisp
{
    [Table("dop_disp_type_tab", Schema = "codifiers")]
    public class DDType
    {
        [Column("id")]
        public long? Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }

    [Table("sp_health_group_tab")]
    public class HealthGroup
    {
        [Column("sp_health_group_id")]
        public long? Id { get; set; }
        [Column("sp_health_group")]
        public string Name { get; set; }
    }

    [Table("sp_health_group_tab")]
    public class DispGroup
    {
        [Column("sp_health_group_id")]
        public long? Id { get; set; }
        [Column("sp_health_group")]
        public string Name { get; set; }
    }


    [Table("codifiers.med_help_result_tab")]
    public class DispResult
    {
        [Column("id")]
        public long? Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }

    public class DDStageResult
    {
        public long? Id { get;set; }
        public string Name { get; set; }
    }

    


}
