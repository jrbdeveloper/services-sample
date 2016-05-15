using System.ComponentModel.DataAnnotations;

namespace ClientManager.Models
{
    public class PropertyModel
    {
        public int PropertyId { get; set; }

        public int? AddressId { get; set; }

        public AddressModel Address { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public string Style { get; set; }

        [Display(Name = "Year Built")]
        public int? YearBuilt { get; set; }

        [Display(Name = "Lot Size")]
        public int? LotSize { get; set; }

        [Display(Name = "Square Footage")]
        public int? SquareFootage { get; set; }
    }
}