using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.DomainLikeDTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Parsing;
using Category = DAL.App.DTO.DomainLikeDTO.Category;
using DomainEntity = Domain.DomainEntity;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Category, Domain.Category, AppDbContext>,
        ICategoryRepository
    {
        public CategoryRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new CategoryMapper())
        {
        }

        public override Category Update(Category entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(m => m.CategoryName)
                .ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);

            if (entityInDb == null) return entity;

            entityInDb.ShopId = entity.ShopId;
            RepositoryDbSet.Update(entityInDb);

            entityInDb.CategoryName.SetTranslation(entity.CategoryName);

            return entity;
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query =  RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public virtual async Task<List<DAL.App.DTO.DomainLikeDTO.Category>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInCategory).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName)
                .ThenInclude(t => t.Translations)
                .Where(a => a.ShopId == shopId).AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => CategoryMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Category> Search(IQueryable<Domain.Category> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s =>s.CategoryName.Translations.Any(e => e.Value.Contains(searchFor) && 
                                                                             e.Culture == Thread.CurrentThread.CurrentUICulture.Name.Substring(0, 2).ToLower()));
            }

            return query;
        }

        private IQueryable<Domain.Category>Order(IQueryable<Domain.Category> list, string order)
        {
            switch (order)
            {
                case "name_desc":
                    return list.OrderByDescending(s => s.CategoryName.Value);
                default:
                    return list.OrderBy(s => s.CategoryName.Value);
            }
        }
        
        public virtual async Task<List<DAL.App.DTO.DomainLikeDTO.Category>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInCategory).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName)
                .ThenInclude(t => t.Translations).AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => CategoryMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query =  RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public virtual async Task<List<DAL.App.DTO.CategoryWithProductCount>> GetAllWithProductCountForShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query =  RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId).AsQueryable();

            query = Search(query, search);

            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query.Select(c => new
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    CategoryProductCount = c.ProductsInCategory.Count,
                    CategoryNameTranslations = c.CategoryName.Translations
                }).ToListAsync();
            
            var resultList = res.Select(c => new CategoryWithProductCount()
            {
                Id = c.Id,
                CategoryName = c.CategoryName.Translate(),
                CategoryProductCount = c.CategoryProductCount
            }).ToList();
            return resultList;
        }
        
        public virtual async Task<DAL.App.DTO.CategoryWithProductCount> GetByIndexAndShop(int categoryId, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId && s.Id == categoryId)
                .Select(c => new
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    CategoryProductCount = c.ProductsInCategory.Count,
                    CategoryNameTranslations = c.CategoryName.Translations
                }).FirstOrDefaultAsync();

            var result = new CategoryWithProductCount()
            {
                Id = res.Id,
                CategoryName = res.CategoryName.Translate(),
                CategoryProductCount = res.CategoryProductCount
            };

            return result;
        }

        public override async Task<List<DAL.App.DTO.DomainLikeDTO.Category>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInCategory).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName)
                .ThenInclude(t => t.Translations)
                .Select(e => CategoryMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<DAL.App.DTO.DomainLikeDTO.Category> FindAsync(params object[] id)
        {
            var category = await RepositoryDbSet.FindAsync(id);

            return CategoryMapper.MapFromDomain(await RepositoryDbSet
                .Include(a => a.CategoryName).ThenInclude(t => t.Translations)
                .Where(a => a.Id == category.Id)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.ProductsInCategory).ThenInclude(aa => aa.Product).ThenInclude(aaa => aaa.ProductName)
                .ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }
    }
}