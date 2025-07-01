using MediatR;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.DeleteNode
{
    public class DeleteNodeCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
}
