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
    public class PizzaInOrderRepository : BaseRepository<PizzaInOrder, PizzaInOrder, AppDbContext>, IPizzaInOrderRepository
    {
        public PizzaInOrderRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<PizzaInOrder>> AllAsyncWithSearch(int userId)
        {
            var query = RepositoryDbSet
                .Include(p => p.Order)
                .Include(p => p.Pizza)
                .Where(p => p.Order.AppUserId == userId).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<PizzaInOrder> FindAsyncById(int? id, int userId)
        {
           return await RepositoryDbSet
                .Include(p => p.Order)
                .Include(p => p.Pizza)
                .Where(p => p.Order.AppUserId == userId && p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.PizzaInOrder>> AllAsyncWithSearchAPI(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(p => p.Order)
                .Include(p => p.Pizza)
                .Where(p => p.Order.AppUserId == userId).AsQueryable();

            return (await query.ToListAsync()).Select(e => new DTO.PizzaInOrder()
            {
                OrderDescription = e.Order.Description,
                OrderId = e.OrderId,
                PizzaDescription = e.Pizza.Description,
                PizzaId = e.PizzaId
            }).ToList();
        }

        public async Task<DTO.PizzaInOrder> FindAsyncByIdAPI(int id, int userId)
        {
            var e = await RepositoryDbSet
                .Include(p => p.Order)
                .Include(p => p.Pizza)
                .Where(p => p.Order.AppUserId == userId && p.Id == id).FirstOrDefaultAsync();
            return new DTO.PizzaInOrder()
            {
                OrderDescription = e.Order.Description,
                OrderId = e.OrderId,
                PizzaDescription = e.Pizza.Description,
                PizzaId = e.PizzaId
            };
        }
    }
}