using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interface
{
    public interface IPrintable
    {
        string[] PrintableTypes();
        void Print(string type, object []args);
    }
}
