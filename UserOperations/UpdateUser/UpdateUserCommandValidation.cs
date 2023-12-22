using System.Data;
using FluentValidation;

namespace WebApi.UserOperations.UpdateUser
{
    public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.UpdateUserModel!.Name).MaximumLength(100).MinimumLength(0);
            RuleFor(x => x.UpdateUserModel!.Age).MinimumLength(1).MaximumLength(3);
            RuleFor(x => x.UpdateUserModel!.JobId).GreaterThan(0).LessThan(4);
        }
    }
}
