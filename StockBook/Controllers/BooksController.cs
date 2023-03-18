using Microsoft.AspNetCore.Mvc;
using StockBook.Models;
using StockBook.Service;

namespace StockBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        public BooksController(BooksService bookService)
        {
            _booksService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookModel>>> GetAll() =>await _booksService.GetAll();
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<BookModel>> GetById(string id) {
            var book = await _booksService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult>Create(BookModel book)
        {
            await _booksService.Create(book);
            return Ok();
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult>Update(string id,BookModel updatedBook)
        {
            var book=await _booksService.GetById(id);
            if(book == null)
            {
                return NotFound();
            }
            updatedBook.Id = id;
            await _booksService.Replace(id, updatedBook);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult>Delete(string id)
        {
            var book = await _booksService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            await _booksService.Remove(id);
            return NoContent();
        }
    }
}
