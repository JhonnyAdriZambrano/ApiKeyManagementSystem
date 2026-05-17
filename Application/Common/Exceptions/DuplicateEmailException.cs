namespace Application.Common.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public string Email { get; }
        public DuplicateEmailException(string email) : base($"No se puede realizar la operacion. El Email: {email} ya se encuentra registrado.") { Email = email; }        
    }
}
