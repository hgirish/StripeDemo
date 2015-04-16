namespace StripeDemo.StripeInfrastructure
{
    public class StripeChargeModel
    {
        public string StripeToken { get; set; }
        public string StripeEmail { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

    }
}