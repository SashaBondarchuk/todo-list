using System.Net;
using TodoList.Common.Enums;
using TodoList.Common.Exceptions;

namespace TodoList.Common.Extensions
{
    public static class ExceptionFilterExtensions
    {
        public static (HttpStatusCode statusCode, ErrorCode errorCode) ParseException(this Exception exception)
        {
            return exception switch
            {
                NotFoundException _ => (HttpStatusCode.NotFound, ErrorCode.NotFound),
                InvalidUsernameOrPasswordException _ => (HttpStatusCode.Unauthorized, ErrorCode.InvalidUsernameOrPassword),
                BadOperationException _ => (HttpStatusCode.BadRequest, ErrorCode.BadRequest),
                _ => (HttpStatusCode.InternalServerError, ErrorCode.General),
            };
        }
    }
}
