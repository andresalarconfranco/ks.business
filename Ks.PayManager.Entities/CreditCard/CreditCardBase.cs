using System;

namespace Ks.PayManager.Entities.CreditCard
{
    public class CreditCardBase
    {
        /// <summary>
        /// Credit Card Number
        /// </summary>
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Expired date specified
        /// </summary>
        public bool ExpiredDateSpecified { get; set; }

        /// <summary>
        /// Security Code
        /// </summary>
        public string SecurityCode { get; set; }

        /// <summary>
        /// Expired Date
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Owner Name
        /// </summary>
        public string OwnerName { get; set; }
    }
}
