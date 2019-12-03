using System;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        ICommentRepository Comments { get; }
        IAppUserRepository AppUsers { get; }
        IDeliveryRepository Deliverys { get; }
        IDrinkInOrderRepository DrinksInOrder { get; }
        IDrinkRepository Drinks { get; }
        IOrderRepository Orders { get; }
        IPizzaInOrderRepository PizzasInOrder { get; }
        IPizzaRepository Pizzas { get; }
        IToppingOnPizzaRepository ToppingsOnPizza { get; }
        IToppingRepository Toppings { get; }
    }
}