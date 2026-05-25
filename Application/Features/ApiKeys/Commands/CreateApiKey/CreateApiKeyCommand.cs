using MediatR;

namespace Application.Features.ApiKeys.Commands.CreateApiKey
{
    public record CreateApiKeyCommand : IRequest<CreateApiKeyResponse>
    {
        public required int UserId {  get; init; } 
        public required string Name { get; init; }
        public  DateTime? ExpiresAt { get; init; }
    }
}
