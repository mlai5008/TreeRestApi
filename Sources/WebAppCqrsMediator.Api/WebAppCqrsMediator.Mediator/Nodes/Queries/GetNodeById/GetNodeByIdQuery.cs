using MediatR;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.Nodes.Queries.GetNodeById
{
    public class GetNodeByIdQuery : IRequest<NodeDto>
    {
        public Guid NodeId { get; set; }
    }
}
