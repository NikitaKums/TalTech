using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Defect = DAL.App.DTO.DomainLikeDTO.Defect;

namespace DAL.App.EF.Repositories
{
    public class DefectRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Defect, Domain.Defect, AppDbContext>,
        IDefectRepository
    {

        public DefectRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new DefectMapper())
        {
        }

        public override Defect Update(Defect entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(m => m.Description)
                .ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);

            if (entityInDb == null) return entity;
            
            entityInDb.ShopId = entity.ShopId;
            RepositoryDbSet.Update(entityInDb);
            
            entityInDb.Description.SetTranslation(entity.Description);

            return entity;
        }

        public override async Task<List<Defect>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsWithDefect).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName).ThenInclude(t => t.Translations)
                .Select(e => DefectMapper.MapFromDomain(e)).ToListAsync();
            
        }

        public override async Task<Defect> FindAsync(params object[] id)
        {
            var defect = await RepositoryDbSet.FindAsync(id);

            return DefectMapper.MapFromDomain(await RepositoryDbSet
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsWithDefect).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName).ThenInclude(t => t.Translations)
                .Where(a => a.Id == defect.Id)
                .FirstOrDefaultAsync());
            
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(d => d.ProductsWithDefect).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
            
        }

        public async Task<List<Defect>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsWithDefect).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName)
                .ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId).AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => DefectMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public async Task<List<Defect>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsWithDefect).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName)
                .ThenInclude(t => t.Translations)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => DefectMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Defect> Search(IQueryable<Domain.Defect> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s => 
                    s.Description.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ).AsQueryable();
            }

            return query;
        }
        
        private IQueryable<Domain.Defect> Order(IQueryable<Domain.Defect> res, string order)
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
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(d => d.ProductsWithDefect)
                .Where(d => d.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }


        public async Task<List<DefectWithProductCount>> GetAllWithProductsWithDefectByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(d => d.ProductsWithDefect)
                .Where(d => d.ShopId == shopId).AsQueryable();
            
            query = Search(query, search);

            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query
                .Select(d => new 
                {
                    Id = d.Id,
                    Description = d.Description,
                    ShopId = d.ShopId,
                    ShopName = d.Shop.ShopName,
                    ProductsWithDefectCount = d.ProductsWithDefect.Count,
                    DescriptionTranslation = d.Description.Translations,
                    ShopNameTranslations = d.Shop.ShopName.Translations
                }).ToListAsync();

            var result = res.Select(d => new DefectWithProductCount()
            {
                Id = d.Id,
                Description = d.Description.Translate(),
                ShopId = d.ShopId,
                ShopName = d.ShopName.Translate(),
                ProductsWithDefectCount = d.ProductsWithDefectCount
            }).ToList();

            return result;
        }

        public async Task<DefectWithProductCount> FindProductsWithDefectByShopAsync(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(d => d.ProductsWithDefect)
                .Include(d => d.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Where(d => d.ShopId == shopId && d.Id == id)
                .Select(d => new
                {
                    Id = d.Id,
                    Description = d.Description,
                    ShopId = d.ShopId,
                    ShopName = d.Shop.ShopName,
                    ProductsWithDefectCount = d.ProductsWithDefect.Count,
                    DescriptionTranslation = d.Description.Translations,
                    ShopNameTranslations = d.Shop.ShopName.Translations
                    
                }).FirstOrDefaultAsync();

            var result = new DefectWithProductCount()
            {
                Id = res.Id,
                Description = res.Description.Translate(),
                ShopId = res.ShopId,
                ShopName = res.ShopName.Translate(),
                ProductsWithDefectCount = res.ProductsWithDefectCount
            };

            return result;
        }
    }
}