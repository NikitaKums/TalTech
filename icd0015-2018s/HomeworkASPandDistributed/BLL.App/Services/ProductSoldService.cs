using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using ProductSold = BLL.App.DTO.DomainLikeDTO.ProductSold;

namespace BLL.App.Services
{
    public class ProductSoldService :
        BaseEntityService<BLL.App.DTO.DomainLikeDTO.ProductSold, DAL.App.DTO.DomainLikeDTO.ProductSold, IAppUnitOfWork>,
        IProductSoldService
    {
        public ProductSoldService(IAppUnitOfWork uow) : base(uow, new ProductSoldMapper())
        {
            ServiceRepository = Uow.ProductsSold;
        }


        /*public override async Task<IEnumerable<ProductSold>> AllAsync()
        {
            return await UOW.ProductsSold.AllAsync();
        }

        public override async Task<ProductSold> FindAsync(params object[] id)
        {
            return await UOW.ProductsSold.FindAsync(id);
        }

        public async Task<IEnumerable<ProductSold>> AllAsyncByShop(int? shopId)
        {
            return await UOW.ProductsSold.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<ProductSoldDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.ProductsSold.AllAsyncByShopDTO(shopId);
        }

        public async Task<ProductSoldDTO> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return await UOW.ProductsSold.GetAsyncByShopAndIdDTO(id, shopId);
        }

        public async Task<int> GetQuantity(int id)
        {
            return await UOW.ProductsSold.GetQuantity(id);
        }*/

        public async Task DeleteWithRestoration(int id)
        {
            var productSold = await Uow.ProductsSold.FindAsync(id);
            var product = await Uow.Products.FindProductInfoAsync(productSold.ProductId);
            product.Quantity = product.Quantity + productSold.Quantity;
            Uow.Products.Update(product);

            Uow.ProductsSold.Remove(id);
            await Uow.SaveChangesAsync();
        }

        public async Task<bool> EditProductInSale(int id, int productId, ProductSold productSold)
        {
            var product = await Uow.Products.FindProductInfoAsync(productId);
            var initialQuantity = await Uow.ProductsSold.GetQuantity(id);

            var quantityBeforeChange = product.Quantity + initialQuantity;


            if (quantityBeforeChange - productSold.Quantity < 0)
            {
                return false;
            }

            product.Quantity = quantityBeforeChange - productSold.Quantity;
            Uow.Products.Update(product);
            Uow.ProductsSold.Update(ProductSoldMapper.MapFromBLL(productSold));
            await Uow.SaveChangesAsync();
            return true;
        }


        public async Task<List<DTO.ProductSold>> AllAsyncByShopAndSaleId(int id, int? shopId)
        {
            return (await Uow.ProductsSold.AllAsyncByShopAndSaleId(id, shopId))
                .Select(e => ProductSoldMapper.MapFromDAL(e)).ToList();
        }

        public async Task<bool> AddProductToSale(int productId, ProductSold productSold)
        {
            var product = await Uow.Products.FindProductInfoAsync(productId);
            // subtract quantity (check if enough in stock)
            if (product.Quantity - productSold.Quantity < 0)
            {
                return false;
            }

            product.Quantity -= productSold.Quantity;
            Uow.Products.Update(product);
            await Uow.ProductsSold.AddAsync(ProductSoldMapper.MapFromBLL(productSold));
            await Uow.SaveChangesAsync();
            return true;
        }

        public override async Task<List<ProductSold>> AllAsync()
        {
            var res = (await Uow.ProductsSold.AllAsync()).Select(e => ProductSoldMapper.MapFromDAL(e)).ToList();
            foreach (var result in res)
            {
                result.Sale = SaleMapper.MapFromDAL(await Uow.Sales.FindAsync(result.SaleId));
            }

            return res;
        }

        public override async Task<ProductSold> FindAsync(params object[] id)
        {
            var res = ProductSoldMapper.MapFromDAL(await Uow.ProductsSold.FindAsync(id));
            res.Sale = SaleMapper.MapFromDAL(await Uow.Sales.FindAsync(res.SaleId));
            return res;
        }

        public async Task<List<ProductSold>> FindBySaleId(int saleId)
        {
            return (await Uow.ProductsSold.FindBySaleId(saleId)).Select(e => ProductSoldMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<ProductSold>> AllAsyncByShop(int? shopId)
        {
            var res = (await Uow.ProductsSold.AllAsyncByShop(shopId)).Select(e => ProductSoldMapper.MapFromDAL(e))
                .ToList();
            foreach (var result in res)
            {
                result.Sale = SaleMapper.MapFromDAL(await Uow.Sales.FindAsync(result.SaleId));
            }

            return res;
        }

        public async Task<int> GetQuantity(int id)
        {
            return await Uow.ProductsSold.GetQuantity(id);
        }

        public async Task<int> CountProductsInSale(int id)
        {
            return await Uow.ProductsSold.CountProductsInSale(id);
        }

        public async Task<List<DTO.ProductSold>> AllAsyncByShopDTO(int? shopId)
        {
            return (await Uow.ProductsSold.AllAsyncByShopDTO(shopId)).Select(e => ProductSoldMapper.MapFromDAL(e))
                .ToList();
        }

        public async Task<DTO.ProductSold> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return ProductSoldMapper.MapFromDAL(await Uow.ProductsSold.GetAsyncByShopAndIdDTO(id, shopId));
        }
    }
}