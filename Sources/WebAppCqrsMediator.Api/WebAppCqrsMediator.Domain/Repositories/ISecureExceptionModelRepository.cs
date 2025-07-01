using WebAppCqrsMediator.Domain.Entities;

namespace WebAppCqrsMediator.Domain.Repositories
{
    public interface ISecureExceptionModelRepository
    {
        Task<List<SecureExceptionModel>> GetAllSecureExceptionModelsAsync();
    }
}
