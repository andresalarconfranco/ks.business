using Ks.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Orders.Business.Interfaces
{
    public interface IServiceOrders
    {
        public long AddItem(Items item);
        public OrderInfo GetOrderInfo(int OrderId);
        public IList<Order> GetIdOrders(string statusOrder, string CustID);
        public IList<OrderInfo> GetOrdersInfo(string Custid = null, string status = null);

    }
}
