using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class UserAnswerRepository : RepositoryBase<UserAnswer>
    {
        private readonly EFContext _dbContext;

        public UserAnswerRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<UserAnswer>> GetByUserIdAsync(int id)
        {
            return await FindByCondition(x => x.UserId == id)
                        .Include(x => x.Answer)
                        .Include(x => x.User)
                        .ToListAsync();
        }
    }
}