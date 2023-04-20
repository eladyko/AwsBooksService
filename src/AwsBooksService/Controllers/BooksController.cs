using AwsBooksService.Contract.Requests;
using AwsBooksService.Mapping;
using AwsBooksService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwsBooksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _bookService.GetAllAsync();

            return Ok(items.Select(x => x.ToReadModel()));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var item = await _bookService.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item.ToReadModel());
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBook book)
        {
            var model = book.ToModel();

            await _bookService.CreateAsync(model);

            return CreatedAtAction(nameof(Get), new { model.Id }, model.ToReadModel());
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBook book)
        {
            var item = await _bookService.GetAsync(id);

            if (item == null)
            {
                return NotFound(id);
            }

            var result = await _bookService.UpdateAsync(book.ToModel(id));

            return base.Ok(result);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _bookService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
