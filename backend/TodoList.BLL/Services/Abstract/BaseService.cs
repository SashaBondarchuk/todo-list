using AutoMapper;
using TodoList.DAL.Context;

namespace TodoList.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly TodoListDbContext _context;
        private protected readonly IMapper _mapper;

        protected BaseService(TodoListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
