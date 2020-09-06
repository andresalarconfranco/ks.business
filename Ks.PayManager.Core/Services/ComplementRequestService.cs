using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Entities.Bpms;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Ks.PayManager.Core.Services
{
    public class ComplementRequestService : IComplementRequestService
    {
        public OrderObj GetComplementRequest(int orderId, string urlServicio)
        {
            try
            {
                var client = new RestClient(string.Concat(urlServicio, orderId.ToString()))
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                return JsonConvert.DeserializeObject<OrderObj>(response.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
