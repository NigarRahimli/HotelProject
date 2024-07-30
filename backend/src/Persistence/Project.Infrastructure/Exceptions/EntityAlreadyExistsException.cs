

namespace Project.Infrastructure.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName, string entityValue)
            : base($"{entityName} for (with) {entityValue} already exists.")
        {
        }
    }
}
