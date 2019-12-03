using System.Threading.Tasks;

namespace ee.itcollege.nikita.Contracts.DAL.Base
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
