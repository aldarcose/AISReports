using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;

namespace Model.Classes.Documents
{
    public abstract class BaseDocumentType : IDocumentType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public abstract string GetSerialRegexMask();

        public abstract string GetNumberRegexMask();
    }
}
