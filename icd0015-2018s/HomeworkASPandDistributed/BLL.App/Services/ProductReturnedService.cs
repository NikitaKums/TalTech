using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using ProductReturned = BLL.App.DTO.DomainLikeDTO.ProductReturned;

namespace BLL.App.Services
{
    public class ProductReturnedService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.ProductReturned, DAL.App.DTO.DomainLikeDTO.ProductReturned, IAppUnitOfWork>, IProductReturnedService
    {
        public ProductReturnedService(IAppUnitOfWork uow) : base(uow, new ProductReturnedMapper())
        {
            ServiceRepository = Uow.ProductsReturned;
        }


        /*public override async Task<IEnumerable<ProductReturned>> AllAsync()
        {
            return await UOW.ProductsReturned.AllAsync();
        }

        public override async Task<ProductReturned> FindAsync(params object[] id)
        {
            return await UOW.ProductsReturned.FindAsync(id);
        }

        public async Task<IEnumerable<ProductReturned>> AllAsyncByShop(int? shopId)
        {
            return await UOW.ProductsReturned.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ProductReturnedDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.ProductsReturned.AllAsyncByShopDTO(shopId);
        }

        public async Task<ProductReturnedDTO> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return await UOW.ProductsReturned.GetAsyncByShopAndIdDTO(id, shopId);
        }*/
        
        public override async Task<List<ProductReturned>> AllAsync()
        {
            return (await Uow.ProductsReturned.AllAsync()).Select(e => ProductReturnedMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<ProductReturned> FindAsync(params object[] id)
        {
            return ProductReturnedMapper.MapFromDAL(await Uow.ProductsReturned.FindAsync(id));
        }

        public async Task<List<ProductReturned>> AllAsyncByReturnId(int returnId)
        {
            return (await Uow.ProductsReturned.AllAsyncByReturnId(returnId))
                .Select(e => ProductReturnedMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<ProductReturned>> AllAsyncByShop(int? shopId)
        {
            return (await Uow.ProductsReturned.AllAsyncByShop(shopId)).Select(e => ProductReturnedMapper.MapFromDAL(e)).ToList();
        }

        public async Task<int> CountProductsInReturn(int returnId)
        {
            return await Uow.ProductsReturned.CountProductsInReturn(returnId);
        }

        public async Task<List<DTO.ProductReturned>> AllAsyncByShopDTO(int? shopId)
        {
            return (await Uow.ProductsReturned.AllAsyncByShopDTO(shopId)).Select(e => ProductReturnedMapper.MapFromDAL(e)).ToList();
            
        }

        public async Task<DTO.ProductReturned> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return ProductReturnedMapper.MapFromDAL(await Uow.ProductsReturned.GetAsyncByShopAndIdDTO(id, shopId));
        }

        public async Task<List<DTO.ProductReturned>> AllAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return (await Uow.ProductsReturned.AllAsyncByShopAndIdDTO(id, shopId)).Select(e => ProductReturnedMapper.MapFromDAL(e)).ToList();
        }
    }
}