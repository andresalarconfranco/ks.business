using System;

namespace Ks.PayManager.Entities.CreditCard
{
    public class ValidateCreditCardResponse : CreditCardBase
    {
        /// <summary>
        /// Validation result
        /// </summary>
        public bool Validation { get; set; }

        /// <summary>
        /// Message validation
        /// </summary>
        public string MessageValidattion { get; set; }
    }
}
