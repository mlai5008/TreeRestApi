using AutoMapper;
using MediatR;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.SecureExceptionModels.Queries.GetSecureExceptionModels
{
    public class GetSecureExceptionModelsQueryHandler : IRequestHandler<GetSecureExceptionModelsQuery, List<SecureExceptionModelDto>>
    {
        private readonly ISecureExceptionModelRepository _secureExceptionModelRepositoty;
        private readonly IMapper _mapper;

        public GetSecureExceptionModelsQueryHandler(ISecureExceptionModelRepository secureExceptionModelRepositoty, IMapper mapper)
        {
            _secureExceptionModelRepositoty = secureExceptionModelRepositoty;
            _mapper = mapper;
        }

        public async Task<List<SecureExceptionModelDto>> Handle(GetSecureExceptionModelsQuery request, CancellationToken cancellationToken)
        {
            var secureExceptionModels = await _secureExceptionModelRepositoty.GetAllSecureExceptionModelsAsync();
            var secureExceptionModelDtos = _mapper.Map<List<SecureExceptionModelDto>>(secureExceptionModels);
            return secureExceptionModelDtos;
        }
    }
}
