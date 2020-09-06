using Ks.PayManager.Entities.Bpms;

namespace Ks.PayManager.Core.Interfaces
{
    public interface IComplementRequestService
    {
        OrderObj GetComplementRequest(int orderId, string urlServicio);
    }
}
