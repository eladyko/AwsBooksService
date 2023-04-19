using AwsBooksService.Contract;
using AwsBooksService.Contract.Dtos;
using AwsBooksService.Mapping;
using AwsBooksService.Repositories;

namespace AwsBooksService.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookDto> CreateAsync(Book item)
        {
            var existing = await _repository.GetAsync(item.Id);

            if (existing != null)
            {
                throw new InvalidOperationException($"Book with Id={item.Id} already exists");
            }

            var dto = item.ToDto();

            await _repository.CreateAsync(dto);

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BookDto> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<bool> UpdateAsync(Book item)
        {
            return await _repository.UpdateAsync(item.ToDto());
        }
    }
}
