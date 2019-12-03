using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Inventory = BLL.App.DTO.DomainLikeDTO.Inventory;

namespace BLL.App.Services
{
    public class InventoryService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Inventory, DAL.App.DTO.DomainLikeDTO.Inventory, IAppUnitOfWork>, IInventoryService
    {
        public InventoryService(IAppUnitOfWork uow) : base(uow, new InventoryMapper())
        {
            ServiceRepository = Uow.Inventories;
        }


       /* public override async Task<IEnumerable<Inventory>> AllAsync()
        {
            return await UOW.Inventories.AllAsync();
        }

        public override async Task<Inventory> FindAsync(params object[] id)
        {
            return await UOW.Inventories.FindAsync(id);
        }

        public async Task<IEnumerable<Inventory>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Inventories.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<InventoryDTO>> GetByShopAsync(int? shopId)
        {
            return await UOW.Inventories.GetByShopAsync(shopId);
        }

        public async Task<InventoryDTO> FindByShopAsyncAndIdAsync(int id, int? shopId)
        {
            return await UOW.Inventories.FindByShopAsyncAndIdAsync(id, shopId);
        }*/
       
       public async Task<int> CountDataAmount(string search)
       {
           return await Uow.Inventories.CountDataAmount(search);
       }

       public override async Task<List<Inventory>> AllAsync()
       {
           return (await Uow.Inventories.AllAsync()).Select(e => InventoryMapper.MapFromDAL(e)).ToList();
       }

       public override async Task<Inventory> FindAsync(params object[] id)
       {
           return InventoryMapper.MapFromDAL(await Uow.Inventories.FindAsync(id));
       }

       public async Task<bool> IsInventoryEmpty(int id)
       {
           return await Uow.Inventories.IsInventoryEmpty(id);
       }
       
       public async Task<int> CountDataAmount(int? shopId, string search)
       {
           return await Uow.Inventories.CountDataAmount(shopId, search);
       }

       public async Task<List<InventoryWithProductCount>> GetByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
       {
           return (await Uow.Inventories.GetByShopAsync(shopId, search, pageIndex, pageSize)).Select(e => InventoryMapper.MapFromDAL(e)).ToList();
       }

       public async Task<InventoryWithProductCount> FindByShopAsyncAndIdAsync(int id, int? shopId)
       {
           return InventoryMapper.MapFromDAL(await Uow.Inventories.FindByShopAsyncAndIdAsync(id, shopId));
       }

       public async Task<List<Inventory>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
       {
           return (await Uow.Inventories.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => InventoryMapper.MapFromDAL(e)).ToList();
       }

       public async Task<List<Inventory>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
       {
           return (await Uow.Inventories.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => InventoryMapper.MapFromDAL(e)).ToList();
       }

    }
}