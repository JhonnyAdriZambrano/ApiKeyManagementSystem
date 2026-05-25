using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ApiKeys.Commands.CreateApiKey
{
    public class CreateApiKeyCommandHandler : IRequestHandler<CreateApiKeyCommand, CreateApiKeyResponse>
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly IApiKeyGenerator _apiKeyGenerator;
        private readonly IUserRepository _userRepository;

        public CreateApiKeyCommandHandler(IApiKeyRepository apiKeyRepository, IApiKeyGenerator apiKeyGenerator, IUserRepository userRepository)
        {
            _apiKeyRepository = apiKeyRepository;
            _apiKeyGenerator = apiKeyGenerator;
            _userRepository = userRepository;
        }

        public async Task<CreateApiKeyResponse> Handle(CreateApiKeyCommand request, CancellationToken cancellationToken)
        {
            var isExistUser = await _userRepository.UserExistsAsync(request.UserId, cancellationToken);
            if (!isExistUser)
                throw new NotFoundException(nameof(User), request.UserId);

            var generatedResult = await _apiKeyGenerator.GenerateAsync();

            ApiKey apiKey = new (request.UserId, request.Name, generatedResult.KeyHash, generatedResult.Prefix, generatedResult.ShortKey,request.ExpiresAt);

            var id = await _apiKeyRepository.AddAsync(apiKey, cancellationToken);

            return new CreateApiKeyResponse { Id = id, RawKey = generatedResult.RawKey };
        }
    }
}
