using Ks.PayManager.Entities.Bpms;
using System.Threading.Tasks;

namespace Ks.PayManager.Core.Interfaces
{
    public interface IBpmService
    {
        BonitaResponse CreateProcessInstanceBonita(ProcesoDetalle process, string urlService);
    }
}
