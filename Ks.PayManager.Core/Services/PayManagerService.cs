using Ks.PayManager.Core.Interfaces;
using Ks.PayManager.Core.Options;
using Ks.PayManager.Entities.Bpms;
using Ks.PayManager.Entities.ManagePayment;
using Ks.PayManager.Entities.Notify;
using Ks.PayManager.Entities.Pay;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Ks.PayManager.Core.Services
{
    public class PayManagerService : IPayManagerService
    {
        public ManagePaymentResponse ManagePayment(ManagePaymentRequest managePaymentRequest, ApplicationSettings applicationSettings)
        {
            ManagePaymentResponse managePaymentResponse = new ManagePaymentResponse();
            
            IValidateCreditCardService validateCreditCard = new ValidateCreditCardService();

            managePaymentRequest.PropValidateCreditCardRequest.Address = applicationSettings.ValidateCreditCard;

            managePaymentResponse.PropValidateCreditCardResponse = validateCreditCard.ValidateCreditCard(managePaymentRequest.PropValidateCreditCardRequest);

            //managePaymentResponse.PropValidateCreditCardResponse = new Entities.CreditCard.ValidateCreditCardResponse
            //{
            //    Validation = true
            //};

            if (managePaymentResponse.PropValidateCreditCardResponse.Validation)
            {
                IComplementRequestService complementRequestService = new ComplementRequestService();

                OrderObj objComplete = complementRequestService.GetComplementRequest(Convert.ToInt32(managePaymentRequest.PropUpdateOrderRequest.OrderId), applicationSettings.Complement);

                objComplete.Destino = objComplete.Destino.ToUpper();

                managePaymentRequest.PropPayRequest = new PayRequest() 
                { 
                    Amount = objComplete.MontoOrden 
                };

                IPayService pay = new PayService();

                managePaymentResponse.PropPayResponse = pay.MakePayment(managePaymentRequest.PropPayRequest);

                if (managePaymentResponse.PropPayResponse.PayOk)
                {

                    IOrderService orderService = new OrderService();

                    managePaymentResponse.PropUpdateOrderResponse = orderService.UpdateOrderPaymentStatus(managePaymentRequest.PropUpdateOrderRequest, applicationSettings.OrderServiceUrl);

                    if (managePaymentResponse.PropUpdateOrderResponse.OperationMessage == "OK")
                    {
                        BonitaResponse bonitaResponse = this.InstanciarProcesoBpm(objComplete, applicationSettings);

                        this.NotifyClient(new Notify()
                        {
                            Email = objComplete.Correo,
                            Message = "Orden generada con exito",
                            Subject = $"Orden #{ objComplete.OrderId }"
                        }, applicationSettings);

                        managePaymentResponse.PropBonitaResponse = bonitaResponse;
                    }
                    else
                    {
                        var order = orderService.GetOrder(objComplete.OrderId.ToString(), applicationSettings.OrderServiceUrl);

                        managePaymentResponse.PropUpdateOrderResponse.Comments = order.Comments;
                        managePaymentResponse.PropUpdateOrderResponse.CustId = order.CustId;
                        managePaymentResponse.PropUpdateOrderResponse.OrderDate = order.OrderDate;
                        managePaymentResponse.PropUpdateOrderResponse.OrderId = order.OrderId;
                        managePaymentResponse.PropUpdateOrderResponse.Price = order.Price;
                        managePaymentResponse.PropUpdateOrderResponse.Status = order.Status;
                    }
                }
                else
                {
                    managePaymentResponse.PropPayResponse.PayMessage = "No fue posible efectuar el pago";
                }
            }
            else
            {
                managePaymentResponse.PropUpdateOrderResponse = null;
            }

            return managePaymentResponse;
        }

        private BonitaResponse InstanciarProcesoBpm(OrderObj orderObj, ApplicationSettings applicationSettings)
        {
            IBpmService bpmService = new BpmService();

            BonitaResponse bonitaResponse = bpmService.CreateProcessInstanceBonita(new ProcesoDetalle()
                {
                    UserName = applicationSettings.BpmUser,
                    Pws = applicationSettings.BpmPws,
                    NombreProceso = applicationSettings.BpmProcessName,
                    objectordenInput = orderObj,

                }, applicationSettings.Bonita);

            return bonitaResponse;
        }

        private void NotifyClient(Notify notify, ApplicationSettings applicationSettings)
        {
            INotifierService notifierService = new NotifierService();

            notifierService.NotifyClient(notify, applicationSettings);
        }
    }
}
