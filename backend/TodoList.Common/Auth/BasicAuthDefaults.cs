namespace TodoList.Common.Auth
{
    public static class BasicAuthDefaults
    {
        public static readonly string AuthenticationScheme = "Basic";
        public static readonly string AuthorizationHeaderKey = "Authorization";
        public static readonly string BasicAuthPrefix = "Basic ";
        public static readonly string MissingAuthorizationMessage = "Missing authorization key";
        public static readonly string InvalidAuthorizationMessage = "Invalid authorization header format";
        public static readonly string UserWasNotFoundMessage = "User with this username does not exist";
        public static readonly string InvalidPasswordMessage = "Invalid password";
    }
}
