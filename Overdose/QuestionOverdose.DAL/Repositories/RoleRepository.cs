using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Interfaces;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly EFContext _dbContext;

        public RoleRepository(EFContext context)
            : base(context)
        {
            _dbContext = context;
        }
    }
    }