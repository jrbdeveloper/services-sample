using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManager.Models
{
    public class ClientModel
    {
        private AddressModel _address;

        public int ClientId { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public AddressModel Address
        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressModel();
                }

                return _address;
            }
            set { _address = value; }
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Emaill Address")]
        public string EmailAddress { get; set; }
    }
}