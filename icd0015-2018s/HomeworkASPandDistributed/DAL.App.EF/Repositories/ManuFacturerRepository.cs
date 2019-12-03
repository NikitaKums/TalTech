using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using ManuFacturer = DAL.App.DTO.DomainLikeDTO.ManuFacturer;

namespace DAL.App.EF.Repositories
{
    public class ManuFacturerRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.ManuFacturer, Domain.ManuFacturer, AppDbContext>,
        IManuFacturerRepository
    {

        public ManuFacturerRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ManuFacturerMapper())
        {
        }
        
        public override async Task<List<ManuFacturer>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations).Select(e => ManuFacturerMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<ManuFacturer> FindAsync(params object[] id)
        {
            var manuFacturer = await RepositoryDbSet.FindAsync(id);
            
            return ManuFacturerMapper.MapFromDomain(await RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations).Where(m => m.Id == manuFacturer.Id).FirstOrDefaultAsync());
        }

        public override ManuFacturer Update(ManuFacturer entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);

            if (entityInDb == null) return entity;
            
            entityInDb.ManuFacturerName.SetTranslation(entity.ManuFacturerName);
            entityInDb.Aadress.SetTranslation(entity.Aadress);
            entityInDb.PhoneNumber.SetTranslation(entity.PhoneNumber);

            return entity;
        }

        public async Task<ManuFacturerWithProductCount> FindByIdAndShop(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Products)
                .Select(m => new 
                {
                    Id = m.Id,
                    Aadress = m.Aadress,
                    ManuFacturerName = m.ManuFacturerName,
                    PhoneNumber = m.PhoneNumber,
                    ProductCount = m.Products.Count(p => p.ShopId == shopId),
                    ManuFacturerNameTranslations = m.ManuFacturerName.Translations,
                    AddressTranslations = m.Aadress.Translations,
                    PhoneNumberTranslations = m.PhoneNumber.Translations
                })
                .Where(m => m.Id == id).FirstOrDefaultAsync();

            var result = new ManuFacturerWithProductCount()
            {
                Id = res.Id,
                Aadress = res.Aadress.Translate(),
                ManuFacturerName = res.ManuFacturerName.Translate(),
                PhoneNumber = res.PhoneNumber.Translate(),
                ProductCount = res.ProductCount
            };

            return result;
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Products).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<ManuFacturerWithProductCount>> GetAllWithProductCountAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Products).AsQueryable();

            query = Search(query, search);
            
            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }

            var res = await query
                .Select(m => new
                {
                    Id = m.Id,
                    Aadress = m.Aadress,
                    ManuFacturerName = m.ManuFacturerName,
                    PhoneNumber = m.PhoneNumber,
                    ProductCount = m.Products.Count(p => p.ShopId == shopId),
                    ManuFacturerNameTranslations = m.ManuFacturerName.Translations,
                    AddressTranslations = m.Aadress.Translations,
                    PhoneNumberTranslations = m.PhoneNumber.Translations
                }).ToListAsync();

            var result = res.Select(m => new ManuFacturerWithProductCount()
            {
                Id = m.Id,
                Aadress = m.Aadress.Translate(),
                ManuFacturerName = m.ManuFacturerName.Translate(),
                PhoneNumber = m.PhoneNumber.Translate(),
                ProductCount = m.ProductCount
            }).ToList();

            return result;
        }

        public async Task<List<ManuFacturer>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(m => m.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Products).AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ManuFacturerMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.ManuFacturer> Search(IQueryable<Domain.ManuFacturer> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s =>
                    s.ManuFacturerName.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.PhoneNumber.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.Aadress.Translations.Any(t => t.Value.ToLower().Contains(searchFor))).AsQueryable();
            }

            return query;
        }

        private IQueryable<Domain.ManuFacturer> Order(IQueryable<Domain.ManuFacturer> res, string order)
        {
            switch (order)
            {
                case "address_desc":
                    return res.OrderByDescending(s => s.Aadress.Value);
                case "address":
                    return res.OrderBy(s => s.Aadress.Value);
                case "number_desc":
                    return res.OrderByDescending(s => s.PhoneNumber.Value);
                case "number":
                    return res.OrderBy(s => s.PhoneNumber.Value);
                case "name_desc":
                    return res.OrderByDescending(s => s.ManuFacturerName.Value);
                default:
                    return res.OrderBy(s => s.ManuFacturerName.Value);
            }
        }
    }
}