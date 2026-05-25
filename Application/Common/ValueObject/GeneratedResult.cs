namespace Application.Common.ValueObjects
{
    public record GeneratedResult
    {
        public required string RawKey { get; init; }
        public required string KeyHash {  get; init; }
        public required string Prefix { get; init; }
        public required string ShortKey { get; init; }
    }
}
