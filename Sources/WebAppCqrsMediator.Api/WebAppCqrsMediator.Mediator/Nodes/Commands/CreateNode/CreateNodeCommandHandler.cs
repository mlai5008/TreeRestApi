using AutoMapper;
using MediatR;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.CreateNode
{
    public class CreateNodeCommandHandler : IRequestHandler<CreateNodeCommand, NodeDto>
    {
        private readonly INodeRepositoty _nodeRepositoty;
        private readonly IMapper _mapper;

        public CreateNodeCommandHandler(INodeRepositoty nodeRepositoty, IMapper mapper)
        {
            _nodeRepositoty = nodeRepositoty;
            _mapper = mapper;
        }

        public async Task<NodeDto> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
        {
            var nodeEntity = new Node { Name = request.Name, ParentId = request.ParentId };
            var node = await _nodeRepositoty.CreateAsync(nodeEntity);            
            var nodeDto = _mapper.Map<NodeDto>(node);
            return nodeDto;
        }
    }
}
