using FluentValidation;

namespace WebApi.Application.JobOperations.Command.UpdateJobCommand;

public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
    }
}