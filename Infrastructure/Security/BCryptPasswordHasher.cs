using Application.Common.Interfaces;
using Infrastructure.Security.SecurityGlobals;

namespace Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public Task<string> HashAsync(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacia.", nameof(password));

            return Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password, SecurityVariable.WorkFactor));
        }

        public Task<bool> VerifyHashAsync(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacia.", nameof(password));
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("El hash ingresado no puede ser vacion.", nameof(hashedPassword));

            return Task.Run(() => BCrypt.Net.BCrypt.Verify(password, hashedPassword));
        }
    }
}
