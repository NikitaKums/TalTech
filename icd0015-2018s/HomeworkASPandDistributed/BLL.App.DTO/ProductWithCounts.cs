using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.IdAndNameDTO;

namespace BLL.App.DTO
{
    // for views, separate requests for dropdowns then
    public class ProductWithCounts : ProductIdName
    {
        [MaxLength(512)]
        [MinLength(1)]
        [Required]
        public string ManuFacturerItemCode { get; set; }
        
        [MaxLength(512)]
        [MinLength(1)]
        [Required]
        public string ShopCode { get; set; }
        
        [Range(1, int.MaxValue)]
        [Required]
        public decimal BuyPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        public int PercentageAddedToBuyPrice { get; set; }
        
        [Range(1, int.MaxValue)]
        public decimal? SellPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        public string Weight { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        public string Length { get; set; }
        
        public int ManuFacturerId { get; set; }
        public string ManuFacturerName { get; set; }
        
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        
        public int? InventoryId { get; set; }
        public string InventoryName { get; set; }
        
        public List<CategoryIdName> CategoryDTOs { get; set; } // multiple
        public List<CommentIdTitleBody> CommentDTOs { get; set; } // multiple
        
        public int ProductsInOrdersCount { get; set; }
        public int ProductsSoldCount { get; set; }
        public int ProductReturnsCount { get; set; }
        public int ProductsWithDefectCount { get; set; }
    }
}