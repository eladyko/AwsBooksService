namespace AwsBooksService.Contract.Events
{
    public record ReadEvent(params Guid[] ReadedIds);
}
