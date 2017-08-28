using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.Registry
{
    public class Cabinet
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? BuildingId { get; set; }
        public string Number { get; set; }

        public string Fullname
        {
            get {
                return ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Number, Name);
        }
    }
}
