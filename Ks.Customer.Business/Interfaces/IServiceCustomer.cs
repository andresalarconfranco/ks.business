using Ks.Entities.Authentication;
using Ks.Entities.Customer;
using Ks.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Customer.Business.Interfaces
{
    public interface IServiceCustomer
    {
        public string Register(CustomerRegister ClientIn);
        public bool SingSingIn(LoginUser LoginIn);
        public bool ChangeCategory(string CustID, string IdCategory);
        public CustomerInfo GetCustomerInfo(InfoCustomer value);
    }
}
