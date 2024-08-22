using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        // IOC register , resolve , dispose
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {

            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);



        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBooksAsync(int id)
        {

            var book = await _manager
                .BookService.GetOneBookByIdAsync(id, false);

            return Ok(book);


        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {

            if (bookDto == null)
                return BadRequest(); // 400

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book); // createdAtRoute() 


        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync(int id, BookDtoForUpdate bookDto)
        {
            if (bookDto == null)
                return BadRequest(); // 400
            //book var mı yok mu kontrol , güncellenecek kitabın bilgisini çekiyor 

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); // 422

            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

            return NoContent(); //204


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync(int id, Book book)
        {

            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallUpdateOneBookAsync(int id, JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest(); // 400


            // check entitiy
            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);


            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); // 204


        }
    }
}
