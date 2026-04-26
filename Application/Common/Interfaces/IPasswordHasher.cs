namespace Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashAsync(string password);
        Task<bool> VerifyHashAsync(string password, string hash);
    }
}
