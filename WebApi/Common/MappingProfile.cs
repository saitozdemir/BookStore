using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<CreateBookModel, Book>();
           CreateMap<Book, BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
           CreateMap<Book, BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
           CreateMap<Genre, GenreViewModel>();
        }
    }
}