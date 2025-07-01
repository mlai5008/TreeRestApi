using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Mediator.Common.Mappings;

namespace WebAppCqrsMediator.Mediator.Common.ModelsDto
{
    public class NodeDto : IMapFrom<Node>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid ParentId { get; set; }
    }
}
