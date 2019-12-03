using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shipper = DAL.App.DTO.DomainLikeDTO.Shipper;

namespace DAL.App.EF.Repositories
{
    public class ShipperRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Shipper, Domain.Shipper, AppDbContext>,
        IShipperRepository
    {
        public ShipperRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ShipperMapper())
        {
        }

        public override async Task<List<Shipper>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations)
                .Select(e => ShipperMapper.MapFromDomain(e)).ToListAsync();
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public override async Task<Shipper> FindAsync(params object[] id)
        {
            var shipper = await RepositoryDbSet.FindAsync(id);

            return ShipperMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == shipper.Id)
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public override Shipper Update(Shipper entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);
            
            if (entityInDb == null) return entity;

            entityInDb.ShipperName.SetTranslation(entity.ShipperName);
            entityInDb.ShipperAddress.SetTranslation(entity.ShipperAddress);
            entityInDb.PhoneNumber.SetTranslation(entity.PhoneNumber);

            return entity;
        }

        public async Task<List<ShipperWithOrderCount>> GetAllWithOrdersCountByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations).AsQueryable();
            
            query = Search(query, search);
            
            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query
                .Select(s => new 
                {
                    Id = s.Id,
                    ShipperName = s.ShipperName,
                    ShipperAddress = s.ShipperAddress,
                    PhoneNumber = s.PhoneNumber,
                    OrdersCount = s.Orders.Count(ss => ss.ShopId == shopId),
                    ShipperNameTranslations = s.ShipperName.Translations,
                    ShipperAddressTranslations = s.ShipperAddress.Translations,
                    PhoneNumberTranslations = s.PhoneNumber.Translations
                })
                .ToListAsync();

            var result = res.Select(s => new ShipperWithOrderCount()
            {
                Id = s.Id,
                ShipperName = s.ShipperName.Translate(),
                ShipperAddress = s.ShipperAddress.Translate(),
                PhoneNumber = s.PhoneNumber.Translate(),
                OrdersCount = s.OrdersCount
            }).ToList();

            return result;
        }

        public async Task<ShipperWithOrderCount> FindByIdAsyncDTO(int id)
        {
            var res = await RepositoryDbSet
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations)
                .Where(s => s.Id == id)
                .Select(s => new 
                {
                    Id = s.Id,
                    ShipperName = s.ShipperName,
                    ShipperAddress = s.ShipperAddress,
                    PhoneNumber = s.PhoneNumber,
                    OrdersCount = s.Orders.Count,
                    ShipperNameTranslations = s.ShipperName.Translations,
                    ShipperAddressTranslations = s.ShipperAddress.Translations,
                    PhoneNumberTranslations = s.PhoneNumber.Translations
                })
                .FirstOrDefaultAsync();

            var result = new ShipperWithOrderCount()
            {
                Id = res.Id,
                ShipperName = res.ShipperName.Translate(),
                ShipperAddress = res.ShipperAddress.Translate(),
                PhoneNumber = res.PhoneNumber.Translate(),
                OrdersCount = res.OrdersCount
            };

            return result;
        }

        public async Task<List<Shipper>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.PhoneNumber).ThenInclude(t => t.Translations)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ShipperMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Shipper> Search(IQueryable<Domain.Shipper> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s =>
                    s.ShipperName.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.PhoneNumber.Translations.Any(t => t.Value.ToLower().Contains(searchFor))||
                    s.ShipperAddress.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ).AsQueryable();
            }

            return query;
        }

        private IQueryable<Domain.Shipper> Order(IQueryable<Domain.Shipper> res, string order)
        {
            switch (order)
            {
                case "address_desc":
                    return res.OrderByDescending(s => s.ShipperAddress.Value);
                case "address":
                    return res.OrderBy(s => s.ShipperAddress.Value);
                case "number_desc":
                    return res.OrderByDescending(s => s.PhoneNumber.Value);
                case "number":
                    return res.OrderBy(s => s.PhoneNumber.Value);
                case "name_desc":
                    return res.OrderByDescending(s => s.ShipperName.Value);
                default:
                    return res.OrderBy(s => s.ShipperName.Value);
            }
        }
        
    }
}