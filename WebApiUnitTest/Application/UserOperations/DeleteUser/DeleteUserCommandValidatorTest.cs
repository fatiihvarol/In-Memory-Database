using FluentAssertions;
using WebApi.UserOperations.DeleteUserCommand;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.DeleteUser
{
    public class DeleteUserCommandValidatorTest
    {
        [Theory]
        [InlineData(-12)]
        [InlineData(-2131)]
        [InlineData(-123)]
        [InlineData(-11111)]
        [InlineData(-12321)]
        public void ShouldHaveErrorWhenUserIdIsNotGreaterThanZero(int userId)
        {
            //arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = userId;

            //act
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
           var result= validator.Validate(command);
            
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void ShouldNotHaveErrorWhenUserIdIsGreaterThanZero(int userId)
        {
            //arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = userId;

            //act
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            var result= validator.Validate(command);
            
            //assert
            result.Errors.Should().BeEmpty();
        }
    }
}