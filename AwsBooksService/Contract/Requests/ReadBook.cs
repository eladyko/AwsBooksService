namespace AwsBooksService.Contract.Requests
{
    public class ReadBook
    {
        public Guid ID { get; init; }
        public string Isbn { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
