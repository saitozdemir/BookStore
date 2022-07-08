using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetGenres(){
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }
        
        [HttpGet("id")]
        public ActionResult GetGenreDetailQuery(int id){
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var obj = query.Handle();
            return Ok(obj);
        }
    }
    
}