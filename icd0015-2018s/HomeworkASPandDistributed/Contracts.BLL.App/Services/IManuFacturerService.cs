using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using Resources.Domain;
using BLLAppDTO = BLL.App.DTO;
using ManuFacturer = BLL.App.DTO.DomainLikeDTO.ManuFacturer;

namespace Contracts.BLL.App.Services
{
    public interface IManuFacturerService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.ManuFacturer>, IManuFacturerRepository<BLLAppDTO.DomainLikeDTO.ManuFacturer>
    {
        Task<BLLAppDTO.ManuFacturerWithProductCount> FindByIdAndShop(int id, int? shopId);
        Task<List<BLLAppDTO.ManuFacturerWithProductCount>> GetAllWithProductCountAsync(int? shopId, string search, int? pageIndex, int? pageSize);
    }
}