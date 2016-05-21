using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ClientManager.Models
{
    public class PropertyModel
    {
        private GalleryModel _gallery;

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

        [JsonIgnore]
        public HttpPostedFileBase Photo { get; set; }

        public GalleryModel Gallery
        {
            get
            {
                if (_gallery == null)
                {
                    _gallery = new GalleryModel();
                }
                return _gallery;
            }
            set { _gallery = value; }
        }
    }
}