using Ks.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace Ks.Entities.Orders
{
    public class Items
    {
        public string ProdId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UseCemail { get; set; }
    }
}
