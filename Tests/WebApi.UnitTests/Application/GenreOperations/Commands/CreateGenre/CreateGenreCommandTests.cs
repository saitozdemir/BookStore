using System;
using FluentAssertions;
using TestsSetup;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>{
        private BookStoreDbContext _context;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrange (Hazırlık)
            var genre = new Genre(){Name= "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){Name = genre.Name};
            
            //act (Çalıştırma)-assert (Doğrulama)
            FluentActions
                .Invoking (()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap ismi zaten mevcut");
                
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            CreateGenreCommand command = new CreateGenreCommand(_context);  
            CreateGenreModel model = new CreateGenreModel()
            {
                    Name = "Personal Growth"
            };

            //arrange
            command.Model = model;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assort
            var genre = _context.Genres.SingleOrDefault(book=>book.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}