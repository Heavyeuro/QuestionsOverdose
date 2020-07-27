using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>
    {
        private readonly EFContext _dbContext;

        public CommentRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public async Task<Comment> GetByDate(int userId, DateTime publicationTime)
        {
            var comment = await FindByCondition(x => x.AuthorId == userId && x.DateOfCreation == publicationTime)
                .Include(x => x.Author).FirstOrDefaultAsync();
            return comment;
        }
    }
}