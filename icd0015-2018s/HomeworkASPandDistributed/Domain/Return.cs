using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Return : DomainEntity
    {
        public int ReturnDescriptionId { get; set; }
        
        [ForeignKey(nameof(ReturnDescriptionId))]
        public MultiLangString Description { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductReturned> ProductsReturned { get; set; }
    }
}