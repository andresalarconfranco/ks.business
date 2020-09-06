using Ks.Entities.Customer;
using Ks.Entities.Customers;
using Ks.Entities.Orders;
using Ks.Entities.Products;
using Ks.Orders.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Orders.Business.Implementation
{
    public class ServiceOrders: IServiceOrders
    {
        public IConfiguration Configuration { get; }

        public ServiceOrders(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public long AddItem(Items item)
        {

            //var aa = Configuration.GetValue("Logging.LogLevel", "Default");
            //Verificar producto
            //34566
            
            var clientProduct = new RestClient(Configuration["UrlApis:GetProduct"] + item.ProdId +"?"+ item.ProdId);

            clientProduct.Timeout = -1;
            var requestProduct = new RestRequest(Method.GET);
            requestProduct.AddHeader("Content-Type", "application/json");
            IRestResponse responseProduct = clientProduct.Execute(requestProduct);
            var responseBodyProduct = JsonConvert.DeserializeObject<Product>(responseProduct.Content.ToString());
            
            if(responseBodyProduct == null)
            {
                return 0;// "No existe el producto";
            }

            string IdCust;
            try
            {
                var client = new RestClient(Configuration["UrlApis:GetCustomerByEmail"] + item.UseCemail);

                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                var response_ = JsonConvert.DeserializeObject<List<CustomerDto>>(response.Content.ToString());

                if (response_.Count() == 0)
                {
                    return 0;// "No existe el Cliente";
                }

                IdCust = response_[0].CustID.Trim();
                Console.WriteLine(response.Content);

                //
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                return 0;// "false";
            }

            Order orderResult = GetIdOrders("Inicial", IdCust)[0];

            if(orderResult.OrdId == 0)
            {
                Order RegOrder = new Order();
                RegOrder.CustId = IdCust;
                RegOrder.OrdId = 0;
                RegOrder.OrderDate = DateTime.Now;
                RegOrder.Price = 0;
                RegOrder.Status = "Inicial";
                RegOrder.Comments = "";
                RegOrder.IdInvoince = null;

                var clientOrder = new RestClient(Configuration["UrlApis:RegisterOrder"]);
                clientOrder.Timeout = -1;
                var requestOrder = new RestRequest(Method.POST);
                requestOrder.AddHeader("Content-Type", "application/json");
                requestOrder.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(RegOrder), ParameterType.RequestBody);
                IRestResponse responseOrder = clientOrder.Execute(requestOrder);
                Console.WriteLine(responseOrder.Content);
                var responseBodyOrder = JsonConvert.DeserializeObject<Order>(responseOrder.Content.ToString());

                orderResult = responseBodyOrder;

            }
            ItemEntity RegItem = new ItemEntity();

            RegItem.ItemId = 0;
            RegItem.ProdId = item.ProdId;
            RegItem.ProductName = item.ProductName;
            RegItem.PartNum = item.ProdId;
            RegItem.Price = item.Price;
            RegItem.Quantity = item.Quantity;
            RegItem.OrdId = orderResult.OrdId;

            var clientItem = new RestClient(Configuration["UrlApis:RegisterItem"]);
            clientItem.Timeout = -1;
            var requestItem = new RestRequest(Method.POST);
            requestItem.AddHeader("Content-Type", "application/json");
            requestItem.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(RegItem), ParameterType.RequestBody);
            IRestResponse responseItem = clientItem.Execute(requestItem);
            Console.WriteLine(responseItem.Content);

            return orderResult.OrdId;
        }

        public OrderInfo GetOrderInfo(int OrderId)
        {

            var clientOrders = new RestClient(Configuration["UrlApis:GetOrder"] + OrderId.ToString());

            clientOrders.Timeout = -1;
            var requestOrders = new RestRequest(Method.GET);
            requestOrders.AddHeader("Content-Type", "application/json");
            IRestResponse responseOrders = clientOrders.Execute(requestOrders);
            var responseBodyOrders = JsonConvert.DeserializeObject<Order>(responseOrders.Content.ToString());

            OrderInfo orderInfo = new OrderInfo();

            
            if (responseBodyOrders == null)
            {
                orderInfo.OrderId = "0";
                return orderInfo;
            }


            var client = new RestClient(Configuration["UrlApis:GetInfoCustomerByIdCustorEmail"] + responseBodyOrders.CustId.Trim() + "&email=");

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var response_ = JsonConvert.DeserializeObject<CustomerInfo>(response.Content.ToString());

            if (response_ == null)
            {
                orderInfo.OrderId = "0";
                return orderInfo;
            }


            var clientItems = new RestClient(Configuration["UrlApis:GetItemsByOrderId"] + responseBodyOrders.OrdId.ToString().Trim());

            clientItems.Timeout = -1;
            var requestItems = new RestRequest(Method.GET);
            IRestResponse responseItems = clientItems.Execute(requestItems);
            var responseItems_ = JsonConvert.DeserializeObject<List<ItemEntity>>(responseItems.Content.ToString());

            if (responseItems_.Count() == 0)
            {
                orderInfo.OrderId = "0";
                return orderInfo;
            }

            var clientRequests = new RestClient(Configuration["UrlApis:GetRequestByOrder"] + responseBodyOrders.OrdId.ToString().Trim());

            clientRequests.Timeout = -1;
            var requestRequests = new RestRequest(Method.GET);
            IRestResponse responseRequests = clientRequests.Execute(requestRequests);
            var responseRequests_ = JsonConvert.DeserializeObject<List<Requests>>(responseRequests.Content.ToString());

            orderInfo.ProveedorMercancia = "";

            if (responseRequests_.Count > 0)
            {
                orderInfo.ProveedorMercancia = responseRequests_[0].Supplier;
            }

            orderInfo.TipoCliente = response_.Category;
            orderInfo.MontoOrden = responseBodyOrders.Price;
            orderInfo.Correo = response_.EMail.ToString().Trim();
            orderInfo.Destino = response_.Country.ToUpper().Equals("COLOMBIA") ? "Nacional" : "InterNacional";
            orderInfo.OrderId = OrderId.ToString();
            orderInfo.NombreCliente = response_.FName.ToString().Trim();
            orderInfo.ApellidoCliente = response_.LName.ToString().Trim();
            orderInfo.Pais = response_.Country.ToString().Trim();
            orderInfo.Departamento = response_.State.ToString().Trim();
            orderInfo.Ciudad = response_.City.ToString().Trim();
            orderInfo.Barrio = response_.Neighborhood.ToString().Trim();
            orderInfo.Direccion = response_.Address1.ToString().Trim();
            orderInfo.CodigoCiudad = response_.City.Length.ToString();
            orderInfo.EstadoOrden = responseBodyOrders.Status.ToString().Trim();

            List<ItemsInfo> ListaItems = new List<ItemsInfo>();

            ItemsInfo itemInfo = new ItemsInfo();

            foreach (ItemEntity x in responseItems_)
            {
                itemInfo = new ItemsInfo();

                itemInfo.ItemId = x.ItemId;
                itemInfo.IdProducto = x.ProdId.ToString().Trim();
                itemInfo.NombreProducto = x.ProductName.ToString().Trim();
                itemInfo.NumeroParte = x.PartNum.ToString().Trim();
                itemInfo.Precio = x.Price;
                itemInfo.Cantidad = int.Parse(x.Quantity.ToString().Replace(".0", "").Replace(",0", ""));
                ListaItems.Add(itemInfo);
            }


            orderInfo.Productos = ListaItems;

            return orderInfo;
        }
        public IList<OrderInfo> GetOrdersInfo(string Custid = null, string status = null)
        {
            List<OrderInfo> orders = new List<OrderInfo>();

            foreach(Order item in GetIdOrders(status, Custid))
            {
                orders.Add(GetOrderInfo(int.Parse(item.OrdId.ToString())));
            }

            return orders;
        }


        public IList<Order> GetIdOrders(string statusOrder=null, string CustID=null)
        {
            var clientOrders = new RestClient(Configuration["UrlApis:StatusOrderByCustIdOrIdOrder"] + CustID + "&status=" + statusOrder);

            clientOrders.Timeout = -1;
            var requestOrders = new RestRequest(Method.GET);
            requestOrders.AddHeader("Content-Type", "application/json");
            IRestResponse responseOrders = clientOrders.Execute(requestOrders);
            var responseBodyOrders = JsonConvert.DeserializeObject<List<Order>>(responseOrders.Content.ToString());

            if (responseBodyOrders.Count() == 0)
            {
                Order orden = new Order();
                orden.OrdId = 0;
                List<Order> lOrder = new List<Order>();
                lOrder.Add(orden);
                return lOrder;
            }

            return responseBodyOrders;
        }
    }
}
