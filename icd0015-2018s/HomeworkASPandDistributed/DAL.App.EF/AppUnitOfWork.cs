using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.DAL.Base.Helpers;
using ee.itcollege.nikita.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {

        public AppUnitOfWork(AppDbContext dbContext, IBaseRepositoryProvider repositoryProvider) : base(dbContext, repositoryProvider)
        {
        }

        public ICategoryRepository Categories =>
            _repositoryProvider.GetRepository<ICategoryRepository>();
        
        public ICommentRepository Comments =>
            _repositoryProvider.GetRepository<ICommentRepository>();
        
        public IDefectRepository Defects =>
            _repositoryProvider.GetRepository<IDefectRepository>();
        
        public IInventoryRepository Inventories =>
            _repositoryProvider.GetRepository<IInventoryRepository>();

        public IManuFacturerRepository ManuFacturers =>
            _repositoryProvider.GetRepository<IManuFacturerRepository>();

        public IOrderRepository Orders =>
            _repositoryProvider.GetRepository<IOrderRepository>();
        
        public IProductRepository Products =>
            _repositoryProvider.GetRepository<IProductRepository>();
        
        public IProductInOrderRepository ProductsInOrder =>
            _repositoryProvider.GetRepository<IProductInOrderRepository>();
        
        public IProductReturnedRepository ProductsReturned =>
            _repositoryProvider.GetRepository<IProductReturnedRepository>();

        public IProductSoldRepository ProductsSold =>
            _repositoryProvider.GetRepository<IProductSoldRepository>();
        
        public IProductWithDefectRepository ProductsWithDefect =>
            _repositoryProvider.GetRepository<IProductWithDefectRepository>();

        public IReturnRepository Returns =>
            _repositoryProvider.GetRepository<IReturnRepository>();

        public ISaleRepository Sales =>
            _repositoryProvider.GetRepository<ISaleRepository>();

        public IShipperRepository Shippers =>
            _repositoryProvider.GetRepository<IShipperRepository>();
        
        public IShopRepository Shops =>
            _repositoryProvider.GetRepository<IShopRepository>();

        public IProductInCategoryRepository ProductsInCategory =>
            _repositoryProvider.GetRepository<IProductInCategoryRepository>();

        public IAppUserRepository AppUsers =>
            _repositoryProvider.GetRepository<IAppUserRepository>();
        
    }
}