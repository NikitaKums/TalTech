using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IShipperService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Shipper>, IShipperRepository<BLLAppDTO.DomainLikeDTO.Shipper>
    {
        Task<List<BLLAppDTO.ShipperWithOrderCount>> GetAllWithOrdersCountByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.ShipperWithOrderCount> FindByIdAsyncDTO(int id); 
    }
}