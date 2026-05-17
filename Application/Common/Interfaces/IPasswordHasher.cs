namespace Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashAsync(string password, CancellationToken cancellationToken = default);
        Task<bool> VerifyHashAsync(string password, string hash, CancellationToken cancellationToken = default);
    }
}
