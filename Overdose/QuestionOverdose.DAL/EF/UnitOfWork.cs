using System.Threading.Tasks;
using QuestionOverdose.DAL.Interfaces;

namespace QuestionOverdose.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(EFContext context)
        {
            Context = context;
        }

        public EFContext Context { get; }

        public async Task Commit()
        {
           await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}