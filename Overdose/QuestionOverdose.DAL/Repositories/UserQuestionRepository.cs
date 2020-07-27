using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class UserQuestionRepository : RepositoryBase<UserQuestion>
    {
        private readonly EFContext _dbContext;

        public UserQuestionRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<UserQuestion>> GetByUserIdAsync(int id)
        {
            return await FindByCondition(x => x.UserId == id)
                        .Include(x => x.Question)
                        .Include(x => x.User)
                        .ToListAsync();
        }
    }
}