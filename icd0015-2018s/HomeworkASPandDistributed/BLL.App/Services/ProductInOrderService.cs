using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using ProductInOrder = BLL.App.DTO.DomainLikeDTO.ProductInOrder;

namespace BLL.App.Services
{
    public class ProductInOrderService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.ProductInOrder, DAL.App.DTO.DomainLikeDTO.ProductInOrder, IAppUnitOfWork>, IProductInOrderService
    {
        public ProductInOrderService(IAppUnitOfWork uow) : base(uow, new ProductInOrderMapper())
        {
            ServiceRepository = Uow.ProductsInOrder;
        }


        /*public override async Task<IEnumerable<ProductInOrder>> AllAsync()
        {
            return await UOW.ProductsInOrder.AllAsync();
        }

        public override async Task<ProductInOrder> FindAsync(params object[] id)
        {
            return await UOW.ProductsInOrder.FindAsync(id);
        }

        public async Task<IEnumerable<ProductInOrder>> AllAsyncByShop(int? shopId)
        {
            return await UOW.ProductsInOrder.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ProductInOrderDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.ProductsInOrder.AllAsyncByShopDTO(shopId);
        }

        public async Task<ProductInOrderDTO> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return await UOW.ProductsInOrder.GetAsyncByShopAndIdDTO(id, shopId);
        }*/

        public async Task<List<OrderReceived>> FindOrdersReceivedByOrderId(int orderId, int? shopId)
        {
            return (await Uow.ProductsInOrder.FindOrdersReceivedByOrderId(orderId, shopId))
                .Select(e => ProductInOrderMapper.MapFromDAL(e)).ToList();
        }

        public async Task ProductInOrderReceived(int id)
        {
            var productInOrder = await Uow.ProductsInOrder.FindAsync(id);
            var product = await Uow.Products.FindAsync(productInOrder.ProductId);
            product.Quantity += productInOrder.Quantity;
            Uow.Products.Update(product);
            Uow.ProductsInOrder.Remove(id);
            await Uow.SaveChangesAsync();
        }

        public async Task<List<DTO.ProductInOrder>> AllAsyncByShopAndOrderId(int id, int? shopId)
        {
            return (await Uow.ProductsInOrder.AllAsyncByShopAndOrderId(id, shopId)).Select(e => ProductInOrderMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<List<ProductInOrder>> AllAsync()
        {
            return (await Uow.ProductsInOrder.AllAsync()).Select(e => ProductInOrderMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<ProductInOrder> FindAsync(params object[] id)
        {
            return ProductInOrderMapper.MapFromDAL(await Uow.ProductsInOrder.FindAsync(id));
        }

        public async Task<List<DTO.DomainLikeDTO.ProductInOrder>> AllAsyncByOrderId(int orderId)
        {
            return (await Uow.ProductsInOrder.AllAsyncByOrderId(orderId)).Select(e => ProductInOrderMapper.MapFromDAL(e)).ToList();
        }
        
        public async Task<List<ProductInOrder>> AllAsyncByShop(int? shopId)
        {
            return (await Uow.ProductsInOrder.AllAsyncByShop(shopId)).Select(e => ProductInOrderMapper.MapFromDAL(e))
                .ToList();
        }

        public async Task<List<DTO.ProductInOrder>> AllAsyncByShopDTO(int? shopId)
        {
            return (await Uow.ProductsInOrder.AllAsyncByShopDTO(shopId)).Select(e => ProductInOrderMapper.MapFromDAL(e))
                .ToList();
        }

        public async Task<DTO.ProductInOrder> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return ProductInOrderMapper.MapFromDAL(await Uow.ProductsInOrder.GetAsyncByShopAndIdDTO(id, shopId));
        }
    }
}