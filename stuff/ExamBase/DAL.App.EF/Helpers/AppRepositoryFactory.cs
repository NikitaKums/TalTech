using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using Domain;
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
            AddToCreationMethods<ICommentRepository>(dataContext => new CommentRepository(dataContext));
            AddToCreationMethods<IAppUserRepository>(dataContext => new AppUserRepository(dataContext));
            AddToCreationMethods<IDeliveryRepository>(dataContext => new DeliveryRepository(dataContext));
            AddToCreationMethods<IDrinkRepository>(dataContext => new DrinkRepository(dataContext));
            AddToCreationMethods<IDrinkInOrderRepository>(dataContext => new DrinkInOrderRepository(dataContext));
            AddToCreationMethods<IOrderRepository>(dataContext => new OrderRepository(dataContext));
            AddToCreationMethods<IPizzaRepository>(dataContext => new PizzaRepository(dataContext));
            AddToCreationMethods<IPizzaInOrderRepository>(dataContext => new PizzaInOrderRepository(dataContext));
            AddToCreationMethods<IToppingOnPizzaRepository>(dataContext => new ToppingOnPizzaRepository(dataContext));
            AddToCreationMethods<IToppingRepository>(dataContext => new ToppingRepository(dataContext));
        }

    }
}