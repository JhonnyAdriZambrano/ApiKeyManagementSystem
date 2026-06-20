using Application.Common.Interfaces;
using Application.Common.ValueObject;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Generator
{
    public class KeyGenerator : IApiKeyGenerator
    {
        private const int PrefixLength = 8;
        private const int ShortKeyLength = 6;
        public Task<GeneratedResult> GenerateAsync()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(32);
            string rawKey = Base64Url.EncodeToString(bytes);

            byte[] rawKeyBytes = Encoding.UTF8.GetBytes(rawKey);
            byte[] hashByte = SHA256.HashData(rawKeyBytes);

            string keyHash = Convert.ToHexString(hashByte).ToLowerInvariant();

            string prefix = rawKey[..PrefixLength];
            string shortKey = rawKey[^ShortKeyLength..];

            GeneratedResult result = new() { KeyHash = keyHash, Prefix = prefix, ShortKey = shortKey , RawKey = rawKey};

            return Task.FromResult(result);
        }
    }
}
