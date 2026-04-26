using Domain.Enums;

namespace Domain.Entities
{
    public class ApiKey
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string KeyHash {  get; private set; }
        public string Prefix { get; private set; }
        public string ShortKey { get; private set; }
        public bool IsRevoked => RevokedAt != null;
        public bool IsExpired => ExpiredAt != null|| ( ExpiresAt.HasValue && ExpiresAt <= DateTime.UtcNow );
        public bool IsDeleted => DeletedAt != null;
        public DateTime? ExpiresAt { get; private set; }
        public DateTime? ExpiredAt { get; private set; }
        public ApiKeyStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public ApiKey(int userId, string name, string keyHash, string prefix, string shortKey, DateTime? expiresAt) { 
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException ("El nombre de la llave es obligatorio.", nameof(name));
            if (string.IsNullOrWhiteSpace(keyHash))
                throw new ArgumentException( "La key generada es obligatoria.", nameof(keyHash));
            if(string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("El prefijo de la llave es obligatorio.", nameof(prefix));
            if (string.IsNullOrWhiteSpace(shortKey))
                throw new ArgumentException("La ShortKey es obligatoria", "shortKey");
            if (userId <= 0)
                throw new ArgumentException("El id no puede ser menor o igual a 0", nameof(userId));

            UserId = userId;
            Name = name;
            KeyHash = keyHash;
            Prefix = prefix;
            ShortKey = shortKey;
            ExpiresAt = expiresAt;
            Status = ApiKeyStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void Revoke()
        {
            if (IsRevoked)
                throw new InvalidOperationException("Esta Key ya fue revocada.");
            
            Status = ApiKeyStatus.Revoked;
            RevokedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Expire()
        {
            if (Status != ApiKeyStatus.Active)
                throw new InvalidOperationException("No se puede expirar si la llave no esta activa.");
            if (!ExpiresAt.HasValue || ExpiresAt > DateTime.UtcNow)
                throw new InvalidOperationException("La key no tiene fecha de venciomiento o la fecha aun no ha vencido.");

            Status = ApiKeyStatus.Expired;
            ExpiredAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Delete()
        {
            if (IsExpired)
                throw new InvalidOperationException("No puedes borrar una Key expirada");
            if(IsRevoked)
                throw new InvalidOperationException("La Key ya fue revocada.");
            if (IsDeleted)
                throw new InvalidOperationException("La Key ya fue eliminada.");
           
            Status = ApiKeyStatus.Inactive;
            DeletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateExpiresAt(DateTime newExpiresAt)
        {
            if (Status != ApiKeyStatus.Active)
                throw new InvalidOperationException("No se puede modificar la fecha de expiracion si la llave no esta activa.");
            if (newExpiresAt < DateTime.UtcNow )
                throw new InvalidOperationException("Se nececita una fecha mayor a la actual.");

            ExpiresAt = newExpiresAt;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
