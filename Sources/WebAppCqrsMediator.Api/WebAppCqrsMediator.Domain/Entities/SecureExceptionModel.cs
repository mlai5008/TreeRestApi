using System.ComponentModel.DataAnnotations;

namespace WebAppCqrsMediator.Domain.Entities
{
    public class SecureExceptionModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
    }
}
