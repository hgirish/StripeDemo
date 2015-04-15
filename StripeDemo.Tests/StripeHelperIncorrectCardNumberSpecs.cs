using System;
using NUnit.Framework;
using Should;
using StripeDemo.StripeInfrastructure;

namespace StripeDemo.Tests
{
    public class StripeHelperIncorrectCardNumberSpecs
    {
        [Explicit]
        public class CardDeclineForIncorrectCardNumber : given_stripehelper.the_card_is_ready
        {
            private StripeResponseModel model;
            protected override void When()
            {
                _model.CardNumber = "4242424242424241";
                
                    _resultTask = SUT.CreateToken(_model);
                    _resultTask.Wait();
                    model = _resultTask.Result;
               

                //string token = _resultTask.Result.TokenId;
                //_tokenId = token;
                //_chargeCustomerModel.TokenId = _tokenId;
                ////try
                ////{
                //var chargeResultThread = SUT.ChargeCustomer(_chargeCustomerModel);
                //chargeResultThread.Wait();
                //if (chargeResultThread.Result != null)
                //{
                //    var chargeResult = chargeResultThread.Result;
                //    _paid = chargeResult.Paid;
                //    _failureMessage = chargeResult.FailureMessage;
                //    failureCode = chargeResult.FailureCode;
                //    isError = chargeResult.IsError;
                //}
                


            }

            [Test]
            [Explicit]
            public void then_card_declined()
            {
                model.Paid.ShouldBeFalse();
                model.FailureMessage.ShouldNotBeEmpty();
                model.FailureCode.ShouldNotBeEmpty();
                model.FailureCode.ShouldEqual("incorrect_number");
                model.IsError.ShouldBeTrue();
                Console.WriteLine(model.FailureCode);
                //Assert.Throws<AggregateException>(() => SUT.ChargeCustomer(_chargeCustomerModel));
            }


        }
 
    }
}