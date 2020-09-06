using System;
using System.Text;

namespace Ks.ValidateCreditCard.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class DummyService : IDummyService
    {
        public ValidateCreditCardResponseService ValidateCreditCard(CreditCardBaseService creditCard)
        {
            bool validacion = false;
            StringBuilder digitsOnly = new StringBuilder();

            for (int i = 0; i < creditCard.CreditCardNumber.Length; i++)
            {
                if (Char.IsDigit(creditCard.CreditCardNumber[i])) digitsOnly.Append(creditCard.CreditCardNumber[i]);
            }

            if (digitsOnly.Length > 18 || digitsOnly.Length < 15) return this.Respuesta(creditCard, false);

            int sum = 0;
            int digit = 0;
            int addend = 0;
            bool timesTwo = false;

            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                digit = Int32.Parse(digitsOnly.ToString(i, 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                        addend -= 9;
                }
                else
                    addend = digit;

                sum += addend;

                timesTwo = !timesTwo;

            }
            validacion = (sum % 10) == 0;

            return this.Respuesta(creditCard, validacion);
        }

        private ValidateCreditCardResponseService Respuesta(CreditCardBaseService creditCard, bool validacion)
        {
            return new ValidateCreditCardResponseService()
            {
                CreditCardNumber = creditCard.CreditCardNumber,
                ExpiredDate = creditCard.ExpiredDate,
                ExpiredDateSpecified = creditCard.ExpiredDateSpecified,
                MessageValidattion = validacion ? "Tarjeta Valida" : "Tarjeta Invalida",
                OwnerName = creditCard.OwnerName,
                SecurityCode = creditCard.SecurityCode,
                Validation = validacion
            };
        }
    }
}
