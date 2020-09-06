using Ks.PayManager.Entities.Pay;

namespace Ks.PayManager.Core.Interfaces
{
    public interface IPayService
    {
        PayResponse MakePayment(PayRequest payRequest);
    }
}
