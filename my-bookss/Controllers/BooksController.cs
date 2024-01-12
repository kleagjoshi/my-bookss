using Microsoft.AspNetCore.Mvc;
using my_bookss.Data.Services;
using my_bookss.Data.ViewModels;

namespace my_bookss.Controllers

{
    [Route("api/[controller]")]
    [ApiController] //api controller decorater - defines if its a api or mvc controller
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        //create the endpoint
        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }

        //update book by id
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM bookVM)
        {
            var updatedBook = _booksService.UpdateBookById(id, bookVM);
            return Ok(updatedBook);
        }


        //get all books endpoint
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{id}")] //{ } what we get from the http request , it has to match with the parameter below
        public IActionResult GetBookById(int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        //delete a book by id
        [HttpDelete("delete-a-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }

    }
}
