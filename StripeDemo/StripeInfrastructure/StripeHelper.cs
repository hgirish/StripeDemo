using System;
using System.Configuration;
using System.Threading.Tasks;
using Stripe;

namespace StripeDemo.StripeInfrastructure
{
    public class StripeHelper
    {
        public  StripeHelper()
        {
            string apiKey = ConfigurationManager.AppSettings["StripePrivateKey"];

            StripeConfiguration.SetApiKey(apiKey);

        }
        public async Task<StripeResponseModel> ChargeCustomer(StripeChargeModel model)
        {

            return await Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = decimal.ToInt32(model.Amount * 100),
                    Currency = string.IsNullOrWhiteSpace(model.Currency) ? "USD" : model.Currency,
                    Description = model.Description,
                    CardId = model.StripeToken,
                    ReceiptEmail = model.StripeEmail

                };
                StripeResponseModel result = new StripeResponseModel();
                try
                {
                    var chargeService = new StripeChargeService();
                    var stripeCharge = chargeService.Create(myCharge);
                    result = new StripeResponseModel
                    {
                        ChargeId = stripeCharge.Id,
                        FailureMessage = stripeCharge.FailureMessage,
                        IsError = string.IsNullOrEmpty(stripeCharge.FailureCode),
                        Paid = stripeCharge.Paid
                        
                    };
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerException.GetType() == typeof (StripeException))
                    {
                        StripeException exception = (StripeException) ex.InnerException;
                        Console.WriteLine(exception.Message);
                        result = new StripeResponseModel
                        {
                            FailureMessage = exception.StripeError.Message,
                            FailureCode = exception.StripeError.Code,
                            IsError = true,
                            Paid = false,
                            DeclineCode = exception.StripeError.DeclineCode
                        };


                    }
                    Console.WriteLine(ex.Message);
                    result = new StripeResponseModel
                    {

                        FailureMessage = ex.Message,
                        IsError = true,
                        Paid = false
                    };

                }
                catch (StripeException exception)
                {
                    Console.WriteLine(exception.Message);
                    result = new StripeResponseModel
                    {
                        FailureMessage = exception.StripeError.Message,
                        FailureCode = exception.StripeError.Code,
                        IsError = true,
                        Paid = false,
                        DeclineCode = exception.StripeError.DeclineCode
                    };
                }

                return result;
            });
        }

        public  async Task<StripeResponseModel> CreateToken(CreateTokenModel createTokenModel)
        {
            return await Task.Run(() =>
            {
                var myToken = new StripeTokenCreateOptions();
                Console.WriteLine(createTokenModel.CardNumber);

                // if you need this...
                myToken.Card = new StripeCreditCardOptions()
                {
                    // set this property if using a token
                    //TokenId = *tokenId*,

                    // set these properties if passing full card details (do not
                    // set these properties if you set TokenId)
                    CardNumber = createTokenModel.CardNumber,
                    CardExpirationYear = createTokenModel.ExpiryYear,
                    CardExpirationMonth = createTokenModel.ExpieryMonth,
                    //CardAddressCountry = "US", // optional
                    //CardAddressLine1 = "24 Beef Flank St", // optional
                    //CardAddressLine2 = "Apt 24", // optional
                    //CardAddressCity = "Biggie Smalls", // optional
                    //CardAddressState = "NC", // optional
                    CardAddressZip = createTokenModel.Zip, // optional
                    CardName = createTokenModel.Name, // optional
                    CardCvc = createTokenModel.Cvc // optional
                };


                StripeResponseModel result = new StripeResponseModel();
                try
                {
                    var tokenService = new StripeTokenService();
                    StripeToken stripeToken = tokenService.Create(myToken);
                    result = new StripeResponseModel {TokenId = stripeToken.Id};
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerException.GetType() == typeof(StripeException))
                    {
                        StripeException exception = (StripeException)ex.InnerException;
                        Console.WriteLine(exception.Message);
                        var stripeError = exception.StripeError;
                        Console.WriteLine("{0}: {1}",stripeError.Error,stripeError.ErrorType);
                        result = new StripeResponseModel
                        {
                            FailureMessage = stripeError.Message,
                            FailureCode = stripeError.Code,
                            IsError = true,
                            DeclineCode = stripeError.DeclineCode,
                            Paid = false
 
                        };


                    }
                    Console.WriteLine(ex.Message);
                    result = new StripeResponseModel
                    {

                        FailureMessage = ex.Message,
                        IsError = true,
                        Paid = false
                    };

                }
                catch (StripeException exception)
                {
                    Console.WriteLine(exception.Message);
                       var stripeError = exception.StripeError;
                        Console.WriteLine("{0}: {1}",stripeError.Error,stripeError.ErrorType);
                    
                    result = new StripeResponseModel
                    {

                        FailureMessage = stripeError.Message,
                        FailureCode = stripeError.Code,
                        DeclineCode = stripeError.DeclineCode,
                        IsError = true,
                        Paid = false
                    };
                }
                return result;




            });

        }
    }
}