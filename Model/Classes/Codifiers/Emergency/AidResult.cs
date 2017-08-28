using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers.Emergency
{
    public class AidResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public int GetIshodCode()
        {
            if (Id != 4)
            {
                return 302 + (int)Id;
            }

            return 0;
        }
    }
}
