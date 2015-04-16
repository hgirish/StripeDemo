using System;
using System.Threading.Tasks;
using SpecsFor;
using StripeDemo.StripeInfrastructure;

namespace StripeDemo.Tests
{
    public class given_stripehelper
    {
        public class the_card_is_ready : SpecsFor<StripeHelper>
        {
            protected CreateTokenModel _model;
            protected StripeChargeModel _chargeCustomerModel;

            protected  Task<StripeResponseModel> _resultTask;

            protected StripeResponseModel model;
            protected  string _tokenId;
            protected  string failureCode;
            protected  bool isError;
            protected  bool _paid;
            protected  string _failureMessage;
            protected override void Given()
            {
                var email = "test@sometest.com";
                _model = new CreateTokenModel
                {
                    CardNumber = "4242424242424242",
                    ExpieryMonth = "10",
                    ExpiryYear = DateTime.Now.AddYears(2).Year.ToString(),
                    Cvc = "123",
                    Email = email,
                    Zip = "12345",
                    Name = "John Doe Smith"

                };

                Random random = new Random();
                double number = random.NextDouble();
                if (number < 0.01)
                {
                    number = 0.01;
                }

                var amount = Convert.ToDecimal(number * 100);
                amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
                Console.WriteLine("Amount: {0}", amount);
                _chargeCustomerModel = new StripeChargeModel
                {
                    Amount = amount,
                    Currency = "usd",
                    Description = string.Format("Charge for : {0}", amount),
                    StripeEmail = email
                };

            }
        }
    }
}