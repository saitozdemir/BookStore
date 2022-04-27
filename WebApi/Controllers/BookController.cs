using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
            // var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
            // return bookList;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            try
            {
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok(result);

            // var book = _context.Books.Where(book=>book.Id == id).SingleOrDefault();
            // return book;
        }
        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = _context.Books.Where(book=>book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context);
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {                
                return BadRequest (ex.Message);
            }
            
            return Ok();           

            // var book = _context.Books.SingleOrDefault(x=>x.Title == newBook.Title);
            // if (book is not null)
            //     return BadRequest();
            
            // _context.Books.Add(newBook);
            // _context.SaveChanges();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();

            // var book = _context.Books.SingleOrDefault(x=>x.Id == id);
            // if (book is null)
            //     return BadRequest();
            // book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            // book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            // book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            // book.Title = updateBook.Title != default ? updateBook.Title : book.Title;

            // _context.SaveChanges();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
            return Ok();
        }
    }    
}