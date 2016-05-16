namespace ClientManager.Models
{
    public class MatchingModel
    {
        public int MatchingId { get; set; }

        public ClientModel Client { get; set; }

        public PropertyModel Property { get; set; }
    }
}