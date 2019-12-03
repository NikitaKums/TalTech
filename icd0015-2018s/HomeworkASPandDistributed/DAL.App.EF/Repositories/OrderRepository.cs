using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Order = DAL.App.DTO.DomainLikeDTO.Order;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Order, Domain.Order, AppDbContext>,
        IOrderRepository
    {

        public OrderRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new OrderMapper())
        {
        }

        public override Order Update(Order entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(m => m.Description)
                .ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);

            if (entityInDb == null) return entity;

            entityInDb.OrderCreationTime = entity.OrderCreationTime;
            entityInDb.ShopId = entity.ShopId;
            entityInDb.ShipperId = entity.ShipperId;
            RepositoryDbSet.Update(entityInDb);
            
            entityInDb.Description.SetTranslation(entity.Description);

            return entity;
        }

        public override async Task<List<Order>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Select(e => OrderMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Order> FindAsync(params object[] id)
        {
            var order = await RepositoryDbContext.Set<Domain.Order>().FindAsync(id);
            RepositoryDbContext.Entry(order).State = EntityState.Detached;

            return OrderMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == order.Id)
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<DTO.DomainLikeDTO.Order>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .Where(a => a.ShopId == shopId).AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => OrderMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public async Task<List<Order>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .AsQueryable();
  
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => OrderMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Order> Search(IQueryable<Domain.Order> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                query = query.Where(s => 
                    s.Description.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.Shipper.ShipperName.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ).AsQueryable();
            }
            return query;
        }

        private IQueryable<Domain.Order> Order(IQueryable<Domain.Order> res, string order)
        {
            switch (order)
            {
                case "createdAt_desc":
                    return res.OrderByDescending(s => s.OrderCreationTime);
                case "createdAt":
                    return res.OrderBy(s => s.OrderCreationTime);
                case "shipper_desc":
                    return res.OrderByDescending(s => s.Shipper.ShipperName.Value);
                case "shipper":
                    return res.OrderBy(s => s.Shipper.ShipperName.Value);
                case "description_desc":
                    return res.OrderByDescending(s => s.Description.Value);
                default:
                    return res.OrderBy(s => s.Description.Value);
            }
        }

        public async Task<int> CountOrdersInShop(int? shopId)
        {
            return await RepositoryDbSet
                .Where(p => p.ShopId == shopId).CountAsync();
        }

        public async Task<DTO.DomainLikeDTO.Order> FindAsyncById(int id)
        {
            return OrderMapper.MapFromDomain(await RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInOrder).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(o => o.Id == id).FirstOrDefaultAsync());
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(o => o.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<OrderWithProductCount>> GetAllByShopDTOAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(o => o.ShopId == shopId).AsQueryable();
            
            query = Search(query, search);
            
            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query
                .Select(o => new
                {
                    Id = o.Id,
                    Description = o.Description,
                    OrderCreationTime = o.OrderCreationTime,
                    ShipperId = o.ShipperId,
                    ShipperName = o.Shipper.ShipperName,
                    ShopId = o.ShopId,
                    ShopName = o.Shop.ShopName,
                    ProductsInOrderCount = o.ProductsInOrder.Count,
                    DescriptionTranslations = o.Description.Translations,
                    ShipperNameTranslations = o.Shipper.ShipperName.Translations,
                    ShopNameTranslations = o.Shop.ShopName.Translations
                })
                .ToListAsync();

            var result = res.Select(o => new OrderWithProductCount()
            {
                Id = o.Id,
                Description = o.Description.Translate(),
                OrderCreationTime = o.OrderCreationTime,
                ShipperId = o.ShipperId,
                ShipperName = o.ShipperName.Translate(),
                ShopId = o.ShopId,
                ShopName = o.ShopName.Translate(),
                ProductsInOrderCount = o.ProductsInOrderCount
            }).ToList();

            return result;
        }

        public async Task<OrderWithProductCount> FindByShopAndIdAsync(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shipper).ThenInclude(aa => aa.ShipperName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(o => o.ShopId == shopId && o.Id == id)
                .Select(o => new
                {
                    Id = o.Id,
                    Description = o.Description,
                    OrderCreationTime = o.OrderCreationTime,
                    ShipperId = o.ShipperId,
                    ShipperName = o.Shipper.ShipperName,
                    ShopId = o.ShopId,
                    ShopName = o.Shop.ShopName,
                    ProductsInOrderCount = o.ProductsInOrder.Count,
                    DescriptionTranslations = o.Description.Translations,
                    ShipperNameTranslations = o.Shipper.ShipperName.Translations,
                    ShopNameTranslations = o.Shop.ShopName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new OrderWithProductCount()
            {
                Id = res.Id,
                Description = res.Description.Translate(),
                OrderCreationTime = res.OrderCreationTime,
                ShipperId = res.ShipperId,
                ShipperName = res.ShipperName.Translate(),
                ShopId = res.ShopId,
                ShopName = res.ShopName.Translate(),
                ProductsInOrderCount = res.ProductsInOrderCount
            };

            return result;
        }
    }
    
}