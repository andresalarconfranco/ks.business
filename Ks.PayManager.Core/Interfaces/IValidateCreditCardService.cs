using Ks.PayManager.Entities.CreditCard;
using System.Threading.Tasks;

namespace Ks.PayManager.Core.Interfaces
{
    public interface IValidateCreditCardService
    {
        /// <summary>
        /// Validate if credit card is valid
        /// </summary>
        /// <param name="validateCreditCardRequest">Validate credit card request</param>
        /// <returns>Validate credit card response</returns>
        ValidateCreditCardResponse ValidateCreditCard(ValidateCreditCardRequest validateCreditCardRequest);
    }
}
