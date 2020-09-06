using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Ks.Entities.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ks.Orders.Business.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public OrdersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET: api/Orders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        // POST: api/Orders/AddItem
        [HttpPost]
        [Route("AddItem")]
        public long AddItem([FromBody] Items item)
        {
            Implementation.ServiceOrders service = new Implementation.ServiceOrders(Configuration);
            return service.AddItem(item);
        }
        // POST: api/Orders/RemoveItem
        [HttpPost]
        [Route("RemoveItem")]
        public string RemoveItem([FromBody] Items item)
        {
            Implementation.ServiceOrders service = new Implementation.ServiceOrders(Configuration);
            return "Item Removido";

        }
        [HttpGet]
        [Route("GetOrderInfo/{OrderId}")]
        public OrderInfo GetOrderInfo(int OrderId)
        {
            Implementation.ServiceOrders service = new Implementation.ServiceOrders(Configuration);
            return service.GetOrderInfo(OrderId);
        }
        [HttpGet]
        [Route("GetOrdersInfo")]
        public IList<OrderInfo> GetOrdersInfo(string Custid = null, string status = null)
        {
            Implementation.ServiceOrders service = new Implementation.ServiceOrders(Configuration);
            return service.GetOrdersInfo(Custid, status);
        }
    }
}
