using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO.Identity;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.nikita.BLL.Base.Services;

namespace BLL.App.Services
{
    public class AppUserService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Identity.AppUser, DAL.App.DTO.DomainLikeDTO.Identity.AppUser, IAppUnitOfWork>, IAppUserService
    {
        public AppUserService(IAppUnitOfWork uow) : base(uow, new AppUserMapper())
        {
            ServiceRepository = Uow.AppUsers;
        }


        /*public override async Task<IEnumerable<AppUser>> AllAsync()
        {
            return await UOW.AppUsers.AllAsync();
        }

        public override async Task<AppUser> FindAsync(params object[] id)
        {
            return await UOW.AppUsers.FindAsync(id);
        }

        public async Task<AppUserDTO> GetUserInfo(int userId)
        {
            return await UOW.AppUsers.GetUserInfo(userId);
        }

        public async Task<IEnumerable<AppUser>> GetUserById(int userId)
        {
            return await UOW.AppUsers.GetUserById(userId);
        }

        public async Task<IEnumerable<AppUser>> AllAsyncByShop(int? shopId)
        {
            return await UOW.AppUsers.AllAsyncByShop(shopId);

        }

        public async Task<int> CountUsersInShop(int? shopId)
        {
            return await UOW.AppUsers.CountUsersInShop(shopId);
        }

        public async Task<int> CountAllUsers()
        {
            return await UOW.AppUsers.CountAllUsers();
        }*/

        public override async Task<List<AppUser>> AllAsync()
        {
            return (await Uow.AppUsers.AllAsync()).Select(e => AppUserMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<AppUser> FindAsync(params object[] id)
        {
            return AppUserMapper.MapFromDAL(await Uow.AppUsers.FindAsync(id));
        }

        public async Task<List<AppUser>> GetUserById(int userId)
        {
            return (await Uow.AppUsers.GetUserById(userId)).Select(e => AppUserMapper.MapFromDAL(e)).ToList();
        }

        public async Task<int> CountUsersInShop(int? shopId)
        {
            return await Uow.AppUsers.CountUsersInShop(shopId);
        }

        public async Task<int> CountAllUsers()
        {
            return await Uow.AppUsers.CountAllUsers();
        }

        public async Task<DTO.Identity.AppUser> GetUserInfo(int userId)
        {
            return AppUserMapper.MapFromDAL(await Uow.AppUsers.GetUserInfo(userId));
        }

        public async Task<List<AppUser>> AllAsyncByShop(int? shopId, string order, string searchFor)
        {
            var res = (await Uow.AppUsers.AllAsyncByShop(shopId, order, searchFor)).Select(e => AppUserMapper.MapFromDAL(e)).ToList();
            return res;
        }

        public async Task<List<AppUser>> AllAsync(string order, string searchFor)
        {
            var res = (await Uow.AppUsers.AllAsync(order, searchFor)).Select(e => AppUserMapper.MapFromDAL(e)).ToList();
            return res;
        }
    }
}