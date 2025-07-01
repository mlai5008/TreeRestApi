using AutoMapper;
using MediatR;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Domain.Repositories;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.UpdateNode
{
    public class UpdateNodeCommandHandler : IRequestHandler<UpdateNodeCommand, int>
    {
        private readonly INodeRepositoty _nodeRepositoty;
        private readonly IMapper _mapper;

        public UpdateNodeCommandHandler(INodeRepositoty nodeRepositoty, IMapper mapper)
        {
            _nodeRepositoty = nodeRepositoty;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            var nodeEntity = new Node { Id = request.Id, Name = request.Name, ParentId = request.ParentId };
            var nodeId = await _nodeRepositoty.UpdateAsync(request.Id, nodeEntity);
            return nodeId;
        }
    }
}
