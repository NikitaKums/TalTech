using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.nikita.DAL.Base.EF.Helpers;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory<AppDbContext>
    {
        
        public AppRepositoryFactory()
        {
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            AddToCreationMethods<ICategoryRepository>(dataContext => new CategoryRepository(dataContext));
            AddToCreationMethods<ICommentRepository>(dataContext => new CommentRepository(dataContext));
            AddToCreationMethods<IDefectRepository>(dataContext => new DefectRepository(dataContext));
            AddToCreationMethods<IInventoryRepository>(dataContext => new InventoryRepository(dataContext));
            AddToCreationMethods<IManuFacturerRepository>(dataContext => new ManuFacturerRepository(dataContext));
            AddToCreationMethods<IOrderRepository>(dataContext => new OrderRepository(dataContext));
            AddToCreationMethods<IProductRepository>(dataContext => new ProductRepository(dataContext));
            AddToCreationMethods<IProductInOrderRepository>(dataContext => new ProductInOrderRepository(dataContext));
            AddToCreationMethods<IProductReturnedRepository>(dataContext => new ProductReturnedRepository(dataContext));
            AddToCreationMethods<IProductSoldRepository>(dataContext => new ProductSoldRepository(dataContext));
            AddToCreationMethods<IProductWithDefectRepository>(dataContext => new ProductWithDefectRepository(dataContext));
            AddToCreationMethods<IReturnRepository>(dataContext => new ReturnRepository(dataContext));
            AddToCreationMethods<ISaleRepository>(dataContext => new SaleRepository(dataContext));
            AddToCreationMethods<IShipperRepository>(dataContext => new ShipperRepository(dataContext));
            AddToCreationMethods<IShopRepository>(dataContext => new ShopRepository(dataContext));
            AddToCreationMethods<IAppUserRepository>(dataContext => new AppUserRepository(dataContext));
            AddToCreationMethods<IProductInCategoryRepository>(dataContext => new ProductInCategoryRepository(dataContext));
        }
    }
}