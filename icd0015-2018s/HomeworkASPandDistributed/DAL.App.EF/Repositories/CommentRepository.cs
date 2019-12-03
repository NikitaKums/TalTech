using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO.DomainLikeDTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DAL.App.EF.Repositories
{
    public class CommentRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Comment, Domain.Comment, AppDbContext>,
        ICommentRepository
    {

        public CommentRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new CommentMapper())
        {
        }
        
        public override Comment Update(Comment entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);
            
            if (entityInDb == null) return entity;
            
            entityInDb.ShopId = entity.ShopId;
            entityInDb.ProductId = entity.ProductId;
            RepositoryDbSet.Update(entityInDb);
            
            entityInDb.CommentTitle.SetTranslation(entity.CommentTitle);
            entityInDb.CommentBody.SetTranslation(entity.CommentBody);

            return entity;

        }

        public override async Task<List<Comment>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Select(e => CommentMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Comment> FindAsync(params object[] id)
        {
            var comment = await RepositoryDbSet.FindAsync(id);

            return CommentMapper.MapFromDomain(await RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Where(a => a.Id == comment.Id)
                .FirstOrDefaultAsync());

        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public virtual async Task<List<DAL.App.DTO.DomainLikeDTO.Comment>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Where(a => a.ShopId == shopId).AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => CommentMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public virtual async Task<List<Comment>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
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
            var res = temp.Select(e => CommentMapper.MapFromDomain(e)).ToList();
            return res;
        }
        
        private IQueryable<Domain.Comment> Search(IQueryable<Domain.Comment> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s => 
                    s.CommentBody.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.CommentBody.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.Product.ProductName.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ).AsQueryable();
            }

            return query;
        }
        
        private IQueryable<Domain.Comment> Order(IQueryable<Domain.Comment> res, string order)
        {
            switch (order)
            {
                case "title_desc":
                    return res.OrderByDescending(s => s.CommentTitle.Value);
                case "body_desc":
                    return res.OrderByDescending(s => s.CommentBody.Value);
                case "body":
                    return res.OrderBy(s => s.CommentBody.Value);
                case "product_desc":
                    return res.OrderByDescending(s => s.Product.ProductName.Value);
                case "product":
                    return res.OrderBy(s => s.Product.ProductName.Value);
                default:
                    return res.OrderBy(s => s.CommentTitle.Value);
            }
        }

        public virtual async Task<DTO.Comment> GetCommentById(int id)
        {
            var res = await RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(s => s.Id == id)
                .Select(c => new
                {
                    Id = c.Id,
                    CommentTitle = c.CommentTitle,
                    CommentBody = c.CommentBody,
                    ProductName = c.Product.ProductName,
                    ProductId = c.ProductId,
                    ShopId = c.ShopId,
                    CommentBodyTranslations = c.CommentBody.Translations,
                    CommentTitleTranslations = c.CommentTitle.Translations,
                    ProductNameTranslations = c.Product.ProductName.Translations
                }).FirstOrDefaultAsync();

            var result = new DTO.Comment()
            {
                Id = res.Id,
                CommentTitle = res.CommentTitle.Translate(),
                CommentBody = res.CommentBody.Translate(),
                ProductName = res.ProductName.Translate(),
                ProductId = res.ProductId,
                ShopId = res.ShopId
            };

            return result;
        }

        public virtual async Task<DTO.Comment> GetCommentByIdAndShop(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(s => s.Id == id && s.ShopId == shopId)
                .Select(c => new
                {
                    Id = c.Id,
                    CommentTitle = c.CommentTitle,
                    CommentBody = c.CommentBody,
                    ProductName = c.Product.ProductName,
                    ProductId = c.ProductId,
                    ShopId = c.ShopId,
                    CommentBodyTranslations = c.CommentBody.Translations,
                    CommentTitleTranslations = c.CommentTitle.Translations,
                    ProductNameTranslations = c.Product.ProductName.Translations
                }).FirstOrDefaultAsync();

            var result = new DTO.Comment()
            {
                Id = res.Id,
                CommentTitle = res.CommentTitle.Translate(),
                CommentBody = res.CommentBody.Translate(),
                ProductName = res.ProductName.Translate(),
                ProductId = res.ProductId,
                ShopId = res.ShopId
            };

            return result;
        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(c => c.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }

        public virtual async Task<List<DTO.Comment>> GetAllWithProductByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(a => a.CommentTitle).ThenInclude(t => t.Translations)
                .Include(a => a.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(c => c.ShopId == shopId);
            
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
                    CommentTitle = c.CommentTitle,
                    CommentBody = c.CommentBody,
                    ProductName = c.Product.ProductName,
                    ProductId = c.ProductId,
                    ShopId = c.ShopId,
                    CommentBodyTranslations = c.CommentBody.Translations,
                    CommentTitleTranslations = c.CommentTitle.Translations,
                    ProductNameTranslations = c.Product.ProductName.Translations
                }).ToListAsync();
            
            var result = res.Select(c => new DTO.Comment()
            {
                Id = c.Id,
                CommentTitle = c.CommentTitle.Translate(),
                CommentBody = c.CommentBody.Translate(),
                ProductName = c.ProductName.Translate(),
                ProductId = c.ProductId,
                ShopId = c.ShopId
            }).ToList();

            return result;
        }
    }
}