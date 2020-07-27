using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Interfaces;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        private readonly EFContext _dbContext;

        public TagRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<Tag> GetByNameAsync(string tagName)
        {
            var tag = await FindByCondition(x => x.TagName == tagName)
                .FirstOrDefaultAsync();
            return tag;
        }

        public Tag GetByName(string tagName)
        {
            var tag = FindByCondition(x => x.TagName == tagName)
                .FirstOrDefault();
            return tag;
        }
    }
}