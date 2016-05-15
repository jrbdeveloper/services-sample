namespace ServicesService.Contracts
{
    public interface IService
    {
        string BaseUri();

        bool CheckHealth();
    }
}
