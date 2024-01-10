using FluentAssertions;
using WebApi.UserOperations.DeleteUserCommand;
using WebApi.UserOperations.GetUserDetail;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.DeleteUser
{
    public class GetUserDetailQueryValidatorTest
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
            GetUserDetailQuery command = new GetUserDetailQuery(null,null);
            command.UserId = userId;

            //act
            GetUserDetailQueryValidation validator = new GetUserDetailQueryValidation();
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
            GetUserDetailQuery command = new GetUserDetailQuery(null,null);
            command.UserId = userId;

            //act
            GetUserDetailQueryValidation validator = new GetUserDetailQueryValidation();
            var result= validator.Validate(command);
            
            //assert
            result.Errors.Should().BeEmpty();
        }
    }
}