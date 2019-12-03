using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.IdAndNameDTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.nikita.BLL.Base.Services;
using ProductWithDefect = BLL.App.DTO.DomainLikeDTO.ProductWithDefect;

namespace BLL.App.Services
{
    public class ProductWithDefectService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.ProductWithDefect, DAL.App.DTO.DomainLikeDTO.ProductWithDefect, IAppUnitOfWork>, IProductWithDefectService
    {
        public ProductWithDefectService(IAppUnitOfWork uow) : base(uow, new ProductWithDefectMapper())
        {
            ServiceRepository = Uow.ProductsWithDefect;
        }


        /*public override async Task<IEnumerable<ProductWithDefect>> AllAsync()
        {
            return await UOW.ProductsWithDefect.AllAsync();
        }

        public override async Task<ProductWithDefect> FindAsync(params object[] id)
        {
            return await UOW.ProductsWithDefect.FindAsync(id);
        }

        public async Task<IEnumerable<ProductWithDefect>> AllAsyncByShop(int? shopId)
        {
            return await UOW.ProductsWithDefect.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ProductWithDefectDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.ProductsWithDefect.AllAsyncByShopDTO(shopId);
        }

        public async Task<ProductWithDefectDTO> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return await UOW.ProductsWithDefect.GetAsyncByShopAndIdDTO(id, shopId);
        }*/

        public override async Task<List<ProductWithDefect>> AllAsync()
        {
            return (await Uow.ProductsWithDefect.AllAsync()).Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<ProductWithDefect> FindAsync(params object[] id)
        {
            return ProductWithDefectMapper.MapFromDAL(await Uow.ProductsWithDefect.FindAsync(id));
        }

        public async Task<List<ProductWithDefect>> AllAsyncByShop(int? shopId)
        {
            return (await Uow.ProductsWithDefect.AllAsyncByShop(shopId)).Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
        }

        public async Task<int> CountDefectItems(int defectId)
        {
            return await Uow.ProductsWithDefect.CountDefectItems(defectId);
        }

        public async Task<List<DTO.ProductWithDefect>> AllAsyncByShopDTO(int? shopId)
        {
            return (await Uow.ProductsWithDefect.AllAsyncByShopDTO(shopId)).Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
        }

        public async Task<DTO.ProductWithDefect> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return ProductWithDefectMapper.MapFromDAL(await Uow.ProductsWithDefect.GetAsyncByShopAndIdDTO(id, shopId));
        }

        public async Task<List<ProductWithDefect>> AllAsyncByDefectId(int defectId)
        {
            return (await Uow.ProductsWithDefect.AllAsyncByDefectId(defectId))
                .Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
        }
        
        public async Task<List<ProductIdName>> AllAsyncByDefectIdAndShopId(int defectId, int? shopId)
        {
            return (await Uow.ProductsWithDefect.AllAsyncByDefectIdAndShopId(defectId, shopId))
                .Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<DTO.ProductWithDefect>> AllAsyncByShopAndDefectIdDTO(int id, int? shopId)
        {
            return (await Uow.ProductsWithDefect.AllAsyncByShopAndDefectIdDTO(id, shopId)).Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
        }
    }
}