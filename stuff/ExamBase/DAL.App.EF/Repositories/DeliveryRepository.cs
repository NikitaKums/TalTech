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
    public class DeliveryRepository : BaseRepository<Delivery, Delivery, AppDbContext>, IDeliveryRepository
    {
        public DeliveryRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<Delivery>> AllAsyncWithSearch(string search)
        {
            var query = RepositoryDbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.Description.ToLower().Contains(search) ||
                                         c.DeliveryPrice.ToString().Equals(search) ||
                                         c.DeliveryService.ToLower().Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Delivery> FindAsyncById(int? id)
        {
            return await RepositoryDbSet.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.Delivery>> AllAsyncWithSearchAPI(string search)
        {
            var query = RepositoryDbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.Description.ToLower().Contains(search) ||
                                         c.DeliveryPrice.ToString().Equals(search) ||
                                         c.DeliveryService.ToLower().Contains(search));
            }

            return (await query.ToListAsync()).Select(e => new DTO.Delivery()
            {
                Description = e.Description,
                DeliveryPrice = e.DeliveryPrice,
                DeliveryService = e.DeliveryService,
                Id = e.Id,
            }).ToList();
            
        }

        public async Task<DTO.Delivery> FindAsyncByIdAPI(int id)
        {
            var e = await RepositoryDbSet.Where(d => d.Id == id).FirstOrDefaultAsync();
            return new DTO.Delivery()
            {
                Description = e.Description,
                DeliveryPrice = e.DeliveryPrice,
                DeliveryService = e.DeliveryService,
                Id = e.Id,
            };
        }
    }
}