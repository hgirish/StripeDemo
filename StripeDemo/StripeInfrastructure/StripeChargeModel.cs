namespace StripeDemo.StripeInfrastructure
{
    public class StripeChargeModel
    {
        public string TokenId { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

    }
}