using FluentValidation;
using MediatR;

namespace WebAppCqrsMediator.Mediator.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, Tresponse> : IPipelineBehavior<TRequest, Tresponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationRequests = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var fails = validationRequests.Where(v => v.Errors.Any()).SelectMany(s => s.Errors).ToList();

                if (fails.Any()) { throw new ValidationException(fails); }
            }

            return await next();
        }
    }
}
