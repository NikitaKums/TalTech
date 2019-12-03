using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO.Identity;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IAppUserService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Identity.AppUser>, IAppUserRepository<BLLAppDTO.DomainLikeDTO.Identity.AppUser>
    {
        Task<BLLAppDTO.Identity.AppUser> GetUserInfo(int userId);
    }
}