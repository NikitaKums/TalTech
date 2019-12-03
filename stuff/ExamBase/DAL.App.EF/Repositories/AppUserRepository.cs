using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Domain.Identity;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
     public class AppUserRepository :
        BaseRepository<AppUser, AppUser, AppDbContext>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }
    }
}

