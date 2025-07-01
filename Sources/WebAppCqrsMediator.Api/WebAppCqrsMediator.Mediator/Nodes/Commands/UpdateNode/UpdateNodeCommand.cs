using MediatR;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.UpdateNode
{
    public class UpdateNodeCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid ParentId { get; set; }
    }
}
