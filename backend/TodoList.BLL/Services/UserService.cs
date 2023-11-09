using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoList.BLL.Interfaces;
using TodoList.BLL.Services.Abstract;
using TodoList.Common.Auth.Helpers;
using TodoList.Common.DTO.User;
using TodoList.Common.Exceptions;
using TodoList.DAL.Context;
using TodoList.DAL.Entities;

namespace TodoList.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(TodoListDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDto> RegisterAsync(NewUserDto newUserDto)
        {
            var user = _mapper.Map<User>(newUserDto);
            var users = _context.Users.ToList();

            if (users.Exists(u => u.Username == user.Username))
            {
                throw new BadOperationException("User with such username already exists");
            }

            if (users.Exists(u => u.Email == user.Email))
            {
                throw new BadOperationException("User with such email already exists");
            }

            user.PasswordHash = PasswordHasher.HashPassword(newUserDto.Password);
            _context.Add(user);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username)
                ?? throw new NotFoundException(nameof(User));

            if (!PasswordHasher.VerifyPassword(user.PasswordHash, userLoginDto.Password))
            {
                throw new InvalidUsernameOrPasswordException("Invalid username or password");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
