using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IActionResult GetAllBooks()
        {

            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);



        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks(int id)
        {

            var book = _manager
                .BookService.GetOneBookById(id, false);

            return Ok(book);


        }

        [HttpPost]
        public IActionResult CreateOneBook(Book book)
        {

            if (book == null)
                return BadRequest(); // 400

            _manager.BookService.CreateOneBook(book);

            return StatusCode(201, book);


        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook(int id, BookDtoForUpdate bookDto)
        {
            if (bookDto == null)
                return BadRequest(); // 400
            //book var mı yok mu kontrol , güncellenecek kitabın bilgisini çekiyor 

            _manager.BookService.UpdateOneBook(id, bookDto, true);

            return NoContent(); //204


        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook(int id, Book book)
        {

            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();

        }



        [HttpPatch("{id:int}")]
        public IActionResult PartiallUpdateOneBook(int id, JsonPatchDocument<Book> bookPatch)
        {
            // check entitiy
            var entity = _manager.BookService.GetOneBookById(id, true);

            bookPatch.ApplyTo(entity);
            _manager.BookService.UpdateOneBook(id, new BookDtoForUpdate(entity.Id,entity.Title,entity.Price), true);
            return NoContent(); // 204


        }
    }
}
