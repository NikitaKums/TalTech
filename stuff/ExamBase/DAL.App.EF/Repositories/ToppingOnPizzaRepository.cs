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
    public class ToppingOnPizzaRepository : BaseRepository<ToppingOnPizza, ToppingOnPizza, AppDbContext>, IToppingOnPizzaRepository
    {
        public ToppingOnPizzaRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<ToppingOnPizza>> AllAsyncWithSearch(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(p => p.Topping)
                .Include(p => p.Pizza).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<ToppingOnPizza> FindAsyncById(int? id, int userId)
        {
            return await RepositoryDbSet
                .Include(p => p.Topping)
                .Include(p => p.Pizza)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.ToppingOnPizza>> AllAsyncWithSearchAPI(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(p => p.Topping)
                .Include(p => p.Pizza).AsQueryable();

            return (await query.ToListAsync()).Select(e => new DTO.ToppingOnPizza()
            {
                Id = e.Id,
                PizzaDescription = e.Pizza.Description,
                ToppingDescription = e.Topping.Description,
                PizzaId = e.PizzaId,
                ToppingId = e.ToppingId
            }).ToList();
        }

        public async Task<DTO.ToppingOnPizza> FindAsyncByIdAPI(int id, int userId)
        {
            var e = await RepositoryDbSet
                .Include(p => p.Topping)
                .Include(p => p.Pizza)
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            return new DTO.ToppingOnPizza()
            {
                Id = e.Id,
                PizzaDescription = e.Pizza.Description,
                ToppingDescription = e.Topping.Description,
                PizzaId = e.PizzaId,
                ToppingId = e.ToppingId
            };
        }
    }
}