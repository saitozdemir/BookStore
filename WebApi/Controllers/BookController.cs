using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
            // var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
            // return bookList;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;

                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);

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
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                // if (!result.IsValid)
                //     foreach (var item in result.Errors)
                //         Console.WriteLine("Özellik: " + item.PropertyName + "-ErrorMessage: " + item.ErrorMessage);
                // else
                //command.Handle();
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

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
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