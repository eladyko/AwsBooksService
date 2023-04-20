using System.Text.Json;
using AwsBooksService.Contract;
using AwsBooksService.Contract.Dtos;
using AwsBooksService.Contract.Events;
using AwsBooksService.Mapping;
using AwsBooksService.Repositories;

namespace AwsBooksService.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IEventService _eventService;

        public BookService(IBookRepository repository, IEventService eventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        public async Task<BookDto> CreateAsync(Book item)
        {
            var existing = await _repository.GetAsync(item.Id);

            if (existing != null)
            {
                throw new InvalidOperationException($"Book with Id={item.Id} already exists");
            }

            var dto = item.ToDto();

            if (await _repository.CreateAsync(dto))
            {
                await _eventService.Push(JsonSerializer.Serialize(new CreateEvent(item)));
            }

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id);

            if (result)
            {
                await _eventService.Push(JsonSerializer.Serialize(new DeleteEvent(id)));
            }

            return result;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            await _eventService.Push(JsonSerializer.Serialize(new ReadEvent(result.Select(x => x.Id).ToArray())));

            return result;
        }

        public async Task<BookDto> GetAsync(Guid id)
        {
            var result = await _repository.GetAsync(id);

            await _eventService.Push(JsonSerializer.Serialize(new ReadEvent(id)));

            return result;
        }

        public async Task<bool> UpdateAsync(Book item)
        {
            var result = await _repository.UpdateAsync(item.ToDto());

            if (result)
            {
                await _eventService.Push(JsonSerializer.Serialize(new UpdateEvent(item)));
            }

            return result;
        }
    }
}
