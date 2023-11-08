using AutoMapper;
using TodoList.Common.DTO.User;
using TodoList.DAL.Entities;

namespace TodoList.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserDto, User>().ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
