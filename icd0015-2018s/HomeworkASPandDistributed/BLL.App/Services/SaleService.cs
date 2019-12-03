using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Sale = BLL.App.DTO.DomainLikeDTO.Sale;

namespace BLL.App.Services
{
    public class SaleService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Sale, DAL.App.DTO.DomainLikeDTO.Sale, IAppUnitOfWork>, ISaleService
    {
        public SaleService(IAppUnitOfWork uow) : base(uow, new SaleMapper())
        {
            ServiceRepository = Uow.Sales;
        }


        /*public override async Task<IEnumerable<Sale>> AllAsync()
        {
            return await UOW.Sales.AllAsync();
        }

        public override async Task<Sale> FindAsync(params object[] id)
        {
            return await UOW.Sales.FindAsync(id);
        }

        public async Task<IEnumerable<Sale>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Sales.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<SaleDTO>> AllAsyncByShopDTO(int? shopId)
        {
            return await UOW.Sales.AllAsyncByShopDTO(shopId);
        }

        public async Task<SaleDTO> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return await UOW.Sales.GetAsyncByShopAndIdDTO(id, shopId);
        }

        public async Task<IEnumerable<Sale>> AllAsyncByShopAndUserId(int? shopId, int userId)
        {
            return await UOW.Sales.AllAsyncByShopAndUserId(shopId, userId);
        }

        public async Task<int> CountProductsInSale(int id)
        {
            return await UOW.Sales.CountProductsInSale(id);
        }

        public async Task<IEnumerable<SaleIdNameDTO>> GetAsyncByShopAndUserIdDTO(int userId, int? shopId)
        {
            return await UOW.Sales.GetAsyncByShopAndUserIdDTO(userId, shopId);
        }*/
        
        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Sales.CountDataAmount(search);
        }

        public async Task<List<Sale>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Sales.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => SaleMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Sale>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Sales.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => SaleMapper.MapFromDAL(e)).ToList();
        }

        public async Task DeleteWithoutRestoration(int saleId)
        {
            var productsInSale = await Uow.ProductsSold.FindBySaleId(saleId);

            foreach (var productInSale in productsInSale)
            {
                Uow.ProductsSold.Remove(productInSale.Id);
            }
            
            Uow.Sales.Remove(saleId);
            await Uow.SaveChangesAsync();
        }

        public async Task DeleteWithRestoration(int saleId)
        {
            var productsInSale = await Uow.ProductsSold.FindBySaleId(saleId);

            foreach (var productInSale in productsInSale)
            {
                var product = await Uow.Products.FindAsync(productInSale.ProductId);
                product.Quantity += productInSale.Quantity;
                Uow.Products.Update(product);
                Uow.ProductsSold.Remove(productInSale.Id);
                await Uow.SaveChangesAsync();
            }
            
            Uow.Sales.Remove(saleId);
            await Uow.SaveChangesAsync();
        }
        
        
        public override async Task<List<Sale>> AllAsync()
        {
            return (await Uow.Sales.AllAsync()).Select(e => SaleMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Sale> FindAsync(params object[] id)
        {
            return SaleMapper.MapFromDAL(await Uow.Sales.FindAsync(id));
        }

        public async Task<List<Sale>> AllAsyncByShopAndUserId(int? shopId, int userId)
        {
            return (await Uow.Sales.AllAsyncByShopAndUserId(shopId, userId)).Select(e => SaleMapper.MapFromDAL(e)).ToList();
        }

        public async Task<Dictionary<string, decimal?>> GetSaleAmounts(int? shopId)
        {
            return await Uow.Sales.GetSaleAmounts(shopId);
        }

        public async Task<List<SaleWithProductCount>> GetAsyncByShopAndUserIdDTO(int userId, int? shopId)
        {
            return (await Uow.Sales.GetAsyncByShopAndUserIdDTO(userId, shopId)).Select(e => SaleMapper.MapFromDAL(e)).ToList();
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Sales.CountDataAmount(shopId, search);
        }

        public async Task<List<SaleWithProductCount>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Sales.AllAsyncByShopDTO(shopId, search, pageIndex, pageSize)).Select(e => SaleMapper.MapFromDAL(e)).ToList();
        }

        public async Task<SaleWithProductCount> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            return SaleMapper.MapFromDAL(await Uow.Sales.GetAsyncByShopAndIdDTO(id, shopId));
        }
    }
}