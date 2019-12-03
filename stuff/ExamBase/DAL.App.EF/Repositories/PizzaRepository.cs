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
    public class PizzaRepository : BaseRepository<Pizza, Pizza, AppDbContext>, IPizzaRepository
    {
        public PizzaRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<Pizza>> AllAsyncWithSearch(string search)
        {
            var query = RepositoryDbSet
                .Include(p => p.PizzasInOrder)
                .Include(p => p.ToppingsOnPizza).ThenInclude(pp => pp.Topping)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.Description.ToLower().Contains(search) ||
                                         c.Pirce.ToString().Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Pizza> FindAsyncById(int? id)
        {
            return await RepositoryDbSet
                .Include(p => p.PizzasInOrder)
                .Include(p => p.ToppingsOnPizza)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DTO.Pizza>> AllAsyncWithSearchAPI(string search)
        {
            var query = RepositoryDbSet
                .Include(p => p.PizzasInOrder)
                .Include(p => p.ToppingsOnPizza).ThenInclude(pp => pp.Pizza)
                .Include(p => p.ToppingsOnPizza).ThenInclude(pp => pp.Topping)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.Description.ToLower().Contains(search) ||
                                         c.Pirce.ToString().Contains(search));
            }

            return (await query.ToListAsync()).Select(e => new DTO.Pizza()
            {
                Id = e.Id,
                Description = e.Description,
                Pirce = e.Pirce,
                ToppingsOnPizza = e.ToppingsOnPizza?.Select(a => new DTO.ToppingOnPizza()
                {
                    Id = a.Id,
                    PizzaDescription = a.Pizza.Description,
                    PizzaId = a.PizzaId,
                    ToppingDescription = a.Topping.Description,
                    ToppingId = a.ToppingId
                }).ToList()
            }).ToList();
        }

        public async Task<DTO.Pizza> FindAsyncByIdAPI(int id)
        {
            var e = await RepositoryDbSet
                .Include(p => p.PizzasInOrder)
                .Include(p => p.ToppingsOnPizza).ThenInclude(pp => pp.Pizza)
                .Include(p => p.ToppingsOnPizza).ThenInclude(pp => pp.Topping)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            
            return new DTO.Pizza()
            {
                Id = e.Id,
                Description = e.Description,
                Pirce = e.Pirce,
                ToppingsOnPizza = e.ToppingsOnPizza?.Select(a => new DTO.ToppingOnPizza()
                {
                    Id = a.Id,
                    PizzaDescription = a.Pizza.Description,
                    PizzaId = a.PizzaId,
                    ToppingDescription = a.Topping.Description,
                    ToppingId = a.ToppingId
                }).ToList()
            };
        }
    }
}