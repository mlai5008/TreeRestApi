using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Mediator.Common.Mappings;

namespace WebAppCqrsMediator.Mediator.Common.ModelsDto
{
    public class SecureExceptionModelDto : IMapFrom<SecureExceptionModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
    }
}
