using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> EmailExistsAsync(string email);
        public Task<int> AddAsync(User user);
    }
}
