using MediatR;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.Nodes.Commands.CreateNode
{
    public class CreateNodeCommand : IRequest<NodeDto>
    {        
        public string? Name { get; set; }
        public Guid ParentId { get; set; }
    }
}
