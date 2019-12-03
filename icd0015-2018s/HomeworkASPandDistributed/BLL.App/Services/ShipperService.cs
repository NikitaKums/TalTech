using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Shipper = BLL.App.DTO.DomainLikeDTO.Shipper;

namespace BLL.App.Services
{
    public class ShipperService :
        BaseEntityService<BLL.App.DTO.DomainLikeDTO.Shipper, DAL.App.DTO.DomainLikeDTO.Shipper, IAppUnitOfWork>,
        IShipperService
    {
        public ShipperService(IAppUnitOfWork uow) : base(uow, new ShipperMapper())
        {
            ServiceRepository = Uow.Shippers;
        }


        /*public async Task<IEnumerable<ShipperDTO>> GetAllWithOrdersCountByShopAsync(int? shopId)
        {
            return await UOW.Shippers.GetAllWithOrdersCountByShopAsync(shopId);
        }

        public async Task<ShipperDTO> FindByIdAsyncDTO(int id)
        {
            return await UOW.Shippers.FindByIdAsyncDTO(id);
        }*/

        public async Task<List<ShipperWithOrderCount>> GetAllWithOrdersCountByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Shippers.GetAllWithOrdersCountByShopAsync(shopId, search, pageIndex, pageSize))
                .Select(e => ShipperMapper.MapFromDAL(e)).ToList();
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Shippers.CountDataAmount(shopId, search);
        }

        public async Task<ShipperWithOrderCount> FindByIdAsyncDTO(int id)
        {
            return ShipperMapper.MapFromDAL(await Uow.Shippers.FindByIdAsyncDTO(id));
        }

        public async Task<List<Shipper>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Shippers.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => ShipperMapper.MapFromDAL(e)).ToList();
        }
    }
}