namespace Application.Features.ApiKeys.Commands.CreateApiKey
{
    public record CreateApiKeyResponse 
    {
        public int Id { get; init; }
        public required string RawKey { get; init; }
    }
}
