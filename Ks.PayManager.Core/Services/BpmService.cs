using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Entities.Bpms;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Ks.PayManager.Core.Services
{
    public class BpmService : IBpmService
    {
		public BonitaResponse CreateProcessInstanceBonita(ProcesoDetalle process, string urlService)
		{
			try
			{
				var client = new RestClient(urlService)
				{
					Timeout = -1
				};
				var request = new RestRequest(Method.POST);
				request.AddHeader("pws", process.Pws);
				request.AddHeader("username", process.UserName);
				request.AddHeader("Content-Type", "application/json");
				request.AddJsonBody(process);
				IRestResponse response = client.Execute(request);
				Console.WriteLine(response.Content);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return new BonitaResponse()
					{
						InvocacionCorrecta = true,
						Mensaje = "Se instancia de forma correcta el servicio en BPMS"
					};
				}
				else
				{
					return new BonitaResponse()
					{
						InvocacionCorrecta = false,
						Mensaje = "No se invoco el BPMS"
					};
				}
			}
			catch (Exception ex)
			{
				return new BonitaResponse()
				{
					InvocacionCorrecta = false,
					Mensaje = ex.Message
				};
			}
		}
	}
}
