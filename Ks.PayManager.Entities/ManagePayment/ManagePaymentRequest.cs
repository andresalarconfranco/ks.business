using Ks.PayManager.Entities.CreditCard;
using Ks.PayManager.Entities.Order;
using Ks.PayManager.Entities.Pay;

namespace Ks.PayManager.Entities.ManagePayment
{
    public class ManagePaymentRequest
    {
        /// <summary>
        /// Validate credit card request
        /// </summary>
        public ValidateCreditCardRequest PropValidateCreditCardRequest { get; set; }

        /// <summary>
        /// Pay request
        /// </summary>
        public PayRequest PropPayRequest { get; set; }

        /// <summary>
        /// Update order request
        /// </summary>
        public UpdateOrderRequest PropUpdateOrderRequest { get; set; }
    }
}
