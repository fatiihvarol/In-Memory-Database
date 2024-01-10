using FluentAssertions;
using FluentValidation;
using WebApi.UserOperations.UpdateUser;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.UpdateUser
{
    public class UpdateUserCommandValidatorTest
    {
        [Theory]
        [InlineData(0, "ValidName", "25", 2)] // UserId is not greater than 0
        [InlineData(1, "ValidName", "", 2)]    // Age is empty
        [InlineData(1, "ValidName", "25", 0)]  // JobId is not greater than 0
        [InlineData(1, "ValidName", "25", 4)]  
        public void ShouldHaveErrorsForInvalidData(int userId, string name, string age, int jobId)
        {
            // Arrange
            var updateUserCommand = new UpdateUserCommand(null,null)
            {
                UserId = userId,
                UpdateUserModel = new UpdateUserModel
                {
                    Name = name,
                    Age = age,
                    JobId = jobId
                }
            };

            var validator = new UpdateUserCommandValidation();

            // Act
            var validationResult = validator.Validate(updateUserCommand);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void ShouldNotHaveErrorsForValidData()
        {
            // Arrange
            var updateUserCommand = new UpdateUserCommand(null,null)
            {
                UserId = 1,
                UpdateUserModel = new UpdateUserModel
                {
                    Name = "ValidName",
                    Age = "25",
                    JobId = 2
                }
            };

            var validator = new UpdateUserCommandValidation();

            // Act
            var validationResult = validator.Validate(updateUserCommand);

            // Assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }
    }
}
