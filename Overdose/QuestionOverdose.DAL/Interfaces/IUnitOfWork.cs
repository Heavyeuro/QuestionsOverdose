using System.Threading.Tasks;

namespace QuestionOverdose.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
