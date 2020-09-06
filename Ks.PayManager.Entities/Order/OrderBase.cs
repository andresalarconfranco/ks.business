using System;

namespace Ks.PayManager.Entities.Order
{
    public class OrderBase
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int CustId { get; set; }

        /// <summary>
        /// Order Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Order date
        /// </summary>
        public Nullable<DateTime> OrderDate { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public Nullable<decimal> Price { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Order Comments
        /// </summary>
        public string Comments { get; set; }
    }
}
