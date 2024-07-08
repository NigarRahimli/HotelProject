namespace Project.Infrastructure.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
            : base("Invalid email or password")
        {
        }
    }
}
