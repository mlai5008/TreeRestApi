using MediatR;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.SecureExceptionModels.Queries.GetSecureExceptionModels
{
    public class GetSecureExceptionModelsQuery : IRequest<List<SecureExceptionModelDto>>
    {
    }
}
