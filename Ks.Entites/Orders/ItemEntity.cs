using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.Entities.Orders
{
    public class ItemEntity
    {
        public int ItemId { get; set; }
        public string ProdId { get; set; }
        public string ProductName { get; set; }
        public string PartNum { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public long OrdId { get; set; }
    }
}
