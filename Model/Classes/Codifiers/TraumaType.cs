using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;

namespace Model.Classes.Codifiers
{
    public class TraumaType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ReestrCode { get; set; }
    }
}
