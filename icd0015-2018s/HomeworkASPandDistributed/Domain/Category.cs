using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain
{
    public class Category : DomainEntity
    {
        public int CategoryNameId { get; set; }
        
        [ForeignKey(nameof(CategoryNameId))]
        public MultiLangString CategoryName { get; set; }
        
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductInCategory> ProductsInCategory { get; set; }
    }
}