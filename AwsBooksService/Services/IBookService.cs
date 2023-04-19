using AwsBooksService.Contract;
using AwsBooksService.Contract.Dtos;

namespace AwsBooksService.Services
{
    public interface IBookService
    {
        Task<BookDto> CreateAsync(Book item);
        Task<BookDto> GetAsync(Guid id);
        Task<IEnumerable<BookDto>> GetAllAsync();

        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Book item);
    }
}
