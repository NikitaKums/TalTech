using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Product>, IProductRepository<BLLAppDTO.DomainLikeDTO.Product>
    {
        Task<BLLAppDTO.ProductWithCounts> FindByShopAndId(int id, int? shopId);
        Task<List<BLLAppDTO.ProductWithCounts>> GetProductIdNameByShopInInventoryDTO(int? shopId);
        Task<List<BLLAppDTO.ProductWithCounts>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<List<BLLAppDTO.ProductWithCounts>> GetProductIdNameByShopDTO(int? shopId);
    }
}