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
    public class DrinkInOrderRepository : BaseRepository<DrinkInOrder, DrinkInOrder, AppDbContext>, IDrinkInOrderRepository
    {
        public DrinkInOrderRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<DrinkInOrder>> AllAsyncByUserId(int userId)
        {
            var query = RepositoryDbSet
                .Include(d => d.Drink)
                .Include(d => d.Order)
                .Where(d => d.Order.AppUserId == userId).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<DrinkInOrder> FindAsyncById(int? id, int userId)
        {
            return await RepositoryDbSet
                .Include(d => d.Drink)
                .Include(d => d.Order)
                .Where(d => d.Order.AppUserId == userId && d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.DrinkInOrder>> AllAsyncWithSearchAPI(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(d => d.Drink)
                .Include(d => d.Order)
                .Where(d => d.Order.AppUserId == userId).AsQueryable();

            return (await query.ToListAsync()).Select(e => new DTO.DrinkInOrder()
            {
                Id = e.Id,
                DrinkDescription = e.Drink.Description,
                DrinkId = e.DrinkId,
                OrderDescription = e.Order.Description,
                OrderId = e.OrderId
            }).ToList();
            
        }

        public async Task<DTO.DrinkInOrder> FindAsyncByIdAPI(int id, int userId)
        {
            var e = await RepositoryDbSet
                .Include(d => d.Drink)
                .Include(d => d.Order)
                .Where(d => d.Order.AppUserId == userId && d.Id == id).FirstOrDefaultAsync();
            
            return new DTO.DrinkInOrder()
            {
                Id = e.Id,
                DrinkDescription = e.Drink.Description,
                DrinkId = e.DrinkId,
                OrderDescription = e.Order.Description,
                OrderId = e.OrderId
            };
        }
    }
}