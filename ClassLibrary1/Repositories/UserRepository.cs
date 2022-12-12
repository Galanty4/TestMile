using BLL.Entities;
using BLL.Interfaces.Repositories;
using DAL.TestMilDbContext;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TestMilDBcontext _dbContext;

        public UserRepository(TestMilDBcontext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
