using BLL.App.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.nikita.BLL.Base.Helpers;

namespace BLL.App.Helpers
{
    public class AppServiceFactory : BaseServiceFactory<IAppUnitOfWork>
    {
        public AppServiceFactory()
        {
            RegisterServices();
        }
        
        private void RegisterServices()
        {
            // Register all your custom services here!
            AddToCreationMethods<ICategoryService>(uow => new CategoryService(uow));
            AddToCreationMethods<ICommentService>(uow => new CommentService(uow));
            AddToCreationMethods<IDefectService>(uow => new DefectService(uow));
            AddToCreationMethods<IInventoryService>(uow => new InventoryService(uow));
            AddToCreationMethods<IManuFacturerService>(uow => new ManuFacturerService(uow));
            AddToCreationMethods<IOrderService>(uow => new OrderService(uow));
            AddToCreationMethods<IProductService>(uow => new ProductService(uow));
            AddToCreationMethods<IProductInOrderService>(uow => new ProductInOrderService(uow));
            AddToCreationMethods<IProductReturnedService>(uow => new ProductReturnedService(uow));
            AddToCreationMethods<IProductSoldService>(uow => new ProductSoldService(uow));
            AddToCreationMethods<IProductWithDefectService>(uow => new ProductWithDefectService(uow));
            AddToCreationMethods<IReturnService>(uow => new ReturnService(uow));
            AddToCreationMethods<ISaleService>(uow => new SaleService(uow));
            AddToCreationMethods<IShipperService>(uow => new ShipperService(uow));
            AddToCreationMethods<IShopService>(uow => new ShopService(uow));
            AddToCreationMethods<IProductInCategoryService>(uow => new ProductInCategoryService(uow));
            AddToCreationMethods<IAppUserService>(uow => new AppUserService(uow));
        }

    }
}