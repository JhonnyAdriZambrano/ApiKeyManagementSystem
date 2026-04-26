using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserStatus Status { get; private set; }
        public bool MfaEnabled { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? InactivedAt { get; private set; }
        public DateTime? LockedAt { get; private set; }
        public DateTime? UnlockedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        
        public User(string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("La contraseña es obligatoria.", nameof(passwordHash));

            Email = email;
            PasswordHash = passwordHash;
            Status = UserStatus.Active;
            CreatedAt = DateTime.UtcNow;
            MfaEnabled = false;
        }

        #region metodos relacionado al estado del usuario
        public void Block()
        {
            if (Status != UserStatus.Active)
                throw new InvalidOperationException("No se puede bloquear un usuario que no este activo");
            
            Status = UserStatus.Locked;
            LockedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unlock()
        {
            if (Status != UserStatus.Locked)
                throw new InvalidOperationException("No se puede desbloquear un usuario que no se encuentre bloqueado.");

            Status = UserStatus.Active;
            UnlockedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Inactivate()
        {
            if(Status != UserStatus.Active)
                throw new InvalidOperationException("Solo se puede inactivar un usuario Activo.");
            
            Status = UserStatus.Inactive;
            InactivedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            if (DeletedAt.HasValue)
                throw new InvalidOperationException("No se puede eliminar un usuario que ya ha sido eliminado.");

            Status = UserStatus.Removed;
            DeletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

        }
        #endregion



        #region metodos relacionados a las actulizaciones de caracteristicas del usuairo
        public void UpdateMfaEnabled(bool newMfaEnabled)
        {
            if (Status != UserStatus.Active)
                throw new InvalidOperationException("No se puede modificar el multifactor si el usuario no se encuentra activo.");
            
            MfaEnabled = newMfaEnabled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePasswordHash(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("La nueva contraseña no debe estar vacia.", nameof(newPasswordHash));
            
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }
        #endregion
    }
}
