using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.Base;

namespace ee.itcollege.nikita.Contracts.BLL.Base
{
    public interface IBaseBLL : ITrackableInstance
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
        /*IBaseEntityService<TEntity> BaseService<TEntity>()
            where TEntity : class, IBaseEntity, new();*/
    }
}