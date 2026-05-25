namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ResourceName { get; }
        public object ResourceId { get; }
        public NotFoundException(string resourceName, object resourceId) : base($"{resourceName} con id {resourceId} no encontrado.")
        {
            ResourceName = resourceName;
            ResourceId = resourceId;
        }

    }
}
