using Microsoft.EntityFrameworkCore;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Infrastructure.Data;

namespace WebAppCqrsMediator.Infrastructure.Reposytories
{
    public class NodeRepositoty : INodeRepositoty
    {
        private readonly NodeDbContext _context;

        public NodeRepositoty(NodeDbContext context)
        {
            _context = context;
        }

        public async Task<Node> CreateAsync(Node node)
        {
            await _context.Nodes.AddAsync(node);
            await _context.SaveChangesAsync();
            return node;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _context.Nodes.Where(i => i.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Node>> GetAllNodesAsync()
        {
            return await _context.Nodes.ToListAsync();
        }

        public async Task<Node> GetNodeByIdAsync(Guid id)
        {
            var node = await _context.Nodes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            return node != null ? node : await Task.FromResult(new Node());
        }

        public async Task<int> UpdateAsync(Guid id, Node node)
        {
            return await _context.Nodes.Where(i => i.Id == id).ExecuteUpdateAsync(s => s
            .SetProperty(m => m.Id, node.Id)
            .SetProperty(m => m.Name, node.Name)
            .SetProperty(m => m.ParentId, node.ParentId));
        }
    }
}
