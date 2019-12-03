using System;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.nikita.BLL.Base;
using ee.itcollege.nikita.Contracts.BLL.Base.Helpers;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {   
        protected readonly IAppUnitOfWork AppUnitOfWork;
        
        public AppBLL(IAppUnitOfWork appUnitOfWork, IBaseServiceProvider serviceProvider) : base(appUnitOfWork, serviceProvider)
        {
            AppUnitOfWork = appUnitOfWork;
        }


        public ICategoryService Categories => ServiceProvider.GetService<ICategoryService>();
        public ICommentService Comments => ServiceProvider.GetService<ICommentService>();
        public IDefectService Defects => ServiceProvider.GetService<IDefectService>();
        public IInventoryService Inventories => ServiceProvider.GetService<IInventoryService>();
        public IManuFacturerService ManuFacturers => ServiceProvider.GetService<IManuFacturerService>();
        public IOrderService Orders => ServiceProvider.GetService<IOrderService>();
        public IProductService Products => ServiceProvider.GetService<IProductService>();
        public IProductInOrderService ProductsInOrder => ServiceProvider.GetService<IProductInOrderService>();
        public IProductReturnedService ProductsReturned => ServiceProvider.GetService<IProductReturnedService>();
        public IProductSoldService ProductsSold => ServiceProvider.GetService<IProductSoldService>();
        public IProductWithDefectService ProductsWithDefect => ServiceProvider.GetService<IProductWithDefectService>();
        public IReturnService Returns => ServiceProvider.GetService<IReturnService>();
        public ISaleService Sales => ServiceProvider.GetService<ISaleService>();
        public IShipperService Shippers => ServiceProvider.GetService<IShipperService>();
        public IShopService Shops => ServiceProvider.GetService<IShopService>();
        public IProductInCategoryService ProductsInCategory => ServiceProvider.GetService<IProductInCategoryService>();
        public IAppUserService AppUsers => ServiceProvider.GetService<IAppUserService>();
    }
}