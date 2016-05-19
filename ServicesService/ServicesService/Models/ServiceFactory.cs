using ServicesService.Contracts;

namespace ServicesService.Models
{
    public static class Service
    {
        public static IService Get(string service) {

            switch (service.ToLower()) {
                case "address":
                    return new AddressService();

                case "client":
                    return new ClientService();

                case "property":
                    return new PropertyService();

                case "logger":
                    return new LoggingService();

                case "matching":
                    return new MatchingService();

                case "gallery":
                    return new GalleryService();

                default:
                    return new ClientService();
            }
        }
    }
}