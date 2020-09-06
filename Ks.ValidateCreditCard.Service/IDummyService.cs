using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Ks.ValidateCreditCard.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDummyService
    {
        [OperationContract]
        ValidateCreditCardResponseService ValidateCreditCard(CreditCardBaseService creditCard);
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    // Puede agregar archivos XSD al proyecto. Después de compilar el proyecto, puede usar directamente los tipos de datos definidos aquí, con el espacio de nombres "Ks.ValidateCreditCard.Service.ContractType".
    [DataContract]
    public class CreditCardBaseService
    {
        /// <summary>
        /// Credit Card Number
        /// </summary>
        [DataMember]
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Expired date specified
        /// </summary>
        [DataMember]
        public bool ExpiredDateSpecified { get; set; }

        /// <summary>
        /// Security Code
        /// </summary>
        [DataMember]
        public string SecurityCode { get; set; }

        /// <summary>
        /// Expired Date
        /// </summary>
        [DataMember]
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Owner Name
        /// </summary>
        [DataMember]
        public string OwnerName { get; set; }
    }

    [DataContract]
    public class ValidateCreditCardResponseService : CreditCardBaseService
    {
        /// <summary>
        /// Validation result
        /// </summary>
        [DataMember]
        public bool Validation { get; set; }

        /// <summary>
        /// Message validation
        /// </summary>
        [DataMember]
        public string MessageValidattion { get; set; }
    }
}
