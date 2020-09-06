using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Entities.Pay;
using System;

namespace Ks.PayManager.Core.Services
{
    public class PayService : IPayService
    {
        public PayResponse MakePayment(PayRequest payRequest)
        {
            try
            {
                return new PayResponse()
                {
                    Amount = payRequest.Amount,
                    PayMessage = "Payment Ok",
                    PayOk = true
                };
            }
            catch (Exception ex)
            {
                return new PayResponse()
                {
                    PayMessage = ex.Message,
                    PayOk = false
                };
            }

        }
    }
}
