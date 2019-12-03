using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO.IdAndNameDTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProductInCategory = DAL.App.DTO.DomainLikeDTO.ProductInCategory;

namespace DAL.App.EF.Repositories
{
    public class ProductInCategoryRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.ProductInCategory, Domain.ProductInCategory, AppDbContext>,
        IProductInCategoryRepository
    {

        public ProductInCategoryRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ProductInCategoryMapper())
        {
        }

        public override async Task<List<ProductInCategory>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(p => p.Category).ThenInclude(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Select(e => ProductInCategoryMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<ProductInCategory> FindAsync(params object[] id)
        {
            var productInCategory = await RepositoryDbSet.FindAsync(id);

            return ProductInCategoryMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == productInCategory.Id)
                .Include(p => p.Category).ThenInclude(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<List<ProductInCategory>> AllAsyncByShop(int? shopId)
        {
            return await RepositoryDbSet
                .Include(p => p.Category).ThenInclude(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(e => ProductInCategoryMapper.MapFromDomain(e))
                .ToListAsync();
        }

        public async Task<List<DTO.ProductInCategory>> AllAsyncByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(p => p.Category).ThenInclude(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(p => new
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    CategoryNameTranslations = p.Category.CategoryName.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(p => new DTO.ProductInCategory()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName.Translate(),
                ProductId = p.ProductId,
                ProductName = p.ProductName.Translate()
            }).ToList();

            return result;
        }

        public async Task<DTO.ProductInCategory> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(p => p.Category).ThenInclude(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    CategoryNameTranslations = p.Category.CategoryName.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new DTO.ProductInCategory()
            {
                Id = res.Id,
                CategoryId = res.CategoryId,
                CategoryName = res.CategoryName.Translate(),
                ProductId = res.ProductId,
                ProductName = res.ProductName.Translate()
            };

            return result;
        }

        public async Task<List<ProductInCategory>> AllAsyncByCategoryId(int categoryId)
        {
            return await RepositoryDbSet
                .Where(a => a.CategoryId == categoryId).Select(e => ProductInCategoryMapper.MapFromDomain(e)).ToListAsync();
        }
        
        public async Task<List<DAL.App.DTO.IdAndNameDTO.ProductIdName>> AllAsyncByCategoryIdAndShopId(int categoryId, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(s => s.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(s => s.Category)
                .Where(a => a.CategoryId == categoryId && a.Product.ShopId == shopId)
                .Select(e => new
                {
                    ProductId = e.ProductId,
                    ProductName = e.Product.ProductName,
                    ProductInCategoryId = e.Id,
                    ProductNameTranslations = e.Product.ProductName.Translations
                }).ToListAsync();

            return res.Select(e => new ProductIdName
            {
                ProductName = e.ProductName.Translate(),
                Id = e.ProductId,
                ProductInCategoryId = e.ProductInCategoryId
            }).ToList();
        }
    }
}