using AutoMapper;
using FluentAssertions;
using WebApi.DbOperations;

using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.DeleteUserCommand;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.DeleteUser
{
    public class DeleteUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public DeleteUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidUserGiven_UserShouldBeDeleted()
        {
            // Arrange
            var createUserCommand = new CreateUserCommand(_context, _mapper);
            var createUserModel = new CreateUserModel() { Name = "John", Age = "30", JobId = 2 };
            createUserCommand.Model = createUserModel;
            createUserCommand.Handle();  // Create a user to be deleted

            var deleteUserCommand = new DeleteUserCommand(_context);
            var userToDelete = _context.Users.FirstOrDefault(u => u.Name == createUserModel.Name);

            // Act
            deleteUserCommand.UserId = userToDelete.Id;
            deleteUserCommand.Handle();

            // Assert
            var deletedUser = _context.Users.FirstOrDefault(u => u.Name == createUserModel.Name);
            deletedUser.Should().BeNull();
        }
    }
}