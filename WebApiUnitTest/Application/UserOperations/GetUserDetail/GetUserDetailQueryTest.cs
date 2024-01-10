using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.UserOperations.GetUserDetail;
using WebApiUnitTest.TestSetup;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.GetUserDetail
{
    public class GetUserDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void ShouldReturnUserDetailModelView()
        {
            // Arrange
            // Assuming you have a user with Id = 1 in your test database
            var userId = 1;
            var query = new GetUserDetailQuery(_context, _mapper) { UserId = userId };

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailModelView>();
            result.Name.Should().NotBeNullOrEmpty();
            result.Age.Should().NotBeNullOrEmpty();
            result.Job.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ShouldThrowExceptionForNonexistentUser()
        {
            // Arrange
            // Assuming you don't have a user with Id = 999 in your test database
            var userId = 999;
            var query = new GetUserDetailQuery(_context, _mapper) { UserId = userId };

            // Act & Assert
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<Exception>()
                .WithMessage("User not exist");
        }
    }
}