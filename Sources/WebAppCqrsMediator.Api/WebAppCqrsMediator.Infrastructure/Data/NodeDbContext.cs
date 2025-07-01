using Microsoft.EntityFrameworkCore;
using WebAppCqrsMediator.Domain.Entities;

namespace WebAppCqrsMediator.Infrastructure.Data
{
    public class NodeDbContext : DbContext
    {
        public NodeDbContext() {}

        public NodeDbContext(DbContextOptions<NodeDbContext> options) : base(options) { }

        #region Property
        public DbSet<Node> Nodes { get; set; } = default!;
        public DbSet<SecureExceptionModel> SecureExceptionModels { get; set; } = default!;
        #endregion
    }
}
