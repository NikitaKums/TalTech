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
    public class DrinkRepository : BaseRepository<Drink, Drink, AppDbContext>, IDrinkRepository
    {
        public DrinkRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<Drink>> AllAsyncWithSearch(string search)
        {
            var query = RepositoryDbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.Description.ToLower().Contains(search) ||
                                         c.Pirce.ToString().Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Drink> FindAsyncById(int? id)
        {
            return await RepositoryDbSet.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.Drink>> AllAsyncWithSearchAPI(string search)
        {
            var query = RepositoryDbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.Description.ToLower().Contains(search) ||
                                         c.Pirce.ToString().Contains(search));
            }

            return (await query.ToListAsync()).Select(e => new DTO.Drink()
            {
                Id = e.Id,
                Description = e.Description,
                Pirce = e.Pirce
            }).ToList();
        }

        public async Task<DTO.Drink> FindAsyncByIdAPI(int id)
        {
            var temp = await RepositoryDbSet.Where(d => d.Id == id).FirstOrDefaultAsync();
            
            return new DTO.Drink()
            {
                Id = temp.Id,
                Description = temp.Description,
                Pirce = temp.Pirce
            };
        }
    }
}