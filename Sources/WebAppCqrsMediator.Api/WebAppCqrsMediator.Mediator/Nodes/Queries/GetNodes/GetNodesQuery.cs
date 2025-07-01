using MediatR;
using WebAppCqrsMediator.Mediator.Common.ModelsDto;

namespace WebAppCqrsMediator.Mediator.Nodes.Queries.GetNodes
{
    public record GetNodesQuery : IRequest<List<NodeDto>>
    {
    }
}
