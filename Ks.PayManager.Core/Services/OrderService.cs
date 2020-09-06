using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Core.Options;
using Ks.PayManager.Entities.Order;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Ks.PayManager.Core.Services
{
    public class OrderService : IOrderService
    {
        public UpdateOrderResponse UpdateOrderPaymentStatus(UpdateOrderRequest updateOrderRequest, string urlOrderService)
        {
            try
            {
                var client = new RestClient(string.Concat(urlOrderService, "UpdateStatus"))
                {
                    Timeout = -1
                };
                OrderBase orderBase = new OrderBase()
                {
                    OrderId = updateOrderRequest.OrderId,
                    Status = updateOrderRequest.Status
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\n\t\"OrderId\":" + updateOrderRequest.OrderId.ToString() + ",\n\t\"Status\":\"" + updateOrderRequest.Status + "\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UpdateOrderResponse()
                    {
                        OperationMessage = "OK",
                        Comments = updateOrderRequest.Comments,
                        OrderId = updateOrderRequest.OrderId,
                        CustId = updateOrderRequest.CustId,
                        Status = updateOrderRequest.Status,
                        Price = updateOrderRequest.Price,
                        OrderDate = updateOrderRequest.OrderDate
                    };
                }
                else
                {
                    return new UpdateOrderResponse()
                    {
                        OperationMessage = "La orden no se actualizo correctamente.",
                        Comments = updateOrderRequest.Comments,
                        OrderId = updateOrderRequest.OrderId,
                        CustId = updateOrderRequest.CustId,
                        Status = updateOrderRequest.Status,
                        Price = updateOrderRequest.Price,
                        OrderDate = updateOrderRequest.OrderDate
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateOrderResponse()
                {
                    Comments = updateOrderRequest.Comments,
                    OrderId = updateOrderRequest.OrderId,
                    CustId = updateOrderRequest.CustId,
                    OperationMessage = ex.Message,
                    OrderDate = updateOrderRequest.OrderDate,
                    Price = updateOrderRequest.Price,
                    Status = updateOrderRequest.Status
                };
            }
        }

        public OrderBase GetOrder(string idOrder, string urlOrderService)
        {
            var client = new RestClient($"{urlOrderService}api/Orders/{idOrder}")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<OrderBase>(response.Content);
        }
    }
}
