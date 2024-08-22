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
        public IActionResult CreateOneBook(BookDtoForInsertion bookDto)
        {

            if (bookDto == null)
                return BadRequest(); // 400

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var book = _manager.BookService.CreateOneBook(bookDto);

            return StatusCode(201, book); // createdAtRoute() 


        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook(int id, BookDtoForUpdate bookDto)
        {
            if (bookDto == null)
                return BadRequest(); // 400
            //book var mı yok mu kontrol , güncellenecek kitabın bilgisini çekiyor 

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); // 422

            _manager.BookService.UpdateOneBook(id, bookDto, false);

            return NoContent(); //204


        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook(int id, Book book)
        {

            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();

        }



        [HttpPatch("{id:int}")]
        public IActionResult PartiallUpdateOneBook(int id, JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest(); // 400


            // check entitiy
            var result = _manager.BookService.GetOneBookForPatch(id, false);


            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate, result.book);
           
            return NoContent(); // 204


        }
    }
}
