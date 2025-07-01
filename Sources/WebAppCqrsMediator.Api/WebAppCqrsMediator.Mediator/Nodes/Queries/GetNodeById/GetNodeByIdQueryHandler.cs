using AutoMapper;
using MediatR;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.Nodes.Queries.GetNodeById
{
    public class GetNodeByIdQueryHandler : IRequestHandler<GetNodeByIdQuery, NodeDto>
    {
        private readonly INodeRepositoty _nodeRepositoty;
        private readonly IMapper _mapper;

        public GetNodeByIdQueryHandler(INodeRepositoty nodeRepositoty, IMapper mapper)
        {
            _nodeRepositoty = nodeRepositoty;
            _mapper = mapper;
        }

        public async Task<NodeDto> Handle(GetNodeByIdQuery request, CancellationToken cancellationToken)
        {
            var node = await _nodeRepositoty.GetNodeByIdAsync(request.NodeId);
            var nodeDto = _mapper.Map<NodeDto>(node);
            return nodeDto;
        }
    }
}
