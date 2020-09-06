using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ks.Customer.Business.Implementation;
using Ks.Entities.Customer;
using Ks.Entities.Authentication;
using Ks.Entities.Customers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Ks.Customer.Business.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBusinessController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public CustomerBusinessController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET: api/CustomerBusiness
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CustomerBusiness/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CustomerBusiness/Register
        [HttpPost]
        [Route("Register")]
        public string PostRegister([FromBody] CustomerRegister value)
        {
            ServiceCustomer cliente = new ServiceCustomer(Configuration);

            return cliente.Register(value);
        }
        // POST: api/CustomerBusiness/SignIn
        [HttpPost]
        [Route("SignIn")]
        public bool PostSingIn([FromBody] LoginUser value)
        {
            ServiceCustomer cliente = new ServiceCustomer(Configuration);
            return cliente.SingSingIn(value);
            //return true;
        }

        // PUT: api/CustomerBusiness/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Route("GetInfoCustomer")]
        public CustomerInfo GetCustomerInfo(string CustID, string email)
        {
            ServiceCustomer cliente = new ServiceCustomer(Configuration);
            InfoCustomer value = new InfoCustomer();
            value.CustID = CustID;
            value.email = email;
            return cliente.GetCustomerInfo(value);
        }
    }
}
