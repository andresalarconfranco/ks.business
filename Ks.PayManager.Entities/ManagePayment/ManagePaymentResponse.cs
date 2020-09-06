using Ks.PayManager.Entities.Bpms;
using Ks.PayManager.Entities.CreditCard;
using Ks.PayManager.Entities.Order;
using Ks.PayManager.Entities.Pay;

namespace Ks.PayManager.Entities.ManagePayment
{
    public class ManagePaymentResponse
    {
        /// <summary>
        /// Validate credit card response
        /// </summary>
        public ValidateCreditCardResponse PropValidateCreditCardResponse { get; set; }

        /// <summary>
        /// Pay response
        /// </summary>
        public PayResponse PropPayResponse { get; set; }

        /// <summary>
        /// Update order response
        /// </summary>
        public UpdateOrderResponse PropUpdateOrderResponse { get; set; }

        /// <summary>
        /// Initialize Bonita Process
        /// </summary>
        public BonitaResponse PropBonitaResponse { get; set; }
    }
}
