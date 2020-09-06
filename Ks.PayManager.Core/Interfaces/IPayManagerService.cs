using Ks.PayManager.Core.Options;
using Ks.PayManager.Entities.ManagePayment;
using System.Threading.Tasks;

namespace Ks.PayManager.Core.Interfaces
{
    public interface IPayManagerService
    {
        ManagePaymentResponse ManagePayment(ManagePaymentRequest managePaymentRequest, ApplicationSettings applicationSettings);
    }
}
