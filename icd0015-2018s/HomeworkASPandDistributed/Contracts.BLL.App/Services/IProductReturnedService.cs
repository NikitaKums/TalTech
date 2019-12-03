using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductReturnedService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.ProductReturned>, IProductReturnedRepository<BLLAppDTO.DomainLikeDTO.ProductReturned>
    {
        Task<List<BLLAppDTO.ProductReturned>> AllAsyncByShopDTO(int? shopId);
        Task<BLLAppDTO.ProductReturned> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<BLLAppDTO.ProductReturned>> AllAsyncByShopAndIdDTO(int id, int? shopId);
    }
}