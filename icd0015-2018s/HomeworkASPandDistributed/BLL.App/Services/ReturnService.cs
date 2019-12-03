using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Return = BLL.App.DTO.DomainLikeDTO.Return;

namespace BLL.App.Services
{
    public class ReturnService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Return, DAL.App.DTO.DomainLikeDTO.Return, IAppUnitOfWork>, IReturnService
    {
        public ReturnService(IAppUnitOfWork uow) : base(uow, new ReturnMapper())
        {
            ServiceRepository = Uow.Returns;
        }


        /*public override async Task<IEnumerable<Return>> AllAsync()
        {
            return await UOW.Returns.AllAsync();
        }

        public override async Task<Return> FindAsync(params object[] id)
        {
            return await UOW.Returns.FindAsync(id);
        }

        public async Task<IEnumerable<Return>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Returns.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ReturnDTO>> GetAllWithProductsReturnedAsync()
        {
            return await UOW.Returns.GetAllWithProductsReturnedAsync();
        }

        public async Task<IEnumerable<ReturnDTO>> GetAllWithProductsReturnedByShopAsync(int? shopId)
        {
            return await UOW.Returns.GetAllWithProductsReturnedByShopAsync(shopId);
        }

        public async Task<ReturnDTO> FindWithProductsReturnedByIdAndShopAsync(int id, int? shopId)
        {
            return await UOW.Returns.FindWithProductsReturnedByIdAndShopAsync(id, shopId);
        }

        public async Task<IEnumerable<ReturnIdNameDTO>> GetAllIdAndDescAsyncByShopDTO(int? shopId)
        {
            return await UOW.Returns.GetAllIdAndDescAsyncByShopDTO(shopId);
        }*/
        
        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Returns.CountDataAmount(search);
        }

        public async Task<List<Return>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var res = (await Uow.Returns.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => ReturnMapper.MapFromDAL(e)).ToList();
            return SearchSort(res, order, searchFor);
        }

        public async Task<List<Return>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var res = (await Uow.Returns.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => ReturnMapper.MapFromDAL(e)).ToList();
            return SearchSort(res, order, searchFor);
        }

        private List<Return> SearchSort(List<Return> res, string order, string searchFor)
        {
            if (searchFor != null)
            {
                searchFor = searchFor.ToLower();
                res = res.Where(s => 
                    s.Description.ToLower().Contains(searchFor)
                ).ToList();
            }
            switch (order)
            {
                case "description_desc":
                    return res.OrderBy(s => s.Description).Reverse().ToList();
                default:
                    return res.OrderBy(s => s.Description).ToList();
            }
        }

        public async Task DeleteReturn(int id)
        {
            var productsInReturn = await Uow.ProductsReturned.AllAsyncByReturnId(id);
            foreach (var productInReturn in productsInReturn)
            {
                Uow.ProductsReturned.Remove(productInReturn.Id);
            }
            Uow.Returns.Remove(id);
            await Uow.SaveChangesAsync();
        }
        
        public override async Task<List<Return>> AllAsync()
        {
            return (await Uow.Returns.AllAsync()).Select(e => ReturnMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Return> FindAsync(params object[] id)
        {
            return ReturnMapper.MapFromDAL(await Uow.Returns.FindAsync(id));
        }

        public async Task<List<ReturnWithProductCount>> GetAllWithProductsReturnedByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Returns.GetAllWithProductsReturnedByShopAsync(shopId, search, pageIndex, pageSize)).Select(e => ReturnMapper.MapFromDAL(e)).ToList();
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Returns.CountDataAmount(shopId, search);
        }

        public async Task<ReturnWithProductCount> FindWithProductsReturnedByIdAndShopAsync(int id, int? shopId)
        {
            return ReturnMapper.MapFromDAL(await Uow.Returns.FindWithProductsReturnedByIdAndShopAsync(id, shopId));
        }

        public async Task<List<BLL.App.DTO.IdAndNameDTO.ReturnIdName>> GetAllIdAndDescAsyncByShopDTO(int? shopId)
        {
            return (await Uow.Returns.GetAllIdAndDescAsyncByShopDTO(shopId)).Select(e => ReturnMapper.MapFromDAL(e)).ToList();
        }
    }
}