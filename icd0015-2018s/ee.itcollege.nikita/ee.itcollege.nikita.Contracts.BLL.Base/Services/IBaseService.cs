using ee.itcollege.nikita.Contracts.Base;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;

namespace ee.itcollege.nikita.Contracts.BLL.Base.Services
{
    public interface IBaseService : ITrackableInstance
    {
    }
    
    public interface IBaseEntityService<TBLLEntity> :IBaseService, IBaseRepository<TBLLEntity> 
        where TBLLEntity : class, new()
    {
        
    }  
}