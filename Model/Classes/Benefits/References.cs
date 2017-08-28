using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.Benefits
{
    public class Benefit
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class RemovalReason
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class EntryReason
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class BenefitDocument
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }

    public class LKKEpicris
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }

}
