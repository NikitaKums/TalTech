using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using ManuFacturer = BLL.App.DTO.DomainLikeDTO.ManuFacturer;

namespace BLL.App.Services
{
    public class ManuFacturerService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.ManuFacturer, DAL.App.DTO.DomainLikeDTO.ManuFacturer, IAppUnitOfWork>, IManuFacturerService
    {
        public ManuFacturerService(IAppUnitOfWork uow) : base(uow, new ManuFacturerMapper())
        {
            ServiceRepository = Uow.ManuFacturers;
        }


        /*public override async Task<IEnumerable<ManuFacturer>> AllAsync()
        {
            return await UOW.ManuFacturers.AllAsync();
        }

        public override async Task<ManuFacturer> FindAsync(params object[] id)
        {
            return await UOW.ManuFacturers.FindAsync(id);
        }

        public async Task<IEnumerable<ManuFacturerDTO>> GetAllWithProductCountAsync(int? shopId)
        {
            return await UOW.ManuFacturers.GetAllWithProductCountAsync(shopId);
        }

        public async Task<ManuFacturerDTO> FindByIdAndShop(int id, int? shopId)
        {
            return await UOW.ManuFacturers.FindByIdAndShop(id, shopId);
        }*/

        public override async Task<List<ManuFacturer>> AllAsync()
        {
            return (await Uow.ManuFacturers.AllAsync()).Select(e => ManuFacturerMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<ManuFacturer> FindAsync(params object[] id)
        {
            return ManuFacturerMapper.MapFromDAL(await Uow.ManuFacturers.FindAsync(id));
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.ManuFacturers.CountDataAmount(shopId, search);
        }
        
        public async Task<ManuFacturerWithProductCount> FindByIdAndShop(int id, int? shopId)
        {
            return ManuFacturerMapper.MapFromDAL(await Uow.ManuFacturers.FindByIdAndShop(id, shopId));
        }

        public async Task<List<ManuFacturerWithProductCount>> GetAllWithProductCountAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.ManuFacturers.GetAllWithProductCountAsync(shopId, search, pageIndex, pageSize)).Select(e => ManuFacturerMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<ManuFacturer>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.ManuFacturers.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => ManuFacturerMapper.MapFromDAL(e)).ToList();
        }
    }
}