using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoList.BLL.Interfaces;
using TodoList.BLL.Services.Abstract;
using TodoList.Common.Auth.Abstractions;
using TodoList.Common.Auth.Helpers;
using TodoList.Common.DTO.User;
using TodoList.DAL.Context;
using TodoList.DAL.Entities;

namespace TodoList.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserIdSetter _userIdSetter;

        public UserService(TodoListDbContext context, IMapper mapper, IUserIdSetter userIdSetter) : base(context, mapper)
        {
            _userIdSetter = userIdSetter;
        }

        public async Task<UserDto> RegisterAsync(NewUserDto newUserDto)
        {
            var user = _mapper.Map<User>(newUserDto);
            var users = _context.Users.ToList();

            if (users.Any(u => u.Username == user.Username))
            {
                throw new InvalidOperationException("User with a such username exists");
            }

            if (users.Any(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("User with an email exists");
            }

            user.PasswordHash = PasswordHasher.HashPassword(newUserDto.Password);
            _context.Add(user);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username)
                ?? throw new InvalidOperationException("User was not found");

            if (!PasswordHasher.VerifyPassword(user.PasswordHash, userLoginDto.Password))
            {
                throw new InvalidOperationException("Authentication failed");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
