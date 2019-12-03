using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IShopService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Shop>, IShopRepository<BLLAppDTO.DomainLikeDTO.Shop>
    {
        Task<BLLAppDTO.ShopWithCounts> GetShopByShopId(int? shopId);
        Task<List<BLLAppDTO.ShopWithCounts>> GetAllWithCountsAsync();
    }
}