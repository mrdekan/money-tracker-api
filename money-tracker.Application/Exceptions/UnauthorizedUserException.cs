namespace money_tracker.Application.Exceptions
{
    public class UnauthorizedUserException : Exception
    {
        public UnauthorizedUserException()
            : base("User is not authorized.") { }

        public UnauthorizedUserException(string message)
            : base(message) { }
    }
}
