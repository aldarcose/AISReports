using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Тип оплаты (codifiers.sp_kind_paid_tab) (ОМС, бюджет, платные услуги, военкомат и т.п.)
    /// </summary>
    public class PaymentType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public bool IsDefault { get; set; }

        public long? ParentId { get; set; }
    }
}
