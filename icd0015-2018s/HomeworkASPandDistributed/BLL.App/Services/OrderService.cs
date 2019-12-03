using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Order = BLL.App.DTO.DomainLikeDTO.Order;

namespace BLL.App.Services
{
    public class OrderService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Order, DAL.App.DTO.DomainLikeDTO.Order, IAppUnitOfWork>, IOrderService
    {
        public OrderService(IAppUnitOfWork uow) : base(uow, new OrderMapper())
        {
            ServiceRepository = Uow.Orders;
        }


        /*public override async Task<IEnumerable<Order>> AllAsync()
        {
            return await UOW.Orders.AllAsync();
        }

        public override async Task<Order> FindAsync(params object[] id)
        {
            return await UOW.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Orders.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllByShopDTOAsync(int? shopId)
        {
            return await UOW.Orders.GetAllByShopDTOAsync(shopId);
        }

        public async Task<OrderDTO> FindByShopAndIdAsync(int id, int? shopId)
        {
            return await UOW.Orders.FindByShopAndIdAsync(id, shopId);
        }

        public async Task<int> CountOrdersInShop(int? shopId)
        {
            return await UOW.Orders.CountOrdersInShop(shopId);
        }

        public async Task<Order> FindAsyncById(int id)
        {
            return await UOW.Orders.FindAsyncById(id);
        }*/

        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Orders.CountDataAmount(search);
        }
        
        public async Task DeleteOrderWithProducts(int id)
        {
            var productInOrders = await Uow.ProductsInOrder.AllAsyncByOrderId(id);
            foreach (var productInOrder in productInOrders)
            {
                Uow.ProductsInOrder.Remove(productInOrder.Id);
            }
            Uow.Orders.Remove(id);
            await Uow.SaveChangesAsync();
        }

        public async Task<List<Order>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Orders.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => OrderMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Order>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Orders.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => OrderMapper.MapFromDAL(e)).ToList();
        }

        public async Task<bool> ProcessReceivedOrder(int id)
        {
            var productInOrders = await Uow.ProductsInOrder.AllAsyncByOrderId(id);
            if (productInOrders.Count == 0)
            {
                return false;
            }
            foreach (var productInOrder in productInOrders)
            {
                var product = await Uow.Products.FindProductInfoAsync(productInOrder.ProductId);
                product.Quantity += productInOrder.Quantity;
                Uow.Products.Update(product);
                await Uow.SaveChangesAsync(); // need to save in case same product is twice or more times in same order
                Uow.ProductsInOrder.Remove(productInOrder.Id);
            }
            Uow.Orders.Remove(id);
            await Uow.SaveChangesAsync();
            return true;
        }

        public override async Task<List<Order>> AllAsync()
        {
            return (await Uow.Orders.AllAsync()).Select(e => OrderMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Order> FindAsync(params object[] id)
        {
            var order = OrderMapper.MapFromDAL(await Uow.Orders.FindAsync(id));
            return order;
        }

        public async Task<int> CountOrdersInShop(int? shopId)
        {
            return await Uow.Orders.CountOrdersInShop(shopId);
        }

        public async Task<Order> FindAsyncById(int id)
        {
            var order = OrderMapper.MapFromDAL(await Uow.Orders.FindAsyncById(id));
            return order;
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Orders.CountDataAmount(shopId, search);
        }

        public async Task<List<OrderWithProductCount>> GetAllByShopDTOAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Orders.GetAllByShopDTOAsync(shopId, search, pageIndex, pageSize)).Select(e => OrderMapper.MapFromDAL(e)).ToList();
        }

        public async Task<OrderWithProductCount> FindByShopAndIdAsync(int id, int? shopId)
        {
            return OrderMapper.MapFromDAL(await Uow.Orders.FindByShopAndIdAsync(id, shopId));
        }
    }
}