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
            try
            {
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw; new Exception(ex.Message);
            }

        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks(int id)
        {
            try
            {
                // lınq , single or defualt ya null döner ya bular döner
                var book = _manager
                    .BookService.GetOneBookById(id, false);

                if (book == null)
                    return NotFound(); //404

                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult CreateOneBook(Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest(); // 400

                _manager.BookService.CreateOneBook(book);

                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook(int id, Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest(); // 400
                                         //book var mı yok mu kontrol , güncellenecek kitabın bilgisini çekiyor 

                _manager.BookService.UpdateOneBook(id, book, true);

                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook(int id, Book book)
        {
            try
            {
                _manager.BookService.DeleteOneBook(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }



        [HttpPatch("{id:int}")]
        public IActionResult PartiallUpdateOneBook(int id, JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                // check entitiy
                var entity = _manager.BookService.GetOneBookById(id, true);
                if (entity == null)
                    return NotFound(); //404

                bookPatch.ApplyTo(entity);
                _manager.BookService.UpdateOneBook(id, entity, true);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
