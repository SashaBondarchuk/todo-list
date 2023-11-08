namespace TodoList.Common.Auth.Abstractions
{
    public interface IUserIdGetter
    {
        int CurrentUserId { get; }
    }
}
