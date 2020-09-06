using Ks.PayManager.Entities.CreditCard;
using System;
using System.Threading.Tasks;
using VerifyCardServiceReference;

namespace Ks.PayManager.Communication
{
    public class CommunicationManager
    {
        /// <summary>
        /// Credit card info
        /// </summary>
        private CreditCardBaseService PropCreditCardBaseService { get; set; }

        /// <summary>
        /// Validation result
        /// </summary>
        public ValidateCreditCardResponse PropValidateCreditCardResponse { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="creditCardBase">Credit card to validate</param>
        public CommunicationManager(ValidateCreditCardRequest creditCardBase)
        {
            PropCreditCardBaseService = new CreditCardBaseService();
            PropValidateCreditCardResponse = new ValidateCreditCardResponse();
            this.MapCreditCardService(creditCardBase);
        }

        /// <summary>
        /// Validate if credit card is valid
        /// </summary>
        public async Task<ValidateCreditCardResponse> ValidateCreditCard(string url)
        {
            try
            {
                ServiceClient validateCreditCardClient = new ServiceClient();

                validateCreditCardClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(url);

                validateCreditCardClient.Endpoint.Binding.SendTimeout = new System.TimeSpan(0, 1, 30);

                ValidateCreditCardResponseService validateCreditCardResponseService = await validateCreditCardClient.ValidateCreditCardAsync(this.PropCreditCardBaseService);

                return this.MapResultValidation(validateCreditCardResponseService);
            }
            catch (Exception ex)
            {
                return new ValidateCreditCardResponse()
                {
                    MessageValidattion = ex.Message + "Error WCF"
                };
            }

        }

        /// <summary>
        /// Map objects
        /// </summary>
        /// <param name="creditCardBase"></param>
        private void MapCreditCardService(CreditCardBase creditCardBase)
        {
            this.PropCreditCardBaseService.CreditCardNumber = creditCardBase.CreditCardNumber;
            this.PropCreditCardBaseService.ExpiredDate = creditCardBase.ExpiredDate;
            this.PropCreditCardBaseService.ExpiredDateSpecified = creditCardBase.ExpiredDateSpecified;
            this.PropCreditCardBaseService.OwnerName = creditCardBase.OwnerName;
            this.PropCreditCardBaseService.SecurityCode = creditCardBase.SecurityCode;
        }

        /// <summary>
        /// Map result objects
        /// </summary>
        /// <param name="validateCreditCardResponseService">Validate credit card response</param>
        private ValidateCreditCardResponse MapResultValidation(ValidateCreditCardResponseService validateCreditCardResponseService)
        {
            this.PropValidateCreditCardResponse.CreditCardNumber = validateCreditCardResponseService.CreditCardNumber;
            this.PropValidateCreditCardResponse.ExpiredDate = validateCreditCardResponseService.ExpiredDate;
            this.PropValidateCreditCardResponse.ExpiredDateSpecified = validateCreditCardResponseService.ExpiredDateSpecified;
            this.PropValidateCreditCardResponse.MessageValidattion = validateCreditCardResponseService.MessageValidattion;
            this.PropValidateCreditCardResponse.OwnerName = validateCreditCardResponseService.OwnerName;
            this.PropValidateCreditCardResponse.SecurityCode = validateCreditCardResponseService.SecurityCode;
            this.PropValidateCreditCardResponse.Validation = validateCreditCardResponseService.Validation;

            return this.PropValidateCreditCardResponse;
        }
    }
}
