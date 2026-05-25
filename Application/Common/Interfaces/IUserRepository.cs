using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<int> AddAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> UserExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
