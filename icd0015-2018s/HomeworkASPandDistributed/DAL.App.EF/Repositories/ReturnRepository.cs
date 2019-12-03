using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.IdAndNameDTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Return = DAL.App.DTO.DomainLikeDTO.Return;

namespace DAL.App.EF.Repositories
{
    public class ReturnRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Return, Domain.Return, AppDbContext>,
        IReturnRepository
    {

        public ReturnRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ReturnMapper())
        {
        }

        public override Return Update(Return entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);
            
            if (entityInDb == null) return entity;
            
            entityInDb.ShopId = entity.ShopId;
            RepositoryDbSet.Update(entityInDb);
            
            entityInDb.Description.SetTranslation(entity.Description);

            return entity;
        }

        public override async Task<List<Return>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Select(e => ReturnMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Return> FindAsync(params object[] id)
        {
            var @return = await RepositoryDbSet.FindAsync(id);

            return ReturnMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == @return.Id)
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(r => r.ProductsReturned).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<Return>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ReturnMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public async Task<List<Return>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ReturnMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Return> Search(IQueryable<Domain.Return> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s =>
                    s.Description.Translations.Any(t => t.Value.ToLower().Contains(searchFor))).AsQueryable();
            }
            return query;
        }

        private IQueryable<Domain.Return> Order(IQueryable<Domain.Return> res, string order)
        {
            switch (order)
            {
                case "description_desc":
                    return res.OrderByDescending(s => s.Description.Value);
                default:
                    return res.OrderBy(s => s.Description.Value);
            }
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(r => r.ProductsReturned)
                .Where(r => r.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<ReturnWithProductCount>> GetAllWithProductsReturnedByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(r => r.ProductsReturned)
                .Where(r => r.ShopId == shopId).AsQueryable();
            
            query = Search(query, search);
            
            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query
                .Select(r => new
                {
                    Id = r.Id,
                    Description = r.Description,
                    ShopId = r.ShopId,
                    ShopName = r.Shop.ShopName,
                    ProductsReturnedCount = r.ProductsReturned.Count,
                    DescriptionTranslations = r.Description.Translations,
                    ShopNameTranslations = r.Shop.ShopName.Translations
                })
                .ToListAsync();

            var result = res.Select(r => new ReturnWithProductCount()
            {
                Id = r.Id,
                Description = r.Description.Translate(),
                ShopId = r.ShopId,
                ShopName = r.ShopName.Translate(),
                ProductsReturnedCount = r.ProductsReturnedCount
            }).ToList();

            return result;
        }

        public async Task<ReturnWithProductCount> FindWithProductsReturnedByIdAndShopAsync(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsReturned).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName).ThenInclude(t => t.Translations)
                .Where(r => r.ShopId == shopId && r.Id == id)
                .Select(r => new
                {
                    Id = r.Id,
                    Description = r.Description,
                    ShopId = r.ShopId,
                    ShopName = r.Shop.ShopName,
                    ProductsReturnedCount = r.ProductsReturned.Count,
                    DescriptionTranslations = r.Description.Translations,
                    ShopNameTranslations = r.Shop.ShopName.Translations,
                    ProductIdNameDtos = r.ProductsReturned.Select(rr => new 
                    {
                        Id = rr.ProductId,
                        ProductName = rr.Product.ProductName,
                        ProductNameTranslations = rr.Product.ProductName.Translations,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            var result = new ReturnWithProductCount()
            {
                Id = res.Id,
                Description = res.Description.Translate(),
                ShopId = res.ShopId,
                ShopName = res.ShopName.Translate(),
                ProductsReturnedCount = res.ProductsReturnedCount,
                ProductIdNameDtos = res.ProductIdNameDtos.Select(rr => new ProductIdName()
                {
                    Id = rr.Id,
                    ProductName = rr.ProductName.Translate()
                }).ToList()
            };

            return result;
        }

        public async Task<List<ReturnIdName>> GetAllIdAndDescAsyncByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Where(r => r.ShopId == shopId)
                .Select(r => new 
                {
                    Id = r.Id,
                    ReturnDescription = r.Description,
                    DescriptionTranslation = r.Description.Translations
                })
                .ToListAsync();

            var result = res.Select(r => new ReturnIdName()
            {
                Id = r.Id,
                ReturnDescription = r.ReturnDescription.Translate()
            }).ToList();

            return result;
        }
    }
}