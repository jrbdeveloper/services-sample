namespace ClientManager.Models
{
    public class MatchingModel
    {
        private ClientModel _client;
        private PropertyModel _property;

        public int MatchingId { get; set; }

        public int ClientId { get; set; }

        public int PropertyId { get; set; }

        public ClientModel Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ClientModel();
                }

                return _client;

            }
            set { _client = value; }
        }

        public PropertyModel Property
        {
            get
            {
                if (_property == null)
                {
                    _property = new PropertyModel();
                }

                return _property;
            }
            set { _property = value; }
        }
    }
}