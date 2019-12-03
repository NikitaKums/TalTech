using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICommentService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Comment>, ICommentRepository<BLLAppDTO.DomainLikeDTO.Comment>
    {
        Task<BLLAppDTO.Comment> GetCommentById(int id);
        Task<BLLAppDTO.Comment> GetCommentByIdAndShop(int id, int? shopId);
        Task<List<BLLAppDTO.Comment>> GetAllWithProductByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
    }
}