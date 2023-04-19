namespace AwsBooksService.Contract.Requests
{
    public class UpdateBook
    {
        public string Isbn { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
