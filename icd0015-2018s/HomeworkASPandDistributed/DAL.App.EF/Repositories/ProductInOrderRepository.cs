using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductInOrder = DAL.App.DTO.ProductInOrder;

namespace DAL.App.EF.Repositories
{
    public class ProductInOrderRepository :
        BaseRepository<DAL.App.DTO.DomainLikeDTO.ProductInOrder, Domain.ProductInOrder, AppDbContext>,
        IProductInOrderRepository
    {

        public ProductInOrderRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ProductInOrderMapper())
        {
        }

        public override async Task<List<DTO.DomainLikeDTO.ProductInOrder>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Select(e => ProductInOrderMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<DTO.DomainLikeDTO.ProductInOrder> FindAsync(params object[] id)
        {
            var productInOrder = await RepositoryDbSet.FindAsync(id);

            return ProductInOrderMapper.MapFromDomain(await RepositoryDbSet.Where(a => a.Id == productInOrder.Id)
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());

        }

        public async Task<List<DTO.DomainLikeDTO.ProductInOrder>> AllAsyncByShop(int? shopId)
        {
            return await RepositoryDbSet
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId).Select(e => ProductInOrderMapper.MapFromDomain(e))
                .ToListAsync();
        }

        public async Task<List<ProductInOrder>> AllAsyncByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId)
                .Select(p => new
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    OrderDescription = p.Order.Description,
                    ProductId = p.ProductId,
                    ProductInOrderPlacingTime = p.ProductInOrderPlacingTime,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    OrderDescriptionTranslations = p.Order.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(p => new ProductInOrder()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                OrderDescription = p.OrderDescription.Translate(),
                ProductId = p.ProductId,
                ProductInOrderPlacingTime = p.ProductInOrderPlacingTime,
                ProductName = p.ProductName.Translate(),
                Quantity = p.Quantity
            }).ToList();

            return result;
        }

        public async Task<ProductInOrder> GetAsyncByShopAndIdDTO(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    OrderDescription = p.Order.Description,
                    ProductId = p.ProductId,
                    ProductInOrderPlacingTime = p.ProductInOrderPlacingTime,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    OrderDescriptionTranslations = p.Order.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .FirstOrDefaultAsync();

            var result = new ProductInOrder()
            {
                Id = res.Id,
                OrderId = res.OrderId,
                OrderDescription = res.OrderDescription.Translate(),
                ProductId = res.ProductId,
                ProductInOrderPlacingTime = res.ProductInOrderPlacingTime,
                ProductName = res.ProductName.Translate(),
                Quantity = res.Quantity
            };

            return result;
        }

        public async Task<List<DTO.DomainLikeDTO.ProductInOrder>> AllAsyncByOrderId(int orderId)
        {
            return await RepositoryDbSet
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(p => p.OrderId == orderId).Select(e => ProductInOrderMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<List<OrderReceived>> FindOrdersReceivedByOrderId(int orderId, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(s => s.Order)
                .Include(s => s.Product)
                .ThenInclude(ss => ss.ProductName).ThenInclude(t => t.Translations)
                .Where(s => s.OrderId == orderId && s.Product.ShopId == shopId)
                .Select(e => new
                {
                    OrderId = e.OrderId,
                    ProductName = e.Product.ProductName,
                    ProductId = e.ProductId,
                    ProductInOrderId = e.Id,
                    Quantity = e.Quantity,
                    ProductNameTranslations = e.Product.ProductName.Translations
                }).ToListAsync();

            var result = res.Select(e => new OrderReceived()
            {
                OrderId = e.OrderId,
                ProductId = e.ProductId,
                ProductName = e.ProductName.Translate(),
                ProductInOrderId = e.ProductInOrderId,
                Quantity = e.Quantity
            }).ToList();

            return result;
        }

        public async Task<List<ProductInOrder>> AllAsyncByShopAndOrderId(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(p => p.Order).ThenInclude(m => m.Description).ThenInclude(t => t.Translations)
                .Include(a => a.Product).ThenInclude(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(p => p.Product.ShopId == shopId && p.OrderId == id)
                .Select(p => new
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    OrderDescription = p.Order.Description,
                    ProductId = p.ProductId,
                    ProductInOrderPlacingTime = p.ProductInOrderPlacingTime,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    OrderDescriptionTranslations = p.Order.Description.Translations,
                    ProductNameTranslations = p.Product.ProductName.Translations
                })
                .ToListAsync();

            var result = res.Select(e => new ProductInOrder()
            {
                Id = e.Id,
                OrderId = e.OrderId,
                OrderDescription = e.OrderDescription.Translate(),
                ProductId = e.ProductId,
                ProductInOrderPlacingTime = e.ProductInOrderPlacingTime,
                ProductName = e.ProductName.Translate(),
                Quantity = e.Quantity
            }).ToList();

            return result;
            
        }
    }
}