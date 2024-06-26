
namespace Project.Infrastructure.Exceptions
{
    public class EntityCreationFailedException : Exception
    {
        public EntityCreationFailedException(string entityName)
            : base($"{entityName} creation failed.")
        {
        }
    }
}
