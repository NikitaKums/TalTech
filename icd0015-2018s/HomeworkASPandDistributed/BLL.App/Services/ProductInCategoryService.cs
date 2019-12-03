using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO.IdAndNameDTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using ProductInCategory = BLL.App.DTO.DomainLikeDTO.ProductInCategory;

namespace BLL.App.Services
{
    public class ProductInCategoryService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.ProductInCategory, DAL.App.DTO.DomainLikeDTO.ProductInCategory, IAppUnitOfWork>, IProductInCategoryService
    {
        public ProductInCategoryService(IAppUnitOfWork uow) : base(uow, new ProductInCategoryMapper())
        {
            ServiceRepository = Uow.ProductsInCategory;
        }


        /*public override async Task<IEnumerable<ProductInCategory>> AllAsync()
        {
            return await UOW.ProductInCategory.AllAsync();
        }

        public override async Task<ProductInCategory> FindAsync(params object[] id)
        {
            return await UOW.ProductInCategory.FindAsync(id);
        }

        public async Task<IEnumerable<ProductInCategory>> AllAsyncByShop(int? shopId)
        {
            return await UOW.ProductInCategory.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ProductInCategoryDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.ProductInCategory.AllAsyncByShopDTO(shopId);
        }

        public async Task<ProductInCategoryDTO> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return await UOW.ProductInCategory.GetAsyncByShopAndIdDTO(id, shopId);
        }*/

        public override async Task<List<ProductInCategory>> AllAsync()
        {
            return (await Uow.ProductsInCategory.AllAsync()).Select(e => ProductInCategoryMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<ProductInCategory> FindAsync(params object[] id)
        {
            return ProductInCategoryMapper.MapFromDAL(await Uow.ProductsInCategory.FindAsync(id));
        }

        public async Task<List<ProductInCategory>> AllAsyncByCategoryId(int categoryId)
        {
            return (await Uow.ProductsInCategory.AllAsyncByCategoryId(categoryId))
                .Select(e => ProductInCategoryMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<ProductInCategory>> AllAsyncByShop(int? shopId)
        {
            return (await Uow.ProductsInCategory.AllAsyncByShop(shopId)).Select(e => ProductInCategoryMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<DTO.ProductInCategory>> AllAsyncByShopDTO(int? shopId)
        {
            return (await Uow.ProductsInCategory.AllAsyncByShopDTO(shopId)).Select(e => ProductInCategoryMapper.MapFromDAL(e)).ToList();
        }

        public async Task<DTO.ProductInCategory> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return ProductInCategoryMapper.MapFromDAL(await Uow.ProductsInCategory.GetAsyncByShopAndIdDTO(id, shopId));
        }

        public async Task<List<ProductIdName>> AllAsyncByCategoryIdAndShopId(int categoryId, int? shopId)
        {
            return (await Uow.ProductsInCategory.AllAsyncByCategoryIdAndShopId(categoryId, shopId))
                .Select(e => ProductInCategoryMapper.MapFromDAL(e)).ToList();
        }
    }
}