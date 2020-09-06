using Ks.PayManager.Communication;
using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Entities.CreditCard;
using System;

namespace Ks.PayManager.Core.Services
{
    public class ValidateCreditCardService : IValidateCreditCardService
    {
        public ValidateCreditCardResponse ValidateCreditCard(ValidateCreditCardRequest validateCreditCardRequest)
        {
            try
            {
                CommunicationManager communicationManager = new CommunicationManager(validateCreditCardRequest);

                return (communicationManager.ValidateCreditCard(validateCreditCardRequest.Address)).Result;
                //return communicationManager.PropValidateCreditCardResponse;
            }
            catch (Exception ex)
            {
                return new ValidateCreditCardResponse()
                {
                    Validation = false,
                    MessageValidattion = ex.Message + ex.InnerException == null ? string.Empty : ex.StackTrace
                };
            }
        }
    }
}
