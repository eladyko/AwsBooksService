using AwsBooksService.Contract;
using AwsBooksService.Contract.Dtos;
using AwsBooksService.Contract.Requests;

namespace AwsBooksService.Mapping
{
    public static class MappingHelper
    {
        public static Book ToModel(this CreateBook create) =>
            new()
            {
                Id = Guid.NewGuid(),
                Isbn = create.Isbn,
                Title = create.Title,
                Description = create.Description
            };

        public static Book ToModel(this BookDto dto) =>
            new()
            {
                Id = dto.Id,
                Isbn = dto.Isbn,
                Title = dto.Title,
                Description = dto.Description
            };

        public static ReadBook ToReadModel(this Book book) =>
            new()
            {
                ID = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                Description = book.Description
            };

        public static ReadBook ToReadModel(this BookDto dto) =>
            new()
            {
                ID = dto.Id,
                Isbn = dto.Isbn,
                Title = dto.Title,
                Description = dto.Description
            };

        public static BookDto ToDto(this Book book) =>
            new()
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                Description = book.Description
            };

        public static Book ToModel(this UpdateBook update, Guid id) =>
            new()
            {
                Id = id,
                Isbn = update.Isbn,
                Title = update.Title,
                Description = update.Description
            };
    }
}
