using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductWithDefectService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.ProductWithDefect>, IProductWithDefectRepository<BLLAppDTO.DomainLikeDTO.ProductWithDefect>
    {
        Task<List<BLLAppDTO.ProductWithDefect>> AllAsyncByShopDTO(int? shopId);
        Task<BLLAppDTO.ProductWithDefect> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<BLLAppDTO.DomainLikeDTO.ProductWithDefect>> AllAsyncByDefectId(int defectId);
        Task<List<BLLAppDTO.IdAndNameDTO.ProductIdName>> AllAsyncByDefectIdAndShopId(int defectId, int? shopId);
        Task<List<BLLAppDTO.ProductWithDefect>> AllAsyncByShopAndDefectIdDTO(int id, int? shopId);
    }
}