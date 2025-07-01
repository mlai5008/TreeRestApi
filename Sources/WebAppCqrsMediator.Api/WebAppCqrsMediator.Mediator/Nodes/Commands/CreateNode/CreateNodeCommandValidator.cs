 using FluentValidation;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.CreateNode
{
    public class CreateNodeCommandValidator : AbstractValidator<CreateNodeCommand>
    {
        public CreateNodeCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must has more 2 characters");
        }
    }
}
