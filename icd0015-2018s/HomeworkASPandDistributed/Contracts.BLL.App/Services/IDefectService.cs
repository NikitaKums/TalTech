using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IDefectService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Defect>, IDefectRepository<BLLAppDTO.DomainLikeDTO.Defect>
    {
        Task<List<BLLAppDTO.DefectWithProductCount>> GetAllWithProductsWithDefectByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.DefectWithProductCount> FindProductsWithDefectByShopAsync(int id, int? shopId);
        Task DeleteDefect(int defectId);
    }
}