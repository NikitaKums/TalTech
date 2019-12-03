using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProductReturned = DAL.App.DTO.ProductReturned;

namespace DAL.App.EF.Repositories
{
    public class ProductReturnedRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.ProductReturned, Domain.ProductReturned, AppDbContext>,
        IProductReturnedRepository
    {

        public ProductReturnedRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ProductReturnedMapper())
        {
        }

        public override async Task<List<DTO.DomainLikeDTO.ProductReturned>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Return).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Select(e => ProductReturnedMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<DTO.DomainLikeDTO.ProductReturned> FindAsync(params object[] id)
        {
            var productReturned = await RepositoryDbSet.FindAsync(id);

            return ProductReturnedMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == productReturned.Id)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Return).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<List<DTO.DomainLikeDTO.ProductReturned>> AllAsyncByShop(int? shopId)
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Return).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(e => ProductReturnedMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<int> CountProductsInReturn(int returnId)
        {
            return await RepositoryDbSet
                .Where(r => r.ReturnId == returnId).CountAsync();
        }

        public async Task<List<ProductReturned>> AllAsyncByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(p => p.Return).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(p => new 
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    ProductReturnedTime = p.ProductReturnedTime,
                    Quantity = p.Quantity,
                    ReturnId = p.ReturnId,
                    ReturnDescription = p.Return.Description,
                    ReturnDescriptionTranslations = p.Return.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(p => new ProductReturned()
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ProductName = p.ProductName.Translate(),
                ProductReturnedTime = p.ProductReturnedTime,
                Quantity = p.Quantity,
                ReturnId = p.ReturnId,
                ReturnDescription = p.ReturnDescription.Translate()
            }).ToList();

            return result;
        }

        public async Task<ProductReturned> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(p => p.Return).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.Id == id)
                .Select(p => new 
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    ProductReturnedTime = p.ProductReturnedTime,
                    Quantity = p.Quantity,
                    ReturnId = p.ReturnId,
                    ReturnDescription = p.Return.Description,
                    ReturnDescriptionTranslations = p.Return.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new ProductReturned()
            {
                Id = res.Id,
                ProductId = res.ProductId,
                ProductName = res.ProductName.Translate(),
                ProductReturnedTime = res.ProductReturnedTime,
                Quantity = res.Quantity,
                ReturnId = res.ReturnId,
                ReturnDescription = res.ReturnDescription.Translate()
            };

            return result;
        }
        
        public async Task<List<ProductReturned>> AllAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(p => p.Return).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.ReturnId == id)
                .Select(p => new 
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    ProductReturnedTime = p.ProductReturnedTime,
                    Quantity = p.Quantity,
                    ReturnId = p.ReturnId,
                    ReturnDescription = p.Return.Description,
                    ReturnDescriptionTranslations = p.Return.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(e => new ProductReturned()
            {
                Id = e.Id,
                ProductId = e.ProductId,
                ProductName = e.ProductName.Translate(),
                ProductReturnedTime = e.ProductReturnedTime,
                Quantity = e.Quantity,
                ReturnId = e.ReturnId,
                ReturnDescription = e.ReturnDescription.Translate()
            }).ToList();

            return result;
        }
        
        public async Task<List<DTO.DomainLikeDTO.ProductReturned>> AllAsyncByReturnId(int returnId)
        {
            return await RepositoryDbSet
                .Where(p => p.ReturnId == returnId).Select(e => ProductReturnedMapper.MapFromDomain(e)).ToListAsync();
        }
    }
}