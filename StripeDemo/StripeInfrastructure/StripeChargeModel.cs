using System.ComponentModel.DataAnnotations;

namespace StripeDemo.StripeInfrastructure
{
    public class StripeChargeModel
    {
        [Required]
        public string StripeToken { get; set; }
        [Required]
        public string StripeEmail { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
      public string StatementDescriptor { get; set; }
    }
}