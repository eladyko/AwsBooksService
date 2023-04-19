namespace AwsBooksService.Contract
{
    public class Book
    {
        public Guid Id { get; init; }
        public string Isbn { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
