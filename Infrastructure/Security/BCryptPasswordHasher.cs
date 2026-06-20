using Application.Common.Interfaces;
using Infrastructure.Security.SecurityGlobals;

namespace Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public async Task<string> HashAsync(string password, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacia.", nameof(password));

            return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password, SecurityVariable.WorkFactor), cancellationToken);
        }

        public async Task<bool> VerifyHashAsync(string password, string hashedPassword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacia.", nameof(password));
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("El hash ingresado no puede ser vacio.", nameof(hashedPassword));

            return await Task.Run(() =>BCrypt.Net.BCrypt.Verify(password, hashedPassword), cancellationToken);
        }
    }
}
