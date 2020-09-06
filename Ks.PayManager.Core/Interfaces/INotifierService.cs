using Ks.PayManager.Core.Options;
using Ks.PayManager.Entities.Notify;

namespace Ks.PayManager.Core.Interfaces
{
    public interface INotifierService
    {
        void NotifyClient(Notify notify, ApplicationSettings applicationSettings);
    }
}
