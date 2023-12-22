using System.Data;
using AutoMapper;
using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.UserOperations.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Model.Age).NotNull();
            RuleFor(x => x.Model.Name).MinimumLength(3).MaximumLength(10);
            RuleFor(x => x.Model.JobId).ExclusiveBetween(0, 4);
        }
    }

}
