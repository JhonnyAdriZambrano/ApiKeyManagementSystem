using Application.Common.ValueObjects;

namespace Application.Common.Interfaces
{
    public interface IApiKeyGenerator
    {
        Task<GeneratedResult> GenerateAsync();
    }
}
