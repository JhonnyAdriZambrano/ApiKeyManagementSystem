using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApiKeyRepository
    {
        Task<int> AddAsync(ApiKey apiKey, CancellationToken cancellationToken = default);
    }
}
