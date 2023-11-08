using TodoList.Common.DTO.User;

namespace TodoList.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(NewUserDto newUserDto);
        Task<UserDto> LoginAsync(UserLoginDto userLoginDto);
    }
}
