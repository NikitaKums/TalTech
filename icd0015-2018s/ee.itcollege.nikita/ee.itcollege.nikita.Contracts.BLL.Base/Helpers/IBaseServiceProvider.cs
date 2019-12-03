
namespace ee.itcollege.nikita.Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceProvider
    {
        TService GetService<TService>();
        
        /*
        IBaseEntityService<TEntity> GetEntityService<TEntity>() where TEntity : class, IDomainEntity, new();
        */
    }
}
