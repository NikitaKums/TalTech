using System;
using Contracts.BLL.App.Services;
using ee.itcollege.nikita.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        ICategoryService Categories { get; }
        ICommentService Comments { get; }
        IDefectService Defects { get; }
        IInventoryService Inventories { get; }
        IManuFacturerService ManuFacturers { get; }
        IOrderService Orders { get; }
        IProductService Products { get; }
        IProductInOrderService ProductsInOrder { get; }
        IProductReturnedService ProductsReturned { get; }
        IProductSoldService ProductsSold { get; }
        IProductWithDefectService ProductsWithDefect { get; }
        IReturnService Returns { get; }
        ISaleService Sales { get; }
        IShipperService Shippers { get; }
        IShopService Shops { get; }
        IProductInCategoryService ProductsInCategory { get; }
        IAppUserService AppUsers { get; }
    }
}