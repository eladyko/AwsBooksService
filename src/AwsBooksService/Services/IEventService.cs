namespace AwsBooksService.Services
{
    public interface IEventService
    {
        Task<bool> Push(string message);
    }
}