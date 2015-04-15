using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StripeDemo.Models
{
    public class StripeChargeModel
    {
        [Required][HiddenInput(DisplayValue = false)]
        public string Token { get; set; }

        [Required]
        public double Amount { get; set; }

        public string CardHolderName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountry { get; set; }
    }
}