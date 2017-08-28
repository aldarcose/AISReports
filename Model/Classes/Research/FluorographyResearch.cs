using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Research
{
    public class FluorographyResearch : BaseResearch
    {
        public FluorographyResearch()
        {
            Name = "Флюорография";
        }

        /// <summary>
        /// Внутренний номер анализа
        /// </summary>
        public long Num { get; set; }
    }
}
