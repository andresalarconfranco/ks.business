using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Core.Options;
using Ks.PayManager.Core.Services;
using Ks.PayManager.Entities.CreditCard;
using Ks.PayManager.Entities.ManagePayment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestSharp.Validation;

namespace Ks.PayManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagePaymentController : ControllerBase
    {

        private IOptions<ApplicationSettings> settings;

        public ManagePaymentController(IOptions<ApplicationSettings> settings)
        {
            this.settings = settings;
        }

        [HttpGet]
        public ManagePaymentResponse Get([FromBody] ManagePaymentRequest managePaymentRequest)
        {
            IPayManagerService payManagerService = new PayManagerService();

            return payManagerService.ManagePayment(managePaymentRequest, this.settings.Value);
        }

        [HttpPost]
        public ManagePaymentResponse Post([FromBody] ManagePaymentRequest managePaymentRequest)
        {
            IPayManagerService payManagerService = new PayManagerService();

            return payManagerService.ManagePayment(managePaymentRequest, this.settings.Value);
        }

        [HttpPost]
        [Route("Validate")]
        public ValidateCreditCardResponse PostValidate([FromBody] ValidateCreditCardRequest validateCreditCardRequest)
        {
            IValidateCreditCardService validateCreditCardService = new ValidateCreditCardService();
            validateCreditCardRequest.Address = this.settings.Value.ValidateCreditCard;

            return validateCreditCardService.ValidateCreditCard(validateCreditCardRequest);
        }
    }
}
