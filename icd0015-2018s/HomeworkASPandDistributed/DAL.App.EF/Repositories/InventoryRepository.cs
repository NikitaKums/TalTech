using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Inventory = DAL.App.DTO.DomainLikeDTO.Inventory;

namespace DAL.App.EF.Repositories
{
    public class InventoryRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Inventory, Domain.Inventory, AppDbContext>,
        IInventoryRepository
    {

        public InventoryRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new InventoryMapper())
        {
        }

        public override Inventory Update(Inventory entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(m => m.Description)
                .ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);

            if (entityInDb == null) return entity;
            
            entityInDb.ShopId = entity.ShopId;
            entityInDb.InventoryCreationTime = entity.InventoryCreationTime;
            RepositoryDbSet.Update(entityInDb);
            
            entityInDb?.Description.SetTranslation(entity.Description);
            
            return entity;
        }

        public override async Task<List<Inventory>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Select(e => InventoryMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Inventory> FindAsync(params object[] id)
        {
            var inventory = await RepositoryDbSet.FindAsync(id);

            return InventoryMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == inventory.Id)
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<bool> IsInventoryEmpty(int id)
        {
            var temp = await RepositoryDbSet
                .Include(i => i.Products)
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    res = i.Products.Count
                }).FirstOrDefaultAsync();

            return temp.res == 0;
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<Inventory>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId).AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => InventoryMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        public async Task<List<Inventory>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => InventoryMapper.MapFromDomain(e)).ToList();
            return res;
            
        }
        
        private IQueryable<Domain.Inventory> Search(IQueryable<Domain.Inventory> query, string searchFor)
        {
            if (searchFor != null)
            {
                searchFor = searchFor.ToLower();
                query = query.Where(s =>
                    s.Description.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ).AsQueryable();
            }

            return query;
        }

        private IQueryable<Domain.Inventory> Order(IQueryable<Domain.Inventory> res, string order)
        {
            switch (order)
            {
                case "createdAt_desc":
                    return res.OrderByDescending(s => s.InventoryCreationTime);
                case "createdAt":
                    return res.OrderBy(s => s.InventoryCreationTime);
                case "description_desc":
                    return res.OrderByDescending(s => s.Description.Value);
                default:
                    return res.OrderBy(s => s.Description.Value);
            }
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(i => i.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<InventoryWithProductCount>> GetByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(i => i.ShopId == shopId).AsQueryable();
            
            query = Search(query, search);
            
            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query
                .Select(i => new
                {
                    Id = i.Id,
                    Description = i.Description,
                    InventoryCreationTime = i.InventoryCreationTime,
                    ShopId = i.ShopId,
                    ShopName = i.Shop.ShopName,
                    ProductCount = i.Products.Count,
                    DescriptionTranslation = i.Description.Translations,
                    ShopNameTranslations = i.Shop.ShopName.Translations
                })
                .ToListAsync();

            var result = res.Select(i => new InventoryWithProductCount()
            {
                Id = i.Id,
                Description = i.Description.Translate(),
                InventoryCreationTime = i.InventoryCreationTime,
                ShopId = i.ShopId,
                ShopName = i.ShopName.Translate(),
                ProductCount = i.ProductCount
            }).ToList();

            return result;
        }

        public async Task<InventoryWithProductCount> FindByShopAsyncAndIdAsync(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(i => i.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(i => i.ShopId == shopId && i.Id == id)
                .Select(i => new
                {
                    Id = i.Id,
                    Description = i.Description,
                    InventoryCreationTime = i.InventoryCreationTime,
                    ShopId = i.ShopId,
                    ShopName = i.Shop.ShopName,
                    ProductCount = i.Products.Count,
                    DescriptionTranslation = i.Description.Translations,
                    ShopNameTranslations = i.Shop.ShopName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new InventoryWithProductCount()
            {
                Id = res.Id,
                Description = res.Description.Translate(),
                InventoryCreationTime = res.InventoryCreationTime,
                ShopId = res.ShopId,
                ShopName = res.ShopName.Translate(),
                ProductCount = res.ProductCount
            };

            return result;
        }
    }
}