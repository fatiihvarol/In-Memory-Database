using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.UserOperations.CreateUser;
using WebApiUnitTest.TestSetup;
using Xunit;
using User = WebApi.Entities.User;

namespace WebApiUnitTest.Application.UserOperations.CreateUser;

public class CreateUserCommandTest:IClassFixture<CommonTestFixture>
{
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTest(CommonTestFixture testFixture)
        {
                _context = testFixture.Context;
                _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistUserGiven_ArgumentNullException_ShouldBeReturn()
        {
                // Arrange
                var user = new User() { Name = "fatih", Age = "21", JobId = 1 };
                _context.Add(user);
                _context.SaveChanges();

                CreateUserCommand command = new CreateUserCommand(_context, _mapper);
                command.Model = new CreateUserModel() { Name = user.Name };

                // Act & Assert
                FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidDataException>()
                        .And.Message.Should().Contain("User with the same properties already exists");
        }
        
        [Fact]
        public void WhenGiveValidData_Book_ShouldBeCreated()
        {
                // Arrange
                CreateUserCommand command = new CreateUserCommand(_context, _mapper);
                CreateUserModel model = new CreateUserModel() { Name = "Dudu", Age = "23", JobId = 1 };
                command.Model = model;

                // Act
                command.Handle();

                // Assert
                var user = _context.Users.FirstOrDefault(x => x.Name == model.Name);
                user.Should().NotBeNull();
                user.Age.Should().Be(model.Age);
                user.JobId.Should().Be(model.JobId);
                user.Name.Should().Be(model.Name);
        }



        
}