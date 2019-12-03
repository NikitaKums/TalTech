using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Helpers;
using ee.itcollege.nikita.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext dataContext, IBaseRepositoryProvider repositoryProvider) : base(dataContext,
            repositoryProvider)
        {
        }
        public ICommentRepository Comments => _repositoryProvider.GetRepository<ICommentRepository>();
        public IAppUserRepository AppUsers => _repositoryProvider.GetRepository<IAppUserRepository>();
        public IDeliveryRepository Deliverys => _repositoryProvider.GetRepository<IDeliveryRepository>();
        public IDrinkInOrderRepository DrinksInOrder => _repositoryProvider.GetRepository<IDrinkInOrderRepository>();
        public IDrinkRepository Drinks => _repositoryProvider.GetRepository<IDrinkRepository>();
        public IOrderRepository Orders => _repositoryProvider.GetRepository<IOrderRepository>();
        public IPizzaInOrderRepository PizzasInOrder => _repositoryProvider.GetRepository<IPizzaInOrderRepository>();
        public IPizzaRepository Pizzas => _repositoryProvider.GetRepository<IPizzaRepository>();
        public IToppingOnPizzaRepository ToppingsOnPizza => _repositoryProvider.GetRepository<IToppingOnPizzaRepository>();
        public IToppingRepository Toppings => _repositoryProvider.GetRepository<IToppingRepository>();
    }
}