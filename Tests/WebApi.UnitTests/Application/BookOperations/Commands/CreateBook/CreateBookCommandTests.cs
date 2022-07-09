using AutoMapper;
using System;
using FluentAssertions;
using TestsSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using System.Linq;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>{
        private BookStoreDbContext _context;
        private IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrange (Hazırlık)
            var book = new Book(){Title= "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount=100, PublishDate = new System.DateTime(1990,01,10), GenreId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title};
            
            //act (Çalıştırma)-assert (Doğrulama)
            FluentActions
                .Invoking (()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
                
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);  
            CreateBookModel model = new CreateBookModel()
            {
                    Title = "Hobbit",
                    PageCount = 100,
                    PublishDate = DateTime.Now.Date.AddYears(-10),
                    GenreId = 1
            };

            //arrange
            command.Model = model;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assort
            var book = _context.Books.SingleOrDefault(book=>book.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}