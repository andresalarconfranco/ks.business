using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.Entities.Orders
{
    public class Order
    {
        public string CustId { get; set; }
        public Int64 OrdId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public Nullable<Int64> IdInvoince { get; set; }
    }
}
