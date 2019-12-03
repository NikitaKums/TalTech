using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAppUserRepository AppUsers { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IDefectRepository Defects { get; }
        IInventoryRepository Inventories { get; }
        IManuFacturerRepository ManuFacturers { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IProductInCategoryRepository ProductsInCategory { get; }
        IProductInOrderRepository ProductsInOrder { get; }
        IProductReturnedRepository ProductsReturned { get; }
        IProductSoldRepository ProductsSold { get; }
        IProductWithDefectRepository ProductsWithDefect { get; }
        IReturnRepository Returns { get; }
        ISaleRepository Sales { get; }
        IShipperRepository Shippers { get; }
        IShopRepository Shops { get; }
    }
}
