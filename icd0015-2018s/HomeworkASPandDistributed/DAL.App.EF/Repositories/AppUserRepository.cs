using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using AppUser = DAL.App.DTO.DomainLikeDTO.Identity.AppUser;

namespace DAL.App.EF.Repositories
{
    public class AppUserRepository :
        BaseRepository<DAL.App.DTO.DomainLikeDTO.Identity.AppUser, Domain.Identity.AppUser, AppDbContext>,
        IAppUserRepository
    {
        public AppUserRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new AppUserMapper())
        {
        }

        public async Task<DAL.App.DTO.Identity.AppUser> GetUserInfo(int userId)
        {
            var res = await RepositoryDbSet
                .Include(s => s.Shop).ThenInclude(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Where(u => u.Id == userId)
                .Select(c => new
                {
                    Id = c.Id,
                    Address = c.Aadress,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ShopId = c.ShopId,
                    ShopName = c.Shop.ShopName,
                    ShopNameTranslations = c.Shop.ShopName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new DAL.App.DTO.Identity.AppUser()
            {
                Id = res.Id,
                Address = res.Address,
                FirstName = res.FirstName,
                LastName = res.LastName,
                ShopId = res.ShopId,
                ShopName = res.ShopName.Translate()
            };

            return result;
        }

        public async Task<List<DAL.App.DTO.DomainLikeDTO.Identity.AppUser>> GetUserById(int userId)
        {
            return await RepositoryDbSet
                .Include(s => s.Shop).ThenInclude(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Where(a => a.Id == userId)
                .Select(e => AppUserMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<List<DAL.App.DTO.DomainLikeDTO.Identity.AppUser>> AllAsyncByShop(int? shopId, string order, string searchFor)
        {
            var query = RepositoryDbSet
                .Include(s => s.Shop).ThenInclude(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Where(a => a.ShopId == shopId || a.ShopId == null)
                .AsQueryable();
            
            query = Search(query, searchFor);

            var res = await query.Select(e => AppUserMapper.MapFromDomain(e)).ToListAsync();
            return Order(res, order);
        }

        public async Task<List<AppUser>> AllAsync(string order, string searchFor)
        {
            var query = RepositoryDbSet
                .Include(s => s.Shop).ThenInclude(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .AsQueryable();

            query = Search(query, searchFor);

            var res = await query.Select(e => AppUserMapper.MapFromDomain(e)).ToListAsync();
            return Order(res, order);
        }

        private IQueryable<Domain.Identity.AppUser> Search(IQueryable<Domain.Identity.AppUser> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                searchFor = searchFor.ToLower();
                query = query.Where(s =>
                    s.FirstName.ToLower().Contains(searchFor) ||
                    s.LastName.ToLower().Contains(searchFor) ||
                    s.Email.ToLower().Contains(searchFor) ||
                    s.Aadress != null && s.Aadress.ToLower().Contains(searchFor)).AsQueryable();
            }

            return query;
        }

        private List<AppUser> Order(List<AppUser> res, string order)
        {
            switch (order)
            {
                case "lastname_desc":
                    return res.OrderBy(s => s.LastName).Reverse().ToList();
                case "lastname":
                    return res.OrderBy(s => s.LastName).ToList();
                case "email_desc":
                    return res.OrderBy(s => s.Email).Reverse().ToList();
                case "email":
                    return res.OrderBy(s => s.Email).ToList();
                case "address_desc":
                    return res.OrderBy(s => s.Aadress).Reverse().ToList();
                case "address":
                    return res.OrderBy(s => s.Aadress).ToList();
                case "shop_desc":
                    return res.OrderBy(s => s.Shop?.ShopName).Reverse().ToList();
                case "shop":
                    return res.OrderBy(s => s.Shop?.ShopName).ToList();
                case "name_desc":
                    return res.OrderBy(s => s.FirstName).Reverse().ToList();
                default:
                    return res.OrderBy(s => s.FirstName).ToList();
            }
        }

        public async Task<int> CountUsersInShop(int? shopId)
        {
            return await RepositoryDbSet
                .Where(p => p.ShopId == shopId).CountAsync();
        }

        public async Task<int> CountAllUsers()
        {
            return await RepositoryDbSet.CountAsync();
        }

        public override async Task<List<AppUser>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(s => s.Shop).ThenInclude(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .Select(e => AppUserMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<AppUser> FindAsync(params object[] id)
        {
            var appUser = await base.FindAsync(id);
            return AppUserMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == appUser.Id)
                .Include(s => s.Shop).ThenInclude(a => a.ShopName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopAddress).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact).ThenInclude(t => t.Translations)
                .Include(s => s.Shop).ThenInclude(a => a.ShopContact2).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }
    }
}