using AutoMapper;
using System;
using FluentAssertions;
using TestsSetup;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>{
        private BookStoreDbContext _context;
        private IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrange (Hazırlık)
            var author = new Author(){Name= "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", Surname="Handler", BirthDate = new System.DateTime(1990,01,10)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel(){Name = author.Name};
            
            //act (Çalıştırma)-assert (Doğrulama)
            FluentActions
                .Invoking (()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
                
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);  
            CreateAuthorModel model = new CreateAuthorModel()
            {
                    Name = "Hobbit",
                    Surname = "Handler",
                    BirthDate = DateTime.Now.Date.AddYears(-10)
            };

            //arrange
            command.Model = model;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assort
            var author = _context.Authors.SingleOrDefault(book=>book.Name == model.Name);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}