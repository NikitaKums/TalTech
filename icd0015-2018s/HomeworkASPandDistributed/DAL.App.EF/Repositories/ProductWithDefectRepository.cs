using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO.IdAndNameDTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProductWithDefect = DAL.App.DTO.ProductWithDefect;

namespace DAL.App.EF.Repositories
{
    public class ProductWithDefectRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.ProductWithDefect, Domain.ProductWithDefect, AppDbContext>,
        IProductWithDefectRepository
    {

        public ProductWithDefectRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ProductWithDefectMapper())
        {
        }

        public override async Task<List<DTO.DomainLikeDTO.ProductWithDefect>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(d => d.Defect).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Select(e => ProductWithDefectMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<DTO.DomainLikeDTO.ProductWithDefect> FindAsync(params object[] id)
        {
            var productWithDefect = await RepositoryDbSet.FindAsync(id);

            return ProductWithDefectMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == productWithDefect.Id)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(d => d.Defect).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<List<DTO.DomainLikeDTO.ProductWithDefect>> AllAsyncByShop(int? shopId)
        {
            return await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(d => d.Defect).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(e => ProductWithDefectMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<List<ProductWithDefect>> AllAsyncByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(d => d.Defect).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(p => new
                {
                    Id = p.Id,
                    DefectDescription = p.Defect.Description,
                    DefectId = p.DefectId,
                    DefectRecordingTime = p.DefectRecordingTime,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    DefectDescriptionTranslations = p.Defect.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(p => new ProductWithDefect()
            {
                Id = p.Id,
                DefectDescription = p.DefectDescription.Translate(),
                DefectId = p.DefectId,
                DefectRecordingTime = p.DefectRecordingTime,
                ProductId = p.ProductId,
                ProductName = p.ProductName.Translate(),
                Quantity = p.Quantity
            }).ToList();
            
            return result;
        }

        public async Task<ProductWithDefect> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(d => d.Defect).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    DefectDescription = p.Defect.Description,
                    DefectId = p.DefectId,
                    DefectRecordingTime = p.DefectRecordingTime,
                    ProductId = p.ProductId,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    DefectDescriptionTranslations = p.Defect.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new ProductWithDefect()
            {
                Id = res.Id,
                DefectDescription = res.DefectDescription.Translate(),
                DefectId = res.DefectId,
                DefectRecordingTime = res.DefectRecordingTime,
                ProductId = res.ProductId,
                ProductName = res.ProductName.Translate(),
                Quantity = res.Quantity
            };

            return result;
        }
        
        public async Task<int> CountDefectItems(int defectId)
        {
            return await RepositoryDbSet
                .Where(d => d.DefectId == defectId)
                .CountAsync();
        }

        public async Task<List<DAL.App.DTO.DomainLikeDTO.ProductWithDefect>> AllAsyncByDefectId(int defectId)
        {
            return await RepositoryDbSet
                .Where(a => a.DefectId == defectId).Select(e => ProductWithDefectMapper.MapFromDomain(e)).ToListAsync();
        }
        
        public async Task<List<DAL.App.DTO.IdAndNameDTO.ProductIdName>> AllAsyncByDefectIdAndShopId(int defectId, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(s => s.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(s => s.Defect)
                .Where(a => a.DefectId == defectId&& a.Product.ShopId == shopId)
                .Select(e => new
                {
                    ProductId = e.ProductId,
                    ProductName = e.Product.ProductName,
                    ProductWithDefectId = e.Id,
                    ProductNameTranslations = e.Product.ProductName.Translations
                }).ToListAsync();

            return res.Select(e => new ProductIdName
            {
                ProductName = e.ProductName.Translate(),
                Id = e.ProductId,
                ProductWithDefectId = e.ProductWithDefectId
            }).ToList();
        }
        
        public async Task<List<DAL.App.DTO.ProductWithDefect>> AllAsyncByShopAndDefectIdDTO(int defectId, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(s => s.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(s => s.Defect).ThenInclude(aa => aa.Description).ThenInclude(t => t.Translations)
                .Where(a => a.DefectId == defectId&& a.Product.ShopId == shopId)
                .Select(e => new
                {
                    ProductId = e.ProductId,
                    ProductName = e.Product.ProductName,
                    ProductWithDefectId = e.Id,
                    Quantity = e.Quantity,
                    DefectRecordingTime = e.DefectRecordingTime,
                    DefectId = e.DefectId,
                    DefectDescription = e.Defect.Description,
                    DefectDescriptionTranslations = e.Defect.Description.Translations,
                    ProductNameTranslations = e.Product.ProductName.Translations
                }).ToListAsync();

            return res.Select(e => new ProductWithDefect()
            {
                Id = e.ProductWithDefectId,
                DefectDescription = e.DefectDescription.Translate(),
                DefectId = e.DefectId,
                Quantity = e.Quantity,
                DefectRecordingTime = e.DefectRecordingTime,
                ProductId = e.ProductId,
                ProductName = e.ProductName.Translate()
            }).ToList();
        }
    }
}