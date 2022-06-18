using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail{
    public class GetBookDetailQuery{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public void Handle(){
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            
        }
    }
    public class GenreViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
    }
}