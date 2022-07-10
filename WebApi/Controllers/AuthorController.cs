using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public AuthorController(IConfiguration configuration, IMapper mapper, IBookStoreDbContext context)
        {
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateAuthorModel newUser)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }
    }
}
