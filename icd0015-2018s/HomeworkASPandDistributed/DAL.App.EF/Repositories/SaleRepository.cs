using System;
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
using Sale = DAL.App.DTO.DomainLikeDTO.Sale;

namespace DAL.App.EF.Repositories
{
    public class SaleRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Sale, Domain.Sale, AppDbContext>,
        ISaleRepository
    {

        public SaleRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new SaleMapper())
        {
        }

        public override Sale Update(Sale entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);
            
            if (entityInDb == null) return entity;
            
            entityInDb.AppUserId = entity.AppUserId;
            entityInDb.SaleInitialCreationTime = entity.SaleInitialCreationTime;
            RepositoryDbSet.Update(entityInDb);
            
            entityInDb.Description.SetTranslation(entity.Description);

            return entity;
        }

        public override async Task<List<Sale>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Select(e => SaleMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Sale> FindAsync(params object[] id)
        {
            var sale = await RepositoryDbSet.FindAsync(id);

            return SaleMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == sale.Id)
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<DTO.DomainLikeDTO.Sale>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .Where(p => p.AppUser.ShopId == shopId)
                .AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => SaleMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public async Task<List<Sale>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => SaleMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Sale> Search(IQueryable<Domain.Sale> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s => 
                    s.Description.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.AppUser.FirstName.ToLower().Contains(searchFor) ||
                    s.AppUser.LastName.ToLower().Contains(searchFor) ||
                    s.AppUser.Email.ToLower().Contains(searchFor)
                );
            }
            return query;
        }

        private IQueryable<Domain.Sale> Order(IQueryable<Domain.Sale> res, string order)
        {
            switch (order)
            {
                case "createdAt_desc":
                    return res.OrderByDescending(s => s.SaleInitialCreationTime);
                case "createdAt":
                    return res.OrderBy(s => s.SaleInitialCreationTime);
                case "user_desc":
                    return res.OrderByDescending(s => s.AppUser.FirstName);
                case "user":
                    return res.OrderBy(s => s.AppUser.FirstName);
                case "description_desc":
                    return res.OrderByDescending(s => s.Description.Value);
                default:
                    return res.OrderBy(s => s.Description.Value);
            }
        }

        public async Task<List<DTO.DomainLikeDTO.Sale>> AllAsyncByShopAndUserId(int? shopId, int userId)
        {
            return await RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Where(p => p.AppUser.ShopId == shopId)
                .Where(p => p.AppUserId == userId)
                .Select(e => SaleMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<List<SaleWithProductCount>> GetAsyncByShopAndUserIdDTO(int userId, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(s => s.ProductsSold).ThenInclude(p => p.Sale).ThenInclude(pp => pp.Description).ThenInclude(t => t.Translations)
                .Where(s => s.AppUser.ShopId == shopId && s.AppUserId == userId)
                .Select(s => new 
                {
                    Id = s.Id,
                    Description = s.Description,
                    DescriptionTranslations = s.Description.Translations
                })
                .ToListAsync();

            var result = res.Select(s => new SaleWithProductCount()
            { 
                Id = s.Id,
                Description = s.Description.Translate()
            }).ToList();

            return result;
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .Where(s => s.AppUser.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }
        
        public virtual async Task<Dictionary<string, decimal?>> GetSaleAmounts(int? shopId)
        {
            var query = shopId != null
                ? await RepositoryDbSet
                    .Include(a => a.AppUser)
                    .Include(a => a.ProductsSold).ThenInclude(a => a.Product)
                    .Where(s => s.AppUser.ShopId == shopId).ToListAsync()
                : await RepositoryDbSet
                    .Include(a => a.AppUser)
                    .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ToListAsync();

            var res = new Dictionary<string, decimal?>
            {
                {"overAllTotalSaleAmount", query.Sum(p => p.ProductsSold.Sum(e => e.Quantity * e.Product.SellPrice))},
                {
                    "overAllTodayTotalSaleAmount", query.Sum(p => p.ProductsSold
                        .Where(e => DateTime.Today < e.ProductSoldTime &&
                                    e.ProductSoldTime < DateTime.Today.AddDays(1).AddTicks(-1))
                        .Sum(e => e.Quantity * e.Product.SellPrice))
                }
            };
            if (res["overAllTotalSaleAmount"] == null)
            {
                res["overAllTotalSaleAmount"] = 0;
            }
            if (res["overAllTodayTotalSaleAmount"] == null)
            {
                res["overAllTodayTotalSaleAmount"] = 0;
            }
            return res;
        }

        public async Task<List<SaleWithProductCount>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode)
                .ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode)
                .ThenInclude(t => t.Translations)
                .Where(s => s.AppUser.ShopId == shopId).AsQueryable();
            
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
                    Description = s.Description,
                    AppUserId = s.AppUserId,
                    AppUserName = s.AppUser.FirstName,
                    AppUserLastName = s.AppUser.LastName,
                    SaleInitialCreationTime = s.SaleInitialCreationTime,
                    ProductsSoldCount = s.ProductsSold.Count,
                    DescriptionTranslations = s.Description.Translations,
                    ProductsInSaleDTOs = s.ProductsSold.Select(p => new 
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        ProductName = p.Product.ProductName,
                        SaleId = p.SaleId,
                        SaleDescription = p.Sale.Description,
                        SaleDescriptionTranslations = p.Sale.Description.Translations,
                        ProductNameTranslations = p.Product.ProductName.Translations
                    }).ToList(),
                    AllTotalSaleAmount = s.ProductsSold.Sum(e => e.Quantity * e.Product.SellPrice),
                    TodayTotalSaleAmount = s.ProductsSold.Where(e => DateTime.Today < e.ProductSoldTime && e.ProductSoldTime < DateTime.Today.AddDays(1).AddTicks(-1))
                    .Sum(e => e.Quantity * e.Product.SellPrice)
                })
                .ToListAsync();

            var result = res.Select(s => new SaleWithProductCount()
            {
                Id = s.Id,
                Description = s.Description.Translate(),
                AppUserId = s.AppUserId,
                AppUserName = s.AppUserName,
                AppUserLastName = s.AppUserLastName,
                SaleInitialCreationTime = s.SaleInitialCreationTime,
                ProductsSoldCount = s.ProductsSoldCount,
                ProductsInSaleDTOs = s.ProductsInSaleDTOs.Select(p => new ProductSoldProductSaleIdName()
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName.Translate(),
                    SaleId = p.SaleId,
                    SaleDescription = p.SaleDescription.Translate()
                }).ToList(),
                AllTotalSaleAmount = s.AllTotalSaleAmount,
                TodayTotalSaleAmount = s.TodayTotalSaleAmount
            }).ToList();

            return result;
        }

        public async Task<SaleWithProductCount> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Description).ThenInclude(t => t.Translations)
                .Include(a => a.AppUser)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsSold).ThenInclude(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(s => s.AppUser.ShopId == shopId && s.Id == id)
                .Select(s => new
                {
                    Id = s.Id,
                    Description = s.Description,
                    AppUserId = s.AppUserId,
                    SaleInitialCreationTime = s.SaleInitialCreationTime,
                    ProductsSoldCount = s.ProductsSold.Count,
                    DescriptionTranslations = s.Description.Translations,
                    ProductsInSaleDTOs = s.ProductsSold.Select(p => new 
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        ProductName = p.Product.ProductName,
                        SaleId = p.SaleId,
                        SaleDescription = p.Sale.Description,
                        SaleDescriptionTranslations = p.Sale.Description.Translations,
                        ProductNameTranslations = p.Product.ProductName.Translations
                    }).ToList(),
                    
                })
                .FirstOrDefaultAsync();

            var result = new SaleWithProductCount()
            {
                Id = res.Id,
                Description = res.Description.Translate(),
                AppUserId = res.AppUserId,
                SaleInitialCreationTime = res.SaleInitialCreationTime,
                ProductsSoldCount = res.ProductsSoldCount,
                ProductsInSaleDTOs = res.ProductsInSaleDTOs.Select(p => new ProductSoldProductSaleIdName()
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName.Translate(),
                    SaleId = p.SaleId,
                    SaleDescription = p.SaleDescription.Translate()
                }).ToList()
            };

            return result;
        }
    }
}