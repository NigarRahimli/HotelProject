

namespace Project.Infrastructure.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName, string entityValue)
            : base($"A {entityName} with the value '{entityValue}' already exists.")
        {
        }
    }
}
