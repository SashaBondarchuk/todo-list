namespace TodoList.Common.Exceptions
{
    public class InvalidUsernameOrPasswordException : Exception
    {
        public InvalidUsernameOrPasswordException() : base("Invalid username or password.") { }

        public InvalidUsernameOrPasswordException(string message) : base(message) { }
    }
}
