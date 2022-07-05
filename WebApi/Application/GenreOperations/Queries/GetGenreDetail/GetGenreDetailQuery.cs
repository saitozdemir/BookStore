using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail{
    public class GetGenreDetailQuery{
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public GetGenreDetailQuery Handle(){
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı.");

            return _mapper.Map<GetGenreDetailQuery>(genre);
            
        }
    }
    public class GenreViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
    }
}