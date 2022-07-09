using System;
using FluentAssertions;
using TestsSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>{
        
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){
           CreateBookCommand command = new CreateBookCommand(null, null);  
           command.Model = new CreateBookModel()
           {
                Title = "",
                PageCount = 0,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 0
           };

           CreateBookCommandValidator validator = new CreateBookCommandValidator();
           var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors(){
            CreateBookCommand command = new CreateBookCommand(null, null);  
            command.Model = new CreateBookModel()
            {
                    Title = "Lord Of The Rings",
                    PageCount = 100,
                    PublishDate = DateTime.Now.Date,
                    GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
           
    }
}