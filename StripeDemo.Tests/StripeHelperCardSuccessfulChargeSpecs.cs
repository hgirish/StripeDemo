using System;
using NUnit.Framework;
using Should;

namespace StripeDemo.Tests
{
    public class StripeHelperCardSuccessfulChargeSpecs 
    {
      
        [Explicit]
        public class CanCreateToken : given_stripehelper.the_card_is_ready
        {
           
            protected override  void When()
            {
                _resultTask =   SUT.CreateToken(_model);
                _resultTask.Wait();
                string token = _resultTask.Result.TokenId;
                _tokenId = token;
                _chargeCustomerModel.TokenId = _tokenId;
                      var chargeResultThread = SUT.ChargeCustomer(_chargeCustomerModel);
                chargeResultThread.Wait();
                if (chargeResultThread.Result != null)
                {
                    model = chargeResultThread.Result;
                    
                    var chargeResult = chargeResultThread.Result;
                    
                    _paid = chargeResult.Paid;
                    _failureMessage = chargeResult.FailureMessage;
                    failureCode = chargeResult.FailureCode;
                    isError = chargeResult.IsError;
                }
            }

            [Test][Explicit]
            public  void  then_the_token_should_be_returned()
            {
                model.TokenId.ShouldNotBeEmpty();
                Console.WriteLine("Token Id: {0}", _tokenId);
            }

            [Test][Explicit]
            public void then_the_card_charged()
            {
                model.Paid.ShouldBeTrue();
                model.FailureMessage.ShouldBeNull();
            }
        }
    }
}