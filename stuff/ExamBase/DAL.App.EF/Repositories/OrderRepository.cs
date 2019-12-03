using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : BaseRepository<Order, Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<Order>> AllAsyncWithSearch(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Include(o => o.DrinksInOrder).ThenInclude(oo => oo.Drink)
                .Include(o => o.PizzasInOrder).ThenInclude(oo => oo.Pizza)
                .Where(c => c.AppUserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(o => o.Description.ToLower().Contains(search) ||
                                         o.Price.ToString().Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Order>> AllAsyncWithSearch(string search)
        {
            var query = RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Include(o => o.DrinksInOrder).ThenInclude(oo => oo.Drink)
                .Include(o => o.PizzasInOrder).ThenInclude(oo => oo.Pizza)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(o => o.Description.ToLower().Contains(search) ||
                                         o.Price.ToString().Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Order> FindAsyncById(int? id, int userId)
        {
            return await RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Where(c => c.AppUserId == userId && c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Order> FindAsyncById(int? id)
        {
            return await RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Where(c => c.Id == id).FirstOrDefaultAsync();
            
        }

        public async Task<int> FindOrderPriceAsyncById(int? id, int userId)
        {
            return await RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Where(c => c.AppUserId == userId && c.Id == id).Select(e => e.Price).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.Order>> AllAsyncWithSearchAPI(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Include(o => o.DrinksInOrder).ThenInclude(oo => oo.Drink)
                .Include(o => o.PizzasInOrder).ThenInclude(oo => oo.Pizza)
                .Where(o => o.AppUserId == userId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(o => o.Description.ToLower().Contains(search) ||
                                         o.Price.ToString().Contains(search));
            }

            return (await query.ToListAsync()).Select(e => new DTO.Order()
            {
                Id = e.Id,
                DeliveryId = e.DeliveryId,
                Description = e.Description,
                DeliveryService = e.Delivery.DeliveryService,
                OrderState = e.OrderState.ToString(),
                Price = e.Price,
                PizzasInOrder = e.PizzasInOrder?.Select(a => new DTO.PizzaInOrder()
                {
                    Id = a.Id,
                    OrderDescription = a.Order.Description,
                    OrderId = a.OrderId,
                    PizzaDescription = a.Pizza.Description,
                    PizzaId = a.PizzaId
                }).ToList(),
                DrinksInOrder = e.DrinksInOrder?.Select(a => new DTO.DrinkInOrder()
                    {
                        DrinkDescription = a.Drink.Description,
                        DrinkId = a.DrinkId,
                        Id = a.Id,
                        OrderDescription = a.Order.Description,
                        OrderId = a.OrderId
                    })
                    .ToList()
            }).ToList();
            
        }

        public async Task<DTO.Order> FindAsyncByIdAPI(int id, int userId)
        {
            var e = await RepositoryDbSet
                .Include(o => o.AppUser)
                .Include(o => o.Delivery)
                .Where(c => c.AppUserId == userId && c.Id == id).FirstOrDefaultAsync();
            if (e == null)
            {
                return new DTO.Order();
            }
            return new DTO.Order()
            {
                Id = e.Id,
                DeliveryId = e.DeliveryId,
                Description = e.Description,
                DeliveryService = e.Delivery.DeliveryService,
                OrderState = e.OrderState.ToString(),
                Price = e.Price,
                PizzasInOrder = e.PizzasInOrder?.Select(a => new DTO.PizzaInOrder()
                {
                    Id = a.Id,
                    OrderDescription = a.Order.Description,
                    OrderId = a.OrderId,
                    PizzaDescription = a.Pizza.Description,
                    PizzaId = a.PizzaId
                }).ToList(),
                DrinksInOrder = e.DrinksInOrder?.Select(a => new DTO.DrinkInOrder()
                    {
                        DrinkDescription = a.Drink.Description,
                        DrinkId = a.DrinkId,
                        Id = a.Id,
                        OrderDescription = a.Order.Description,
                        OrderId = a.OrderId
                    })
                    .ToList()
            };
        }

        private string EnumParser(OrderState state)
        {
            switch (state)
            {
                case OrderState.Done:
                    return "done";
                case OrderState.Paid:
                    return "paid";
                case OrderState.Waiting:
                    return "waiting";
                case OrderState.InProcess:
                    return "inProcess";
            }

            return "";
        }
    }
}