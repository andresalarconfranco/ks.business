namespace Ks.PayManager.Entities.Pay
{
    public class PayResponse : PayBase
    {
        /// <summary>
        /// Message pay
        /// </summary>
        public string PayMessage { get; set; }

        /// <summary>
        /// Pay Ok
        /// </summary>
        public bool PayOk { get; set; }
    }
}
