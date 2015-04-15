namespace StripeDemo.StripeInfrastructure
{
    public class CreateTokenModel
    {

        public string CardNumber { get;  set; }

        public string ExpiryYear { get;  set; }

        public string ExpieryMonth { get;  set; }

        public string Name { get;  set; }

        public string Cvc { get;  set; }

        public string Email { get;  set; }

        public string Zip { get;  set; }
    }
}