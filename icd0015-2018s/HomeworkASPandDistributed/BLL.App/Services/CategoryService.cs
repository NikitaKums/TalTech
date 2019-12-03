using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Category = BLL.App.DTO.DomainLikeDTO.Category;

namespace BLL.App.Services
{
    public class CategoryService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Category, DAL.App.DTO.DomainLikeDTO.Category, IAppUnitOfWork>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork uow) : base(uow, new CategoryMapper())
        {
            ServiceRepository = Uow.Categories;
        }


        /*public override async Task<IEnumerable<Category>> AllAsync()
        {
            return await UOW.Categories.AllAsync();
        }

        public override async Task<Category> FindAsync(params object[] id)
        {
            return await UOW.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Categories.AllAsyncByShop(shopId);
        }
        

        public async Task<IEnumerable<CategoryDTO>> GetAllWithProductCountForShopAsync(int? shopId)
        {
            return await UOW.Categories.GetAllWithProductCountForShopAsync(shopId);
        }

        public async Task<CategoryDTO> GetByIndexAndShop(int categoryId, int? shopId)
        {
            return await UOW.Categories.GetByIndexAndShop(categoryId, shopId);
        }*/

        public async Task DeleteCategory(int categoryId)
        {
            var productsInCategory = await Uow.ProductsInCategory.AllAsyncByCategoryId(categoryId);
            foreach (var productInCategory in productsInCategory)
            {
                Uow.ProductsInCategory.Remove(productInCategory.Id);
            }
            Uow.Categories.Remove(categoryId);
            await Uow.SaveChangesAsync();
        }

        public override async Task<List<Category>> AllAsync()
        {
            return (await Uow.Categories.AllAsync()).Select(e => CategoryMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Category> FindAsync(params object[] id)
        {
            return CategoryMapper.MapFromDAL(await Uow.Categories.FindAsync(id));
        }

        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Categories.CountDataAmount(search);
        }

        public async Task<List<Category>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var res = (await Uow.Categories.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => CategoryMapper.MapFromDAL(e)).ToList();
            return res;
        }

        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Categories.CountDataAmount(shopId, search);
        }

        public async Task<List<Category>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var res = (await Uow.Categories.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => CategoryMapper.MapFromDAL(e)).ToList();
            return res;
        }

        public async Task<List<CategoryWithProductCount>> GetAllWithProductCountForShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Categories.GetAllWithProductCountForShopAsync(shopId, search, pageIndex, pageSize))
                .Select(e => CategoryMapper.MapFromDAL(e)).ToList();
        }

        public async Task<CategoryWithProductCount> GetByIndexAndShop(int categoryId, int? shopId)
        {
            return CategoryMapper.MapFromDAL(await Uow.Categories.GetByIndexAndShop(categoryId, shopId));
        }
    }
}