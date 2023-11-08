using Microsoft.AspNetCore.Authorization;

namespace TodoList.Common.Auth.Attributes
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = BasicAuthDefaults.AuthenticationScheme;
        }
    }
}
