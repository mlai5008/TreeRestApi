using AutoMapper;
using MediatR;
using WebAppCqrsMediator.Domain.Repositories;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.DeleteNode
{
    public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, int>
    {
        private readonly INodeRepositoty _nodeRepositoty;        

        public DeleteNodeCommandHandler(INodeRepositoty nodeRepositoty)
        {
            _nodeRepositoty = nodeRepositoty;            
        }

        public async Task<int> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            return await _nodeRepositoty.DeleteAsync(request.Id);
        }
    }
}
