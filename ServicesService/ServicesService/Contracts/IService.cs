namespace ServicesService.Contracts
{
    public interface IService
    {
        string Uri();

        bool CheckHealth();
    }
}
