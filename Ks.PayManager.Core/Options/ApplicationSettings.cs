using System.Reflection.Metadata.Ecma335;

namespace Ks.PayManager.Core.Options
{
    public class ApplicationSettings
    {
        /// <summary>
        /// End point Orders
        /// </summary>
        public string OrderServiceUrl { get; set; }

        /// <summary>
        /// End Point BPM
        /// </summary>
        public string Bonita { get; set; }

        /// <summary>
        /// End Point ComplementOrders
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// Notifier End Point
        /// </summary>
        public string Notifier { get; set; }

        /// <summary>
        /// Email user account
        /// </summary>
        public string UserFrom { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string PwsEmail { get; set; }

        /// <summary>
        /// Email Port
        /// </summary>
        public string EmailPort { get; set; }

        /// <summary>
        /// Usuario Bpm
        /// </summary>
        public string BpmUser { get; set; }

        /// <summary>
        /// Passwor Bpm
        /// </summary>
        public string BpmPws { get; set; }

        /// <summary>
        /// BpmProcessName
        /// </summary>
        public string BpmProcessName { get; set; }

        /// <summary>
        /// ValidateCreditCard
        /// </summary>
        public string ValidateCreditCard { get; set; }
    }
}
