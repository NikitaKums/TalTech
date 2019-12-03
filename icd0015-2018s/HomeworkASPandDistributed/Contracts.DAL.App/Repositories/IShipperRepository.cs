using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IShipperRepository : IShipperRepository<DALAppDTO.DomainLikeDTO.Shipper>
    {
        Task<List<DALAppDTO.ShipperWithOrderCount>> GetAllWithOrdersCountByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<DALAppDTO.ShipperWithOrderCount> FindByIdAsyncDTO(int id);
    }
    
    public interface IShipperRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);        
    }
}