using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManager.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Emaill Address")]
        public string EmailAddress { get; set; }
    }
}