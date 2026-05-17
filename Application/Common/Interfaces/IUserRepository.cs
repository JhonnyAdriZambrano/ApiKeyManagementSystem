using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
        public Task<int> AddAsync(User user, CancellationToken cancellationToken = default);
    }
}
