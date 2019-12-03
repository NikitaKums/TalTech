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
using Shop = DAL.App.DTO.DomainLikeDTO.Shop;

namespace DAL.App.EF.Repositories
{
    public class ShopRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Shop, Domain.Shop, AppDbContext>,
        IShopRepository
    {

        public ShopRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ShopMapper())
        {
        }

        public override async Task<List<Shop>> AllAsync()
        {
            return await RepositoryDbSet.Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations).Select(e => ShopMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Shop> FindAsync(params object[] id)
        {
            var shop = await RepositoryDbSet.FindAsync(id);

            return ShopMapper.MapFromDomain(await RepositoryDbSet
                .Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations)               
                .Where(a => a.Id == shop.Id)
                .FirstOrDefaultAsync());
        }

        public override Shop Update(Shop entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);
            
            if (entityInDb == null) return entity;

            entityInDb.ShopName.SetTranslation(entity.ShopName);
            entityInDb.ShopAddress.SetTranslation(entity.ShopAddress);
            entityInDb.ShopContact.SetTranslation(entity.ShopContact);
            entityInDb.ShopContact2.SetTranslation(entity.ShopContact2);

            return entity;
        }

        public virtual async Task<List<Shop>> GetShopByUserShopId(int? shopId)
        {
            return await RepositoryDbSet
                .Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Include(s => s.Inventories).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Include(s => s.Orders).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Products).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)              
                .Include(s => s.Returns).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Include(d => d.Defects).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Include(s => s.AppUsers)
                .Where(s => s.Id == shopId).Select(e => ShopMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet.Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<Shop>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet.Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ShopMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Shop> Search(IQueryable<Domain.Shop> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s =>
                    s.ShopName.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.ShopAddress.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.ShopContact.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.ShopContact2.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ).AsQueryable();
            }

            return query;
        }

        private IQueryable<Domain.Shop> Order(IQueryable<Domain.Shop> res, string order)
        {
            switch (order)
            {
                case "address_desc":
                    return res.OrderByDescending(s => s.ShopAddress.Value);
                case "address":
                    return res.OrderBy(s => s.ShopAddress.Value);
                case "contact_desc":
                    return res.OrderByDescending(s => s.ShopContact.Value);
                case "contact":
                    return res.OrderBy(s => s.ShopContact.Value);
                case "contact2_desc":
                    return res.OrderByDescending(s => s.ShopContact2.Value);
                case "contact2":
                    return res.OrderBy(s => s.ShopContact2.Value);
                case "name_desc":
                    return res.OrderByDescending(s => s.ShopName.Value);
                default:
                    return res.OrderBy(s => s.ShopName.Value);
            }
        }

        public virtual async Task<List<Shop>> GetShopByUserShopIdForDropDown(int? shopId)
        {
            return await RepositoryDbSet
                .Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Where(s => s.Id == shopId).Select(e => ShopMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<ShopWithCounts> GetShopByShopId(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Include(s => s.Inventories)
                .Include(s => s.Orders)
                .Include(s => s.Products)
                .Include(s => s.Returns)
                .Include(s => s.Defects)
                .Include(s => s.AppUsers)
                .Where(s => s.Id == shopId)
                .Select(a => new 
                    {
                        Id = a.Id,
                        ShopName = a.ShopName,
                        ShopAddress = a.ShopAddress,
                        ShopContact = a.ShopContact,
                        ShopContact2 = a.ShopContact2,
                        InventoryId = a.Inventories.Select(s => s.Id).FirstOrDefault(),
                        OrdersCount = a.Orders.Select(s => s.ProductsInOrder).Count(),
                        ProductsCount = a.Products.Count,
                        ReturnsCount = a.Returns.Select(s => s.ProductsReturned).Count(),
                        DefectsCount = a.Defects.Select(s => s.ProductsWithDefect).Count(),
                        AppUsersCount = a.AppUsers.Count,
                        ShopNameTranslations = a.ShopName.Translations,
                        ShopAddressTranslations = a.ShopAddress.Translations,
                        ShopContactTranslations = a.ShopContact.Translations,
                        ShopContact2Translations = a.ShopContact2.Translations
                    }
                ).FirstOrDefaultAsync();

            var result = new ShopWithCounts()
            {
                Id = res.Id,
                ShopName = res.ShopName.Translate(),
                ShopAddress = res.ShopAddress.Translate(),
                ShopContact = res.ShopContact.Translate(),
                ShopContact2 = res.ShopContact2.Translate(),
                InventoryId = res.InventoryId,
                OrdersCount = res.OrdersCount,
                ProductsCount = res.ProductsCount,
                ReturnsCount = res.ReturnsCount,
                DefectsCount = res.DefectsCount,
                AppUsersCount = res.AppUsersCount
            };

            return result;
        }

        public async Task<List<ShopWithCounts>> GetAllWithCountsAsync()
        {
            var res = await RepositoryDbSet
                .Include(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Include(s => s.Inventories)
                .Include(s => s.Orders)
                .Include(s => s.Products)
                .Include(s => s.Returns)
                .Include(s => s.Defects)
                .Include(s => s.AppUsers)
                .Select(a => new 
                    {
                        Id = a.Id,
                        ShopName = a.ShopName,
                        ShopAddress = a.ShopAddress,
                        ShopContact = a.ShopContact,
                        ShopContact2 = a.ShopContact2,
                        InventoryId = a.Inventories.Select(s => s.Id).FirstOrDefault(),
                        OrdersCount = a.Orders.Select(s => s.ProductsInOrder).Count(),
                        ProductsCount = a.Products.Count,
                        ReturnsCount = a.Returns.Select(s => s.ProductsReturned).Count(),
                        DefectsCount = a.Defects.Select(s => s.ProductsWithDefect).Count(),
                        AppUsersCount = a.AppUsers.Count,
                        ShopNameTranslations = a.ShopName.Translations,
                        ShopAddressTranslations = a.ShopAddress.Translations,
                        ShopContactTranslations = a.ShopContact.Translations,
                        ShopContact2Translations = a.ShopContact2.Translations
                    }
                ).ToListAsync();

            var result = res.Select(e => new ShopWithCounts()
            {
                Id = e.Id,
                ShopName = e.ShopName.Translate(),
                ShopAddress = e.ShopAddress.Translate(),
                ShopContact = e.ShopContact.Translate(),
                ShopContact2 = e.ShopContact2.Translate(),
                InventoryId = e.InventoryId,
                OrdersCount = e.OrdersCount,
                ProductsCount = e.ProductsCount,
                ReturnsCount = e.ReturnsCount,
                DefectsCount = e.DefectsCount,
                AppUsersCount = e.AppUsersCount
            }).ToList();

            return result;
        }
    }
}