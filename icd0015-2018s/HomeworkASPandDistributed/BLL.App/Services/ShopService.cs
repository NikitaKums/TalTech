using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Shop = BLL.App.DTO.DomainLikeDTO.Shop;

namespace BLL.App.Services
{
    public class ShopService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Shop, DAL.App.DTO.DomainLikeDTO.Shop, IAppUnitOfWork>, IShopService
    {
        public ShopService(IAppUnitOfWork uow) : base(uow, new ShopMapper())
        {
            ServiceRepository = Uow.Shops;
        }


        /*public override async Task<IEnumerable<Shop>> AllAsync()
        {
            return await UOW.Shops.AllAsync();
        }

        public override async Task<Shop> FindAsync(params object[] id)
        {
            return await UOW.Shops.FindAsync(id);
        }

        public async Task<ShopDTO> GetShopByShopId(int? shopId)
        {
            return await UOW.Shops.GetShopByShopId(shopId);
        }

        public async Task<IEnumerable<Shop>> GetShopByUserShopId(int? shopId)
        {
            return await UOW.Shops.GetShopByUserShopId(shopId);
        }

        public async Task<IEnumerable<ShopDTO>> GetAllWithCountsAsync()
        {
            return await UOW.Shops.GetAllWithCountsAsync();
        }*/
        
        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Shops.CountDataAmount(search);
        }
        
        public override async Task<List<Shop>> AllAsync()
        {
            return (await Uow.Shops.AllAsync()).Select(e => ShopMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Shop> FindAsync(params object[] id)
        {
            return ShopMapper.MapFromDAL(await Uow.Shops.FindAsync(id));
        }

        public async Task<List<Shop>> GetShopByUserShopIdForDropDown(int? shopId)
        {
            return (await Uow.Shops.GetShopByUserShopIdForDropDown(shopId)).Select(e => ShopMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Shop>> GetShopByUserShopId(int? shopId)
        {
            return (await Uow.Shops.GetShopByUserShopId(shopId)).Select(e => ShopMapper.MapFromDAL(e)).ToList();
        }

        public async Task<ShopWithCounts> GetShopByShopId(int? shopId)
        {
            return ShopMapper.MapFromDAL(await Uow.Shops.GetShopByShopId(shopId));
        }

        public async Task<List<ShopWithCounts>> GetAllWithCountsAsync()
        {
            return (await Uow.Shops.GetAllWithCountsAsync()).Select(e => ShopMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Shop>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Shops.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => ShopMapper.MapFromDAL(e)).ToList();
        }
    }
}