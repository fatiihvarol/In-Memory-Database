
using FluentAssertions;
using FluentValidation;
using WebApi.UserOperations.CreateUser;
using WebApiUnitTest.TestSetup;
using Xunit;

namespace WebApiUnitTest.Application.UserOperations.CreateUser;

public class CreateUserCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    

    [Theory]
    [InlineData("fa","1",1)]
    [InlineData("fat","",1)]
    [InlineData("fat","1",4)]
    [InlineData("namenamenamenamename","1234",6)]
    [InlineData("fat","1",99)]
    public void WhenInvalidDataGiven_Error_ShouldBeThrow(string name,string age,int jobId)
    {
        CreateUserCommand command = new CreateUserCommand(null, null);
        command.Model = new CreateUserModel()
        {
            Name = name,
            Age = age,
            JobId = jobId
            
        };
        
        //act

        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

   


        
}