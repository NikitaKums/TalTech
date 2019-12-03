using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.nikita.BLL.Base.Services;
using Product = BLL.App.DTO.DomainLikeDTO.Product;

namespace BLL.App.Services
{
    public class ProductService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Product, DAL.App.DTO.DomainLikeDTO.Product, IAppUnitOfWork>, IProductService
    {
        public ProductService(IAppUnitOfWork uow) : base(uow, new ProductMapper())
        {
            ServiceRepository = Uow.Products;
        }


        /*public override async Task<IEnumerable<Product>> AllAsync()
        {
            return await UOW.Products.AllAsync();
        }

        public override async Task<Product> FindAsync(params object[] id)
        {
            return await UOW.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Products.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ProductDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.Products.AllAsyncByShopDTO(shopId);
        }

        public async Task<IEnumerable<ProductIdNameDTO>> GetProductIdNameByShopDTO(int? shopId)
        {
            return await UOW.Products.GetProductIdNameByShopDTO(shopId);
        }

        public async Task<ProductDTO> FindByShopAndId(int id, int? shopId)
        {
            return await UOW.Products.FindByShopAndId(id, shopId);
        }

        public async Task<int> CountProductsInShop(int? shopId)
        {
            return await UOW.Products.CountProductsInShop(shopId);
        }

        public async Task<IEnumerable<ProductIdNameDTO>> GetProductIdNameByShopInInventoryDTO(int? shopId)
        {
            return await UOW.Products.GetProductIdNameByShopInInventoryDTO(shopId);
        }

        public async Task<IEnumerable<Product>> AllAsyncByShopAndInInventory(int? shopId)
        {
            return await UOW.Products.AllAsyncByShopAndInInventory(shopId);
        }*/
        
        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Products.CountDataAmount(search);
        }

        public override async Task<List<Product>> AllAsync()
        {
            return (await Uow.Products.AllAsync()).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Product> FindAsync(params object[] id)
        {
            return ProductMapper.MapFromDAL(await Uow.Products.FindAsync(id));
        }

        public async Task<Product> FindProductInfoAsync(int id)
        {
            return ProductMapper.MapFromDAL(await Uow.Products.FindProductInfoAsync(id));
        }

        public async Task<List<Product>> AllAsyncByShopForDropDown(int? shopId)
        {
            return (await Uow.Products.AllAsyncByShopForDropDown(shopId)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }
        
        public async Task<List<Product>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Products.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }


        public async Task<List<Product>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Products.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }


        public async Task<int> CountProductsInShop(int? shopId)
        {
            return await Uow.Products.CountProductsInShop(shopId);
        }

        public async Task<List<Product>> AllAsyncByShopAndInInventory(int? shopId)
        {
            return (await Uow.Products.AllAsyncByShopAndInInventory(shopId)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }

        public async Task<ProductWithCounts> FindByShopAndId(int id, int? shopId)
        {
            return ProductMapper.MapFromDAL(await Uow.Products.FindByShopAndId(id, shopId));
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Products.CountDataAmount(shopId, search);
        }

        public async Task<List<ProductWithCounts>> GetProductIdNameByShopInInventoryDTO(int? shopId)
        {
            return (await Uow.Products.GetProductIdNameByShopInInventoryDTO(shopId)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<ProductWithCounts>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Products.AllAsyncByShopDTO(shopId, search, pageIndex, pageSize)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<ProductWithCounts>> GetProductIdNameByShopDTO(int? shopId)
        {
            return (await Uow.Products.GetProductIdNameByShopDTO(shopId)).Select(e => ProductMapper.MapFromDAL(e)).ToList();
        }
    }
}