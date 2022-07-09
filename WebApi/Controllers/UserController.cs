using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateUserCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, IMapper mapper, IBookStoreDbContext context)
        {
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }
    }
}
