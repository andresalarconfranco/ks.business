using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.Entities.Orders
{
    public class Requests
    {
        public decimal IdRequest { get; set; }
        public string OrdId { get; set; }
        public string Supplier { get; set; }
        public decimal TotalValue { get; set; }
        public string Status { get; set; }
    }
}
