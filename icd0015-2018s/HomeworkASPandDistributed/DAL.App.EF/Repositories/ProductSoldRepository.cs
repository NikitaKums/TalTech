using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProductSold = DAL.App.DTO.ProductSold;

namespace DAL.App.EF.Repositories
{
    public class ProductSoldRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.ProductSold, Domain.ProductSold, AppDbContext>,
        IProductSoldRepository
    {

        public ProductSoldRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ProductSoldMapper())
        {
        }

        public override async Task<List<DTO.DomainLikeDTO.ProductSold>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Select(e => ProductSoldMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<DTO.DomainLikeDTO.ProductSold> FindAsync(params object[] id)
        {
            var productSold = await RepositoryDbSet.FindAsync(id);

            return ProductSoldMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == productSold.Id)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<List<DTO.DomainLikeDTO.ProductSold>> AllAsyncByShop(int? shopId)
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(e => ProductSoldMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<int> GetQuantity(int id)
        {
            return await RepositoryDbSet
                .Where(p => p.Id == id)
                .Select(p => p.Quantity).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.DomainLikeDTO.ProductSold>> FindBySaleId(int saleId)
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(s => s.SaleId == saleId)
                .Select(e => ProductSoldMapper.MapFromDomain(e))
                .ToListAsync();
        }

        public async Task<int> CountProductsInSale(int id)
        {
            return await RepositoryDbSet
                .Where(s => s.SaleId == id)
                .CountAsync();
        }

        public async Task<List<ProductSold>> AllAsyncByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    ProductSoldTime = p.ProductSoldTime,
                    Quantity = p.Quantity,
                    SaleId = p.SaleId,
                    SaleDescription = p.Sale.Description,
                    SaleDescriptionTranslations = p.Sale.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(p => new ProductSold()
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ProductName = p.ProductName.Translate(),
                ProductSoldTime = p.ProductSoldTime,
                Quantity = p.Quantity,
                SaleId = p.SaleId,
                SaleDescription = p.SaleDescription.Translate()
            }).ToList();
            
            return result;
        }

        public async Task<ProductSold> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    ProductSoldTime = p.ProductSoldTime,
                    Quantity = p.Quantity,
                    SaleId = p.SaleId,
                    SaleDescription = p.Sale.Description,
                    SaleDescriptionTranslations = p.Sale.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new ProductSold()
            {
                Id = res.Id,
                ProductId = res.ProductId,
                ProductName = res.ProductName.Translate(),
                ProductSoldTime = res.ProductSoldTime,
                Quantity = res.Quantity,
                SaleId = res.SaleId,
                SaleDescription = res.SaleDescription.Translate()
            };
            
            return result;
        }
        
        public async Task<List<ProductSold>> AllAsyncByShopAndSaleId(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(p => p.Sale).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.SaleId == id)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    ProductSoldTime = p.ProductSoldTime,
                    Quantity = p.Quantity,
                    SaleId = p.SaleId,
                    SaleDescription = p.Sale.Description,
                    SaleDescriptionTranslations = p.Sale.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(e => new ProductSold()
            {
                Id = e.Id,
                ProductId = e.ProductId,
                ProductName = e.ProductName.Translate(),
                ProductSoldTime = e.ProductSoldTime,
                Quantity = e.Quantity,
                SaleId = e.SaleId,
                SaleDescription = e.SaleDescription.Translate()
            }).ToList();
            
            return result;
        }
    }
}