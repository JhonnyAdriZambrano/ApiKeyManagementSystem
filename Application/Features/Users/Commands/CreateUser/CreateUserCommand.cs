using MediatR;

namespace Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<int>
    {
        public required string Email {  get; init; }
        public required string Password { get; init; }
    }
}
