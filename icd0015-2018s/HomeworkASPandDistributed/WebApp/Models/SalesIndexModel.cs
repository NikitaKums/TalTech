using System.Collections.Generic;

namespace WebApp.Models
{
    public class SalesIndexModel
    {
        public PaginatedList<BLL.App.DTO.DomainLikeDTO.Sale> Sales { get; set; }
        public Dictionary<string, decimal?> SaleAmounts { get; set; }
    }
}