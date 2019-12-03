using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Defect : DomainEntity
    {        
        public int DefectDescriptionId { get; set; }
        
        [ForeignKey(nameof(DefectDescriptionId))]
        public MultiLangString Description { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductWithDefect> ProductsWithDefect { get; set; }
    }
}