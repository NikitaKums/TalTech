using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IReturnService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Return>, IReturnRepository<BLLAppDTO.DomainLikeDTO.Return>
    {
        Task<List<BLLAppDTO.ReturnWithProductCount>> GetAllWithProductsReturnedByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.ReturnWithProductCount> FindWithProductsReturnedByIdAndShopAsync(int id, int? shopId);
        Task<List<BLLAppDTO.IdAndNameDTO.ReturnIdName>> GetAllIdAndDescAsyncByShopDTO(int? shopId);
        Task DeleteReturn(int id);
    }
}