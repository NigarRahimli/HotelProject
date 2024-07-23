

namespace Project.Infrastructure.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName, string entityValue)
            : base($"A {entityName} for {entityValue} already exists.")
        {
        }
    }
}
