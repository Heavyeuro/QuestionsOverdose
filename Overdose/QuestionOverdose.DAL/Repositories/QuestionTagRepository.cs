using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class QuestionTagRepository : RepositoryBase<QuestionTag>
    {
        private readonly EFContext _dbContext;

        public QuestionTagRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }
    }
}