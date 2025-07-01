using AutoMapper;
using MediatR;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.Nodes.Queries.GetNodes
{
    public class GetNodesQueryHandler : IRequestHandler<GetNodesQuery, List<NodeDto>>
    {
        private readonly INodeRepositoty _nodeRepositoty;
        private readonly IMapper _mapper;

        public GetNodesQueryHandler(INodeRepositoty nodeRepositoty, IMapper mapper)
        {
            _nodeRepositoty = nodeRepositoty;
            _mapper = mapper;
        }

        public async Task<List<NodeDto>> Handle(GetNodesQuery request, CancellationToken cancellationToken)
        {
            var nodes = await _nodeRepositoty.GetAllNodesAsync();
            var nodeDtos = _mapper.Map<List<NodeDto>>(nodes);
            return nodeDtos;
        }
    }
}
