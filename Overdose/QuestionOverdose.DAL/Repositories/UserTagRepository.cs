using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class UserTagRepository : RepositoryBase<UserTag>
    {
        private readonly EFContext _dbContext;

        public UserTagRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<UserTag>> GetByUserIdAsync(int id)
        {
            return await FindByCondition(x => x.UserId == id)
                        .Include(x => x.Tag)
                        .Include(x => x.User)
                        .ToListAsync();
        }
    }
}