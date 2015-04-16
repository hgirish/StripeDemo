using System;
using NUnit.Framework;
using Should;

namespace StripeDemo.Tests
{
    public class StripeHelperDebitCardSuccessfulChargeSpecs 
    {
      
        [Explicit]
        public class ChargeMasterDebitCard : given_stripehelper.the_card_is_ready
        {
            protected override void Given()
            {
                base.Given();
                _model.CardNumber = "5200828282828210";
            }

            protected override  void When()
            {
                _resultTask =   SUT.CreateToken(_model);
                _resultTask.Wait();
                string token = _resultTask.Result.TokenId;
                _tokenId = token;
                _chargeCustomerModel.StripeToken = _tokenId;
                      var chargeResultThread = SUT.ChargeCustomer(_chargeCustomerModel);
                chargeResultThread.Wait();
                if (chargeResultThread.Result != null)
                {
                    var chargeResult = chargeResultThread.Result;
                    _paid = chargeResult.Paid;
                    _failureMessage = chargeResult.FailureMessage;
                    failureCode = chargeResult.FailureCode;
                    isError = chargeResult.IsError;
                }
            }

            [Test]
            public  void  then_the_token_should_be_returned()
            {
                _tokenId.ShouldNotBeEmpty();
                Console.WriteLine("Token Id: {0}", _tokenId);
            }

            [Test]
            public void then_the_card_charged()
            {
                _paid.ShouldBeTrue();
                _failureMessage.ShouldBeNull();
            }
        }


        [Explicit]
        public class ChargeVisaDebitCard : given_stripehelper.the_card_is_ready
        {
            protected override void Given()
            {
                base.Given();
                _model.CardNumber = "4000056655665556";
            }

            protected override void When()
            {
                _resultTask = SUT.CreateToken(_model);
                _resultTask.Wait();
                string token = _resultTask.Result.TokenId;
                _tokenId = token;
                _chargeCustomerModel.StripeToken = _tokenId;
                var chargeResultThread = SUT.ChargeCustomer(_chargeCustomerModel);
                chargeResultThread.Wait();
                if (chargeResultThread.Result != null)
                {
                    var chargeResult = chargeResultThread.Result;
                    _paid = chargeResult.Paid;
                    _failureMessage = chargeResult.FailureMessage;
                    failureCode = chargeResult.FailureCode;
                    isError = chargeResult.IsError;
                }
            }

            [Test]
            public void then_the_token_should_be_returned()
            {
                _tokenId.ShouldNotBeEmpty();
                Console.WriteLine("Token Id: {0}", _tokenId);
            }

            [Test]
            public void then_the_card_charged()
            {
                _paid.ShouldBeTrue();
                _failureMessage.ShouldBeNull();
            }
        }


        [Explicit]
        public class ChargeVisaDebitCardFail : given_stripehelper.the_card_is_ready
        {
            
            protected override void When()
            {
                _model.CardNumber = "4000056655665564";
                _resultTask = SUT.CreateToken(_model);
                _resultTask.Wait();
                string token = _resultTask.Result.TokenId;
                _tokenId = token;
                _chargeCustomerModel.StripeToken = _tokenId;
                var chargeResultThread = SUT.ChargeCustomer(_chargeCustomerModel);
                chargeResultThread.Wait();
                if (chargeResultThread.Result != null)
                {
                    model = chargeResultThread.Result;
                    model.TokenId = _tokenId;
                    var chargeResult = chargeResultThread.Result;
                    
                    _paid = chargeResult.Paid;
                    _failureMessage = chargeResult.FailureMessage;
                    failureCode = chargeResult.FailureCode;
                    isError = chargeResult.IsError;
                }
            }

            [Test]
            public void then_the_token_should_be_returned()
            {
                model.TokenId.ShouldNotBeEmpty();
                Console.WriteLine("Token Id: {0}", _tokenId);
            }

            [Test]
            public void then_the_card_charged()
            {
                model.Paid.ShouldBeTrue();
                model.FailureMessage.ShouldBeNull();
            }
        }

    }
}