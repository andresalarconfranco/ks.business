using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Customer.Business.Connections
{
    public class ConnectionsApi<EntityIn,EntityOut>
    {
        public EntityOut GetInfo(string url, EntityIn objeto, object[] parameters)
        {
            var clientSecurity = new RestClient(url);

            clientSecurity.Timeout = -1;
            var requestSecurity = new RestRequest(Method.POST);
            requestSecurity.AddHeader("Content-Type", "application/json");
            requestSecurity.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(objeto), ParameterType.RequestBody);
            IRestResponse responseSecurity = clientSecurity.Execute(requestSecurity);
            var responseBody_ = JsonConvert.DeserializeObject<EntityOut>(responseSecurity.Content.ToString());

            return responseBody_;
        }
    }
}
