namespace StripeDemo.StripeInfrastructure
{
    public class StripeResponseModel
    {
        public bool Paid { get; set; }
        public bool IsError { get; set; }
        public string FailureMessage { get; set; }
        public string FailureCode { get; set; }
        public string DeclineCode { get; set; }
        public string ChargeId { get; set; }
        public string TokenId { get; set; }
    }
}