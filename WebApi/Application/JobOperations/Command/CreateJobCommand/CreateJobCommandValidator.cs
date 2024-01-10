using FluentValidation;

namespace WebApi.Application.JobOperations.Command.CreateJobCommand;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
    }
}