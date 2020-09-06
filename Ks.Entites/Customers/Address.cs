using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.Entities.Customers
{
    public class Address
    {
        public decimal AddrId { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressType { get; set; }
        public string Address1 { get; set; }
        public string Neighborhood { get; set; }
    }
}
