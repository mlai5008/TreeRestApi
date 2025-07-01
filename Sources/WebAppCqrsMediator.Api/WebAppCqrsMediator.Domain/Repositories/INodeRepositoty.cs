using WebAppCqrsMediator.Domain.Entities;

namespace WebAppCqrsMediator.Domain.Repositories
{
    public interface INodeRepositoty
    {
        Task<List<Node>> GetAllNodesAsync();
        Task<Node> GetNodeByIdAsync(Guid id);
        Task<Node> CreateAsync(Node node);
        Task<int> UpdateAsync(Guid id, Node node);
        Task<int> DeleteAsync(Guid id);
    }
}
