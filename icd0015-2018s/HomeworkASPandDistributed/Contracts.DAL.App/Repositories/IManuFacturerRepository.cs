using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IManuFacturerRepository : IManuFacturerRepository<DALAppDTO.DomainLikeDTO.ManuFacturer>
    {
        Task<DALAppDTO.ManuFacturerWithProductCount> FindByIdAndShop(int id, int? shopId);
        Task<List<DALAppDTO.ManuFacturerWithProductCount>> GetAllWithProductCountAsync(int? shopId, string search, int? pageIndex, int? pageSize);
    }

    public interface IManuFacturerRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);        
    }
}