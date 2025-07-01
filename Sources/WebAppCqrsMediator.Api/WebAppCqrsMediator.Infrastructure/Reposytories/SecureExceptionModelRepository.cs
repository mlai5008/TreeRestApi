using Microsoft.EntityFrameworkCore;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Infrastructure.Data;

namespace WebAppCqrsMediator.Infrastructure.Reposytories
{
    public class SecureExceptionModelRepository : ISecureExceptionModelRepository
    {
        private readonly NodeDbContext _context;

        public SecureExceptionModelRepository(NodeDbContext context)
        {
            _context = context;
        }

        public async Task<List<SecureExceptionModel>> GetAllSecureExceptionModelsAsync()
        {
            return await _context.SecureExceptionModels.ToListAsync();
        }
    }
}
