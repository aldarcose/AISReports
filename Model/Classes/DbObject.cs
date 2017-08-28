using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes
{
    public interface DbObject
    {
        long? Id { get; set; }
    }
}
