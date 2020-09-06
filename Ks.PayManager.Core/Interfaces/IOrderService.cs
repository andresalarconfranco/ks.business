using Ks.PayManager.Entities.Order;

namespace Ks.PayManager.Core.Interfaces
{
    public interface IOrderService
    {
        UpdateOrderResponse UpdateOrderPaymentStatus(UpdateOrderRequest updateOrderRequest, string urlOrderService);

        OrderBase GetOrder(string idOrder, string urlOrderService);
    }
}
