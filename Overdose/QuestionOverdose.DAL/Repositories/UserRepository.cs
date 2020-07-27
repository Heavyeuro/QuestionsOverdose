using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        private readonly EFContext _dbContext;

        public UserRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<User> GetUserByNickAndPass(string nick, string pass)
        {
            return await FindByCondition(x => x.Username == nick && x.Password == pass)
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByNick(string nick)
        {
            var user = await FindByCondition(x => x.Username == nick)
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}