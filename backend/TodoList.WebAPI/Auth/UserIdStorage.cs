using TodoList.Common.Auth.Abstractions;

namespace TodoList.WebAPI.Auth
{
    public class UserIdStorage : IUserIdGetter, IUserIdSetter
    {
        private int _id;

        public int CurrentUserId => _id;

        public void SetUserId(int userId)
        {
            _id = userId;
        }
    }
}
