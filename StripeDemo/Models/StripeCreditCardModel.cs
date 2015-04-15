namespace StripeDemo.Models
{
    public class StripeCreditCardModel
    {
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }

    }
}