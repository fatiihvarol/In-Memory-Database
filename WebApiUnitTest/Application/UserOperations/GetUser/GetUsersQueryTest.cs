using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.UserOperations.GetUserQuery;
using WebApiUnitTest.TestSetup;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.GetUser
{
    public class GetUsersQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void ShouldReturnMappedUserViewModels()
        {
            // Arrange
            var query = new GetUserQuery(_context, _mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<UserViewModel>>();

            // Assuming you have some sample users in your test database
            var expectedUsersCount = _context.Users.Count();
            result.Should().HaveCount(expectedUsersCount);
        }
    }
}