using Application.Common.ValueObject;

namespace Application.Common.Interfaces
{
    public interface IApiKeyGenerator
    {
        Task<GeneratedResult> GenerateAsync();
    }
}
