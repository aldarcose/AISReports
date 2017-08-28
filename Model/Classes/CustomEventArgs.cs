using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes
{
    public class CancelableEventArgs:EventArgs
    {
        public CancelableEventArgs()
        {
            Cancel = false;
        }
        public bool Cancel { get; set; }
        public string CancelReason { get; set; }
    }
}
