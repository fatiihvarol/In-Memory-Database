using FluentValidation;

namespace WebApi.Application.JobOperations.Command.DeleteJobCommand;

public class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(command => command.JobId).GreaterThan(0);
    }
}