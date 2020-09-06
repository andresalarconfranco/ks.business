using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ks.Entities.Authentication;
using Ks.Entities.Customer;
using Ks.Entities.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Ks.Customer.Business.Interfaces;

namespace Ks.Customer.Business.Implementation
{
    public class ServiceCustomer :  Connections.ConnectionsApi<Users, PicUsers>, IServiceCustomer
    {
        public IConfiguration Configuration { get; }

        public ServiceCustomer(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Register(CustomerRegister ClientIn)
        {
            try
            {
                /*Cargar Información del Cliente*/
                Customers Regcliente = new Customers();
                Regcliente.CustID =  ClientIn.CustID.Trim();
                Regcliente.FName = ClientIn.FName.Trim();
                Regcliente.LName = ClientIn.LName.Trim();
                Regcliente.PhoneNumber = ClientIn.PhoneNumber.Trim();
                Regcliente.EMail = ClientIn.EMail.Trim();
                Regcliente.Password = ClientIn.Password.Trim();
                Regcliente.CreditCardType = ClientIn.CreditCardType.Trim();
                Regcliente.CrediCardNumber = ClientIn.CrediCardNumber.Trim();
                Regcliente.Status = "Ingresado";
                Regcliente.IdCategory = "1";
                Regcliente.Country = ClientIn.Country.Trim();
                Regcliente.City = ClientIn.City.Trim();
                Regcliente.ROL_NCODE = ClientIn.ROL_NCODE;


                /*Validar Cliente*/
                var clientCustomer = new RestClient(Configuration["UrlApis:GetCustomer"] + ClientIn.CustID.Trim());

                clientCustomer.Timeout = -1;
                var requestCustomer = new RestRequest(Method.GET);
                IRestResponse responseCustomer = clientCustomer.Execute(requestCustomer);
                var response_ = JsonConvert.DeserializeObject<Customers>(responseCustomer.Content.ToString());
                Console.WriteLine(responseCustomer.Content);
                if (response_ != null)
                {
                    return "El Cliente ya Existe";
                }

                /*Validar Correo*/
                var clientCustomerMail = new RestClient(Configuration["UrlaApis:GetCustomerByEmail"] + ClientIn.EMail.Trim());

                clientCustomerMail.Timeout = -1;
                var requestCustomerMail = new RestRequest(Method.GET);
                IRestResponse responseCustomerMail = clientCustomerMail.Execute(requestCustomerMail);
                var responseMail_ = JsonConvert.DeserializeObject<List<CustomerDto>>(responseCustomerMail.Content.ToString());
                Console.WriteLine(responseCustomerMail.Content);
                if (responseMail_.Count() > 0)
                {
                    return "El Correo ya Existe";
                }

                /* Cargar Información del Usuario*/
                Users RegUser = new Users();
                RegUser.UseNcode = 0;
                RegUser.UseCemail = ClientIn.EMail.Trim(); ;
                RegUser.UseCpassword = ClientIn.Password.Trim(); ;
                RegUser.role = ClientIn.ROL_NCODE;

                /*Cargar Información de la Dirección*/
                Address RegAddress = new Address();
                RegAddress.AddrId = 0;
                RegAddress.State = ClientIn.State;
                RegAddress.Country = ClientIn.Country;
                RegAddress.City = ClientIn.City;
                RegAddress.AddressType = ClientIn.AddressType;
                RegAddress.Address1 = ClientIn.Address;
                RegAddress.Neighborhood = ClientIn.Neighborhood;


                /*Guardar información Cliente*/
                var client = new RestClient(Configuration["UrlApis:SaveCustomer"]);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(Regcliente), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                /*Guardar información de la Dirección*/
                var clientAddressReg = new RestClient(Configuration["UrlApis:SaveAddress"]);

                clientAddressReg.Timeout = -1;
                var requestAddressReg = new RestRequest(Method.POST);
                requestAddressReg.AddHeader("Content-Type", "application/json");
                requestAddressReg.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(RegAddress), ParameterType.RequestBody);
                IRestResponse responseAddressReg = clientAddressReg.Execute(requestAddressReg);
                var responseAddressReg_ = JsonConvert.DeserializeObject<Address>(responseAddressReg.Content.ToString());
                Console.WriteLine(responseAddressReg.Content);




                /*Guardar información de la Dirección con el cliente*/
                Customer_Address RegCustomer_Address = new Customer_Address();
                RegCustomer_Address.CustId = ClientIn.CustID;
                RegCustomer_Address.AddressId = responseAddressReg_.AddrId;

                /*Guardar información Dirección Cliente*/
                var clientCustomer_AddressReg = new RestClient(Configuration["UrlApis:SaverAddressCustomer"]);

                clientCustomer_AddressReg.Timeout = -1;
                var requestCustomer_AddressReg = new RestRequest(Method.POST);
                requestCustomer_AddressReg.AddHeader("Content-Type", "application/json");
                requestCustomer_AddressReg.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(RegCustomer_Address), ParameterType.RequestBody);
                IRestResponse responseCustomer_Address = clientAddressReg.Execute(requestAddressReg);
                var responseCustomer_Address_ = JsonConvert.DeserializeObject<Address>(responseCustomer_Address.Content.ToString());
                Console.WriteLine(responseCustomer_Address.Content);

                /*Prueba*/
                /*Guardar información del usuario*/

                PicUsers responseBody_ = GetInfo(Configuration["UrlApis:SaveUser"], RegUser, null);
                /*Prueba*/

                /*
                var clientSecurity = new RestClient("http://10.39.1.212:8080/Security/API/PicUsers");

                clientSecurity.Timeout = -1;
                var requestSecurity = new RestRequest(Method.POST);
                requestSecurity.AddHeader("Content-Type", "application/json");
                requestSecurity.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(RegUser), ParameterType.RequestBody);
                IRestResponse responseSecurity = clientSecurity.Execute(requestSecurity);
                var responseBody_ = JsonConvert.DeserializeObject<Entities.PicUsers>(responseSecurity.Content.ToString());
                
                Console.WriteLine(responseSecurity.Content);
                */

                /*Cargar información del usuario y rol*/
                PicRoleUser RolerUser = new PicRoleUser();

                RolerUser.RusNcode = 0;
                RolerUser.RolNcode = ClientIn.ROL_NCODE;
                RolerUser.UseNcode = responseBody_.UseNcode;



                /*Guardar información del usuario con el rol*/
                var clientRolUser = new RestClient(Configuration["UrlApis:SaveRol"]);

                clientRolUser.Timeout = -1;
                var requestRolUser = new RestRequest(Method.POST);
                requestRolUser.AddHeader("Content-Type", "application/json");
                requestRolUser.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(RegUser), ParameterType.RequestBody);
                IRestResponse responseRolUser = clientRolUser.Execute(requestRolUser);
                var responseBodyRolUser_ = JsonConvert.DeserializeObject<PicRoleUser>(responseRolUser.Content.ToString());

                Console.WriteLine(responseRolUser.Content);
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                return "false";
            }

            return "true";

            /*
            string _baseUrl = "http://10.39.1.212/Ks.Customer/";

            var client = new RestClient("http://10.39.1.212/Ks.Customer/api/Customers");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var responseBody_ = JsonConvert.DeserializeObject<List<Entities.Customer>>(response.Content.ToString());
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            */
        }
        public bool SingSingIn(LoginUser LoginIn)
        {
            try
            {
                var client = new RestClient(Configuration["UrlApis:GetCustomerByEmail"] + LoginIn.IdUser);

                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                var response_ = JsonConvert.DeserializeObject<List<CustomerDto>>(response.Content.ToString());

                if(response_.Count() > 0 && response_[0].Password.Trim().Equals(LoginIn.Password.Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }



                //
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                return false;
            }

            
        }
        public bool ChangeCategory(string CustID, string IdCategory)
        {
            return true;
        }
        public CustomerInfo GetCustomerInfo(InfoCustomer value)
        {
            string CustID = value.CustID;
            string email = value.email;
            CustomerInfo resultCustomer = new CustomerInfo();

            if((string.IsNullOrEmpty(CustID)&&string.IsNullOrEmpty(email)) || ((!string.IsNullOrEmpty(CustID)) && (!string.IsNullOrEmpty(email))))
            {
                return null;
            }
            if (!string.IsNullOrEmpty(CustID))
            {
                var clientCustomer = new RestClient(Configuration["UrlApis:GetCustomer"] + CustID.Trim());

                clientCustomer.Timeout = -1;
                var requestCustomer = new RestRequest(Method.GET);
                IRestResponse responseCustomer = clientCustomer.Execute(requestCustomer);
                var response_ = JsonConvert.DeserializeObject<Customers>(responseCustomer.Content.ToString());
                Console.WriteLine(responseCustomer.Content);
                if (response_ == null)
                {
                    return null;
                }
                else
                {
                    string categoria = string.Empty;
                    switch (response_.IdCategory)
                    {
                        case "1":
                            categoria = "PLATINO";
                            break;
                        case "2":
                            categoria = "DORADO";
                            break;
                        case "3":
                            categoria = "PLATEADO";
                            break;
                    }

                    resultCustomer.CustID = response_.CustID;
                    resultCustomer.FName = response_.FName;
                    resultCustomer.LName = response_.LName;
                    resultCustomer.PhoneNumber = response_.PhoneNumber;
                    resultCustomer.EMail = response_.EMail;
                    resultCustomer.Password = response_.Password;
                    resultCustomer.CreditCardType = response_.CreditCardType;
                    resultCustomer.CrediCardNumber = response_.CrediCardNumber;
                    resultCustomer.Status = response_.Status;
                    resultCustomer.Category = categoria;
                    resultCustomer.Country = response_.Country;
                    resultCustomer.City = response_.City;
                }

}
            if(!string.IsNullOrEmpty(email))
            {
                var clientCustomerMail = new RestClient(Configuration["UrlApis:GetCustomerByEmail"] + email.Trim());

                clientCustomerMail.Timeout = -1;
                var requestCustomerMail = new RestRequest(Method.GET);
                IRestResponse responseCustomerMail = clientCustomerMail.Execute(requestCustomerMail);
                var response_ = JsonConvert.DeserializeObject<List<CustomerDto>>(responseCustomerMail.Content.ToString());
                Console.WriteLine(responseCustomerMail.Content);
                if (response_.Count() == 0)
                {
                    return null;
                }
                
                string categoria = string.Empty;
                switch (response_[0].IdCategory)
                {
                    case "1":
                        categoria = "PLATINO";
                        break;
                    case "2":
                        categoria = "DORADO";
                        break;
                    case "3":
                        categoria = "PLATEADO";
                        break;
                }


                resultCustomer.CustID = response_[0].CustID;
                resultCustomer.FName = response_[0].FName;
                resultCustomer.LName = response_[0].LName;
                resultCustomer.PhoneNumber = response_[0].PhoneNumber;
                resultCustomer.EMail = response_[0].EMail;
                resultCustomer.Password = response_[0].Password;
                resultCustomer.CreditCardType = response_[0].CreditCardType;
                resultCustomer.CrediCardNumber = response_[0].CrediCardNumber;
                resultCustomer.Status = response_[0].Status;
                resultCustomer.Category = categoria;
                resultCustomer.Country = response_[0].Country;
                resultCustomer.City = response_[0].City;

            }

            var clientCustomerAddress = new RestClient(Configuration["UrlApis:GetAddressCustomerByCustId"] + resultCustomer.CustID.Trim());

            clientCustomerAddress.Timeout = -1;
            var requestCustomerAddress = new RestRequest(Method.GET);
            IRestResponse responseCustomerAddress = clientCustomerAddress.Execute(requestCustomerAddress);
            var responseCustomerAddress_ = JsonConvert.DeserializeObject<List<Customer_Address>>(responseCustomerAddress.Content.ToString());
            Console.WriteLine(responseCustomerAddress.Content);
            if (responseCustomerAddress_.Count() == 0)
            {
                return null;
            }

            var clientAddress = new RestClient(Configuration["UrlApis:GetAddress"] + int.Parse(responseCustomerAddress_[0].AddressId.ToString().Replace(",0", "").Replace(".0", "")).ToString().Trim());

            clientAddress.Timeout = -1;
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAdress = clientAddress.Execute(requestAddress);
            var responseAddress_ = JsonConvert.DeserializeObject<Address>(responseAdress.Content.ToString());
            Console.WriteLine(responseAdress.Content);
            if (responseAddress_ == null)
            {
                return null;
            }

            resultCustomer.State = responseAddress_.State;
            resultCustomer.AddressType = responseAddress_.AddressType;
            resultCustomer.Address1 = responseAddress_.Address1;
            resultCustomer.Neighborhood = responseAddress_.Neighborhood;


            return resultCustomer;
        }
    }
}
