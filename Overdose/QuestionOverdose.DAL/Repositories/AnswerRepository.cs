using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>
    {
        public AnswerRepository(EFContext context)
            : base(context)
        {
        }

        public async Task<Answer> GetByDate(int userId, DateTime publicationTime)
        {
            var answer = await FindByCondition(x => x.AuthorId == userId && x.DateOfPublication == publicationTime)
                .Include(x => x.Author)
                .Include(y => y.Voters)
                .ThenInclude(z => z.User)
                .Include(y => y.Comments)
                .ThenInclude(z => z.Author)
                .FirstOrDefaultAsync();
            return answer;
        }
    }
}