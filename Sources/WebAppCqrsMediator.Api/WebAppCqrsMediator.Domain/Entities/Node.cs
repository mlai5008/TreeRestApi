using System.ComponentModel.DataAnnotations;

namespace WebAppCqrsMediator.Domain.Entities
{
    public class Node
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
    }
}
