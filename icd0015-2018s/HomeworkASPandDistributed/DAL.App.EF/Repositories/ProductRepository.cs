using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.IdAndNameDTO;
using DAL.App.EF.Mappers;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Product = DAL.App.DTO.DomainLikeDTO.Product;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository : BaseRepository<DAL.App.DTO.DomainLikeDTO.Product, Domain.Product, AppDbContext>,
        IProductRepository
    {

        public ProductRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ProductMapper())
        {
        }
        
        public override Product Update(Product entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);

            if (entityInDb == null) return entity;
            
            entityInDb.Quantity = entity.Quantity;
            entityInDb.BuyPrice = entity.BuyPrice;
            entityInDb.PercentageAddedToBuyPrice = entity.PercentageAddedToBuyPrice;
            entityInDb.SellPrice = entity.SellPrice;
            entityInDb.ManuFacturerId = entity.ManuFacturerId;
            entityInDb.InventoryId = entity.InventoryId;
            entityInDb.ShopId = entity.ShopId;
            RepositoryDbSet.Update(entityInDb);

            entityInDb.ProductName.SetTranslation(entity.ProductName);
            entityInDb.Length.SetTranslation(entity.Length);
            entityInDb.Weight.SetTranslation(entity.Weight);
            entityInDb.ManuFacturerItemCode.SetTranslation(entity.ManuFacturerItemCode);
            entityInDb.ShopCode.SetTranslation(entity.ShopCode);

            return entity;
        }

        public override async Task<List<Product>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Select(e => ProductMapper.MapFromDomain(e)).ToListAsync();
        }

        public override async Task<Product> FindAsync(params object[] id)
        {
            var product = await RepositoryDbContext.Set<Domain.Product>().FindAsync(id);
            RepositoryDbContext.Entry(product).State = EntityState.Detached;

            return ProductMapper.MapFromDomain(await RepositoryDbSet.AsNoTracking().Where(a => a.Id == product.Id)
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .FirstOrDefaultAsync());
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(p => p.ProductsReturned)
                .Include(p => p.ProductsInOrder)
                .Include(p => p.ProductsSold)
                .Include(p => p.ProductsWithDefect)
                .Include(p => p.ProductsInCategory).ThenInclude(aa => aa.Category).ThenInclude(aaa => aaa.CategoryName)
                .ThenInclude(t => t.Translations).AsQueryable();

            query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<Product> FindProductInfoAsync(int id)
        {
            var product = await RepositoryDbContext.Set<Domain.Product>().FindAsync(id);
            RepositoryDbContext.Entry(product).State = EntityState.Detached;

            return ProductMapper.MapFromDomain(await RepositoryDbSet.AsNoTracking().Where(a => a.Id == product.Id)
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(s => s.Shop)
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync());
        }

        public async Task<List<DTO.DomainLikeDTO.Product>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId)
                .AsQueryable();

            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ProductMapper.MapFromDomain(e)).ToList();
            return res;
        }

        public async Task<List<Product>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .AsQueryable();
            
            query = Search(query, searchFor);
            query = Order(query, order);
            if (pageIndex != null && pageSize != null)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            var temp = await query.ToListAsync();
            var res = temp.Select(e => ProductMapper.MapFromDomain(e)).ToList();
            return res;
            
        }
        
        private IQueryable<Domain.Product> Search(IQueryable<Domain.Product> query, string searchFor)
        {
            if (!string.IsNullOrWhiteSpace(searchFor))
            {
                searchFor = searchFor.ToLower().Trim();
                query = query.Where(s => 
                    s.ProductName.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.Manufacturer.ManuFacturerName.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    (s.Weight != null && s.Weight.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    (s.Length != null && s.Length.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.ShopCode.Translations.Any(t => t.Value.ToLower().Contains(searchFor)) ||
                    s.ManuFacturerItemCode.Translations.Any(t => t.Value.ToLower().Contains(searchFor))
                ))).AsQueryable();
                
            }
            return query;
        }

        private IQueryable<Domain.Product>  Order(IQueryable<Domain.Product>  res, string order)
        {
            switch (order)
            {
                case "manufacturer_desc":
                    return res.OrderByDescending(s => s.Manufacturer.ManuFacturerName.Value);
                case "manufacturer":
                    return res.OrderBy(s => s.Manufacturer.ManuFacturerName.Value);
                case "length_desc":
                    return res.OrderByDescending(s => s.Length.Value);
                case "length":
                    return res.OrderBy(s => s.Length.Value);
                case "weight_desc":
                    return res.OrderByDescending(s => s.Weight.Value);
                case "weight":
                    return res.OrderBy(s => s.Weight.Value);
                case "quantity_desc":
                    return res.OrderByDescending(s => s.Quantity);
                case "quantity":
                    return res.OrderBy(s => s.Quantity);
                case "percentage_desc":
                    return res.OrderByDescending(s => s.PercentageAddedToBuyPrice);
                case "percentage":
                    return res.OrderBy(s => s.PercentageAddedToBuyPrice);
                case "sellPrice_desc":
                    return res.OrderByDescending(s => s.SellPrice);
                case "sellPrice":
                    return res.OrderBy(s => s.SellPrice);
                case "buyPrice_desc":
                    return res.OrderByDescending(s => s.BuyPrice);
                case "buyPrice":
                    return res.OrderBy(s => s.BuyPrice);
                case "shopCode_desc":
                    return res.OrderByDescending(s => s.ShopCode.Value);
                case "shopCode":
                    return res.OrderBy(s => s.ShopCode.Value);
                case "manufacturerCode_desc":
                    return res.OrderByDescending(s => s.ManuFacturerItemCode.Value);
                case "manufacturerCode":
                    return res.OrderBy(s => s.ManuFacturerItemCode.Value);
                case "productName_desc":
                    return res.OrderByDescending(s => s.ProductName.Value);
                default:
                    return res.OrderBy(s => s.ProductName.Value);
            }
        }

        public async Task<List<DTO.DomainLikeDTO.Product>> AllAsyncByShopForDropDown(int? shopId)
        {
            return await RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId)
                .Select(e => ProductMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<int> CountProductsInShop(int? shopId)
        {
            return await RepositoryDbSet
                .Where(p => p.ShopId == shopId).CountAsync();
        }

        public async Task<List<DTO.DomainLikeDTO.Product>> AllAsyncByShopAndInInventory(int? shopId)
        {
            return await RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.Aadress).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.PhoneNumber).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopAddress).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopContact2).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(a => a.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId && p.InventoryId != null)
                .Select(e => ProductMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<ProductWithCounts> FindByShopAndId(int id, int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(p => p.ProductsInCategory).ThenInclude(aa => aa.Category).ThenInclude(aaa => aaa.CategoryName).ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId && p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ManuFacturerItemCode = p.ManuFacturerItemCode,
                    ShopCode = p.ShopCode,
                    BuyPrice = p.BuyPrice,
                    PercentageAddedToBuyPrice = p.PercentageAddedToBuyPrice,
                    SellPrice = p.SellPrice,
                    Quantity = p.Quantity,
                    Weight = p.Weight,
                    Length = p.Length,
                    ShopId = p.ShopId,
                    ShopName = p.Shop.ShopName,
                    ManuFacturerId = p.ManuFacturerId,
                    ManuFacturerName = p.Manufacturer.ManuFacturerName,
                    InventoryId = p.InventoryId,
                    InventoryName = p.Inventory.Description,
                    ProductsInOrdersCount = p.ProductsInOrder.Count,
                    ProductsSoldCount = p.ProductsSold.Count,
                    ProductReturnsCount = p.ProductsReturned.Count,
                    ProductsWithDefectCount = p.ProductsWithDefect.Count,
                    CategoryDTOs = p.ProductsInCategory.Select(a => new
                    {
                        CategoryName = a.Category.CategoryName,
                        Id = a.CategoryId,
                        CategoryNameTranslations = a.Category.CategoryName.Translations
                    }).ToList(),
                    CommentDTOs = p.Comments.Select(a => new
                    {
                        Id = a.Id,
                        CommentTitle = a.CommentTitle,
                        CommentBody = a.CommentBody,
                        CommentTitleTranslations = a.CommentTitle.Translations,
                        CommentBodyTranslations = a.CommentBody.Translations
                    }).ToList(),
                    ProductNameTranslations = p.ProductName.Translations,
                    ManuFacturerCodeTranslations = p.ManuFacturerItemCode.Translations,
                    ShopCodeTranslations = p.ShopCode.Translations,
                    WeightTranslations = p.Weight.Translations,
                    LengthTranslations = p.Length.Translations,
                    ShopNameTranslations = p.Shop.ShopName.Translations,
                    ManuFacurerNameTranslations = p.Manufacturer.ManuFacturerName.Translations,
                    InventoryNameTranslations = p.Inventory.Description.Translations
                }).FirstOrDefaultAsync();

            var result = new ProductWithCounts()
            {
                Id = res.Id,
                ProductName = res.ProductName?.Translate(),
                ManuFacturerItemCode = res.ManuFacturerItemCode?.Translate(),
                ShopCode = res.ShopCode?.Translate(),
                BuyPrice = res.BuyPrice,
                PercentageAddedToBuyPrice = res.PercentageAddedToBuyPrice,
                SellPrice = res.SellPrice,
                Quantity = res.Quantity,
                Weight = res.Weight?.Translate(),
                Length = res.Length?.Translate(),
                ShopId = res.ShopId,
                ShopName = res.ShopName?.Translate(),
                ManuFacturerId = res.ManuFacturerId,
                ManuFacturerName = res.ManuFacturerName?.Translate(),
                InventoryId = res.InventoryId,
                InventoryName = res.InventoryName?.Translate(),
                ProductsInOrdersCount = res.ProductsInOrdersCount,
                ProductsSoldCount = res.ProductsSoldCount,
                ProductReturnsCount = res.ProductReturnsCount,
                ProductsWithDefectCount = res.ProductsWithDefectCount,
                CategoryDTOs = res.CategoryDTOs.Select(a => new CategoryIdName()
                {
                    CategoryName = a.CategoryName?.Translate(),
                    Id = a.Id
                }).ToList(),
                CommentDTOs = res.CommentDTOs.Select(a => new CommentIdTitleBody()
                {
                    Id = a.Id,
                    CommentTitle = a.CommentTitle?.Translate(),
                    CommentBody = a.CommentBody?.Translate()
                }).ToList()
            };

            return result;
        }

        public async Task<List<ProductWithCounts>> GetProductIdNameByShopInInventoryDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId && s.InventoryId != null)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductNameTranslations = p.ProductName.Translations
                }).ToListAsync();

            var result = res.Select(p => new ProductWithCounts()
            {
                Id = p.Id,
                ProductName = p.ProductName?.Translate()
            }).ToList();

            return result;

        }
        
        public virtual async Task<int> CountDataAmount(int? shopId, string search)
        {
            var query = RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(p => p.ProductsReturned)
                .Include(p => p.ProductsInOrder)
                .Include(p => p.ProductsSold)
                .Include(p => p.ProductsWithDefect)
                .Include(p => p.ProductsInCategory).ThenInclude(aa => aa.Category).ThenInclude(aaa => aaa.CategoryName)
                .ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId).AsQueryable();

                query = Search(query, search);
            return await query.CountAsync();
        }

        public async Task<List<ProductWithCounts>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Include(p => p.Inventory).ThenInclude(i => i.Description).ThenInclude(t => t.Translations)
                .Include(m => m.Manufacturer).ThenInclude(mm => mm.ManuFacturerName).ThenInclude(t => t.Translations)
                .Include(a => a.Shop).ThenInclude(aa => aa.ShopName).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentTitle).ThenInclude(t => t.Translations)
                .Include(p => p.Comments).ThenInclude(aa => aa.CommentBody).ThenInclude(t => t.Translations)
                .Include(p => p.ProductsReturned)
                .Include(p => p.ProductsInOrder)
                .Include(p => p.ProductsSold)
                .Include(p => p.ProductsWithDefect)
                .Include(p => p.ProductsInCategory).ThenInclude(aa => aa.Category).ThenInclude(aaa => aaa.CategoryName)
                .ThenInclude(t => t.Translations)
                .Where(p => p.ShopId == shopId).AsQueryable();
            
            query = Search(query, search);
            
            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }
            
            var res = await query
                .Select(p => new
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ManuFacturerItemCode = p.ManuFacturerItemCode,
                    ShopCode = p.ShopCode,
                    BuyPrice = p.BuyPrice,
                    PercentageAddedToBuyPrice = p.PercentageAddedToBuyPrice,
                    SellPrice = p.SellPrice,
                    Quantity = p.Quantity,
                    Weight = p.Weight,
                    Length = p.Length,
                    ShopId = p.ShopId,
                    ShopName = p.Shop.ShopName,
                    ManuFacturerId = p.ManuFacturerId,
                    ManuFacturerName = p.Manufacturer.ManuFacturerName,
                    InventoryId = p.InventoryId,
                    InventoryName = p.Inventory.Description,
                    ProductsInOrdersCount = p.ProductsInOrder.Count,
                    ProductsSoldCount = p.ProductsSold.Count,
                    ProductReturnsCount = p.ProductsReturned.Count,
                    ProductsWithDefectCount = p.ProductsWithDefect.Count,
                    CategoryDTOs = p.ProductsInCategory.Select(a => new
                    {
                        CategoryName = a.Category.CategoryName,
                        Id = a.CategoryId,
                        CategoryNameTranslations = a.Category.CategoryName.Translations
                    }).ToList(),
                    CommentDTOs = p.Comments.Select(a => new
                    {
                        Id = a.Id,
                        CommentTitle = a.CommentTitle,
                        CommentBody = a.CommentBody,
                        CommentTitleTranslations = a.CommentTitle.Translations,
                        CommentBodyTranslations = a.CommentBody.Translations
                    }).ToList(),
                    ProductNameTranslations = p.ProductName.Translations,
                    ManuFacturerCodeTranslations = p.ManuFacturerItemCode.Translations,
                    ShopCodeTranslations = p.ShopCode.Translations,
                    WeightTranslations = p.Weight.Translations,
                    LengthTranslations = p.Length.Translations,
                    ShopNameTranslations = p.Shop.ShopName.Translations,
                    ManuFacurerNameTranslations = p.Manufacturer.ManuFacturerName.Translations,
                    InventoryNameTranslations = p.Inventory.Description.Translations
                }).ToListAsync();

            var result = res.Select(e => new ProductWithCounts()
            {
                Id = e.Id,
                ProductName = e.ProductName?.Translate(),
                ManuFacturerItemCode = e.ManuFacturerItemCode?.Translate(),
                ShopCode = e.ShopCode?.Translate(),
                BuyPrice = e.BuyPrice,
                PercentageAddedToBuyPrice = e.PercentageAddedToBuyPrice,
                SellPrice = e.SellPrice,
                Quantity = e.Quantity,
                Weight = e.Weight?.Translate(),
                Length = e.Length?.Translate(),
                ShopId = e.ShopId,
                ShopName = e.ShopName?.Translate(),
                ManuFacturerId = e.ManuFacturerId,
                ManuFacturerName = e.ManuFacturerName?.Translate(),
                InventoryId = e.InventoryId,
                InventoryName = e.InventoryName?.Translate(),
                ProductsInOrdersCount = e.ProductsInOrdersCount,
                ProductsSoldCount = e.ProductsSoldCount,
                ProductReturnsCount = e.ProductReturnsCount,
                ProductsWithDefectCount = e.ProductsWithDefectCount,
                CategoryDTOs = e.CategoryDTOs?.Select(a => new CategoryIdName()
                {
                    CategoryName = a.CategoryName?.Translate(),
                    Id = a.Id
                }).ToList(),
                CommentDTOs = e.CommentDTOs?.Select(a => new CommentIdTitleBody()
                {
                    Id = a.Id,
                    CommentTitle = a.CommentTitle?.Translate(),
                    CommentBody = a.CommentBody?.Translate()
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<List<ProductWithCounts>> GetProductIdNameByShopDTO(int? shopId)
        {
            var res = await RepositoryDbSet
                .Include(aa => aa.ProductName).ThenInclude(t => t.Translations)
                .Include(aa => aa.Length).ThenInclude(t => t.Translations)
                .Include(aa => aa.Weight).ThenInclude(t => t.Translations)
                .Include(aa => aa.ManuFacturerItemCode).ThenInclude(t => t.Translations)
                .Include(aa => aa.ShopCode).ThenInclude(t => t.Translations)
                .Where(s => s.ShopId == shopId)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductNameTranslations = p.ProductName.Translations
                }).ToListAsync();

            var result = res.Select(p => new ProductWithCounts()
            {
                Id = p.Id,
                ProductName = p.ProductName?.Translate()
            }).ToList();

            return result;
        }
        
        
    }
}