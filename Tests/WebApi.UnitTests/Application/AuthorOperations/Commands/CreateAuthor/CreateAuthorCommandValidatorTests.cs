using System;
using FluentAssertions;
using TestsSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>{
        
        [Theory]
        [InlineData("Lord Of The Rings", "")]
        [InlineData("", "Handler")]
        [InlineData("", "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){
           CreateAuthorCommand command = new CreateAuthorCommand(null, null);  
           command.Model = new CreateAuthorModel()
           {
                Name = "",
                Surname = "",
                BirthDate = DateTime.Now.Date.AddYears(-1)
           };

           CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
           var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors(){
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);  
            command.Model = new CreateAuthorModel()
            {
                    Name = "Lord Of The Rings",
                    Surname = "Handler",
                    BirthDate = DateTime.Now.Date
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
           
    }
}