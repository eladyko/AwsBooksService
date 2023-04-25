using System.Text.Json.Serialization;

namespace AwsBooksService.Contract.Dtos
{
    public class BookDto
    {
        public Guid Id { get; init; }
        public string Isbn { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
