using System.ComponentModel.DataAnnotations;

namespace ClientManager.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }

        [Display(Name="Street 1")]
        public string Street1 { get; set; }

        [Display(Name = "Street 2")]
        public string Street2 { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}