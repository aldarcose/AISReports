using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers.Emergency;

namespace Model.Classes.Emergency
{
    public class CallReason
    {
        public CallReason()
        {
            this.Type = new CallReasonType();
        }
        public long Id { get; set; }
        public CallReasonType Type { get; set; }
        public bool IsOtherReason
        {
            get
            {
                var otherReasonCode = "7"; // другое
                return !string.IsNullOrEmpty(Type.Code) && Type.Code.Equals(otherReasonCode);
            }
        }
        public string Additional { get; set; }

        public override string ToString()
        {
            return this.Type.ToString();
        }
    }
}
