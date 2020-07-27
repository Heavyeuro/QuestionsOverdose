using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Interfaces;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        private readonly EFContext _dbContext;

        public QuestionRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<Question>> GetPartlyAsync(int skip, int take, string tagName, bool isDeleted = false)
        {
            return await FindByCondition(x => x.Author != null && x.QuestionsTags
                    .Any(y => y.Tag.TagName == (tagName ?? y.Tag.TagName) && y.Question.IsDeleted == isDeleted))
            .OrderByDescending(x => x.Votes)
            .Skip(skip)
            .Take(take)
            .Include(x => x.Author)
            .Include(x => x.QuestionsTags)
                .ThenInclude(y => y.Tag)
            .Include(x => x.Voters)
                 .ThenInclude(y => y.User)
            .ToListAsync();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            var question = await FindByCondition(x => x.Id == id)
                .Include(x => x.Author)
                .Include(x => x.Answers.Where(z => z.IsDeleted == false))
                    .ThenInclude(y => y.Author)
                .Include(x => x.Answers.Where(z => z.IsDeleted == false))
                    .ThenInclude(y => y.Voters)
                        .ThenInclude(z => z.User)
                .Include(x => x.Answers.Where(z => z.IsDeleted == false))
                    .ThenInclude(y => y.Comments)
                        .ThenInclude(z => z.Author)
                .Include(x => x.QuestionsTags)
                .FirstOrDefaultAsync();
            return question;
        }

        public async Task<int> CountAsync(string tagName)
        {
            return await FindByCondition(x => x.QuestionsTags
                                                  .Any(y => y.Tag.TagName == (tagName ?? y.Tag.TagName))
                                              && x.IsDeleted == false).CountAsync();
        }

        public async Task<List<Question>> GetQuestionsLiveSearchAsync(string titleName)
        {
            return await FindByCondition(x => x.Title.ToLower().Contains(titleName)).Take(5).ToListAsync();
        }
    }
}