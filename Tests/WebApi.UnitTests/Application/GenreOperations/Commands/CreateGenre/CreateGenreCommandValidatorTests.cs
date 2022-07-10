using FluentAssertions;
using TestsSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>{
        
        [Theory]
        [InlineData("Lord Of The Rings")]
        [InlineData("Lor")]
        [InlineData("")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name){
           CreateGenreCommand command = new CreateGenreCommand(null);  
           command.Model = new CreateGenreModel()
           {
                Name = ""
           };

           CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
           var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors(){
            CreateGenreCommand command = new CreateGenreCommand(null);  
            command.Model = new CreateGenreModel()
            {
                    Name = "Lord Of The Rings"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
           
    }
}