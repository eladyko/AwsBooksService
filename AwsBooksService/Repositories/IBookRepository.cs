using AwsBooksService.Contract.Dtos;

namespace AwsBooksService.Repositories
{
    public interface IBookRepository
    {
        Task<BookDto> GetAsync(Guid id);
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<bool> UpdateAsync(BookDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CreateAsync(BookDto item);
    }
}
