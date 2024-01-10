using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.UserOperations.UpdateUser;
using WebApiUnitTest.TestSetup;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.UpdateUser
{
    public class UpdateUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void ShouldUpdateUserProperties()
        {
            // Arrange
            // Assuming you have a user with Id = 1 in your test database
            var userId = 1;
            var updateUserModel = new UpdateUserModel
            {
                Name = "UpdatedName",
                Age = "25",
                JobId = 2
            };

            var command = new UpdateUserCommand(_context, _mapper)
            {
                UserId = userId,
                UpdateUserModel = updateUserModel
            };

            // Act
            command.Handle();

            // Assert
            var updatedUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            updatedUser.Should().NotBeNull();
            updatedUser.Name.Should().Be(updateUserModel.Name);
            updatedUser.Age.Should().Be(updateUserModel.Age);
            updatedUser.JobId.Should().Be(updateUserModel.JobId);
        }

        [Fact]
        public void ShouldThrowExceptionForNonexistentUser()
        {
            // Arrange
            // Assuming you don't have a user with Id = 999 in your test database
            var userId = 999;
            var updateUserModel = new UpdateUserModel
            {
                Name = "UpdatedName",
                Age = "25",
                JobId = 2
            };

            var command = new UpdateUserCommand(_context, _mapper)
            {
                UserId = userId,
                UpdateUserModel = updateUserModel
            };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<Exception>()
                .WithMessage("user does not exist to update");
        }
    }
}
