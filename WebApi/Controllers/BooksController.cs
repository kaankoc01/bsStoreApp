using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.Ef_Core;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // IOC register , resolve , dispose
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
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
                    .Book
                    .GetOneBookById(id, false);

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

                _manager.Book.CreateOneBook(book);
                _manager.Save();

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
                //book var mı yok mu kontrol , güncellenecek kitabın bilgisini çekiyor 
                var entity = _manager.Book.GetOneBookById(id, true);

                if (entity == null)
                    return NotFound(); //404
                                       // parametre olarak gönderilen id ile , update edilmek istenen id uyuşuyor mu check ID

                if (id != book.Id)
                    return BadRequest(); //400


                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();

                return Ok(book);
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
                var entity = _manager.Book.GetOneBookById(id ,false);
                if (entity == null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Book with id : {id} could not found."
                    });  // 404 id

                _manager.Book.DeleteOneBook(entity);
                _manager.Save();

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
                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity == null)
                    return NotFound(); //404

                bookPatch.ApplyTo(entity);
                _manager.Book.Update(entity);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
        }
    }
}
