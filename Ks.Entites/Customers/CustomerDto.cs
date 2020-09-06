using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Entities.Customer
{
    public class CustomerDto
    {
        public string CustID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string CreditCardType { get; set; }
        public string CrediCardNumber { get; set; }
        public string Status { get; set; }
        public string IdCategory { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ROL_NCODE { get; set; }

    }
}
