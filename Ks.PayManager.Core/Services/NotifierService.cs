using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Core.Options;
using Ks.PayManager.Entities.Notify;
using RestSharp;

namespace Ks.PayManager.Core.Services
{
    public class NotifierService : INotifierService
    {
        public void NotifyClient(Notify notify, ApplicationSettings applicationSettings)
        {
			try
			{
                var client = new RestClient(string.Concat(applicationSettings.Notifier, "from=", applicationSettings.UserFrom, "&to=", notify.Email,
                    "&subject=", notify.Subject, "&message=", notify.Message, "&password=", applicationSettings.PwsEmail, "&port=", applicationSettings.EmailPort))
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
            }
			catch (System.Exception)
			{

			}
        }
    }
}
