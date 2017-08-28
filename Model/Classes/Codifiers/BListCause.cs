using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Причина выдачи больничного листа (prichina_tab)
    /// </summary>
    public class BListCause
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
