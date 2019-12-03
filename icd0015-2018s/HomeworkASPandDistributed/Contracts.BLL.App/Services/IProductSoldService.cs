using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductSoldService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.ProductSold>, IProductSoldRepository<BLLAppDTO.DomainLikeDTO.ProductSold>
    {
        Task<List<BLLAppDTO.ProductSold>> AllAsyncByShopDTO(int? shopId);
        Task<BLLAppDTO.ProductSold> GetAsyncByShopAndIdDTO(int id, int? shopId); 
        Task<List<BLLAppDTO.ProductSold>> AllAsyncByShopAndSaleId(int id, int? shopId);
        Task<bool> AddProductToSale(int productId, BLLAppDTO.DomainLikeDTO.ProductSold productSold);
        Task<bool> EditProductInSale(int id, int productId, BLLAppDTO.DomainLikeDTO.ProductSold productSold);
        Task DeleteWithRestoration(int id);
    }
}