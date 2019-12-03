using PublicApi.v1.DTO.IdAndNameDTO;

namespace PublicApi.v1.DTO
{
    public class CategoryWithProductCount : CategoryIdName
    {           
        public int? ShopId { get; set; }
        public int CategoryProductCount { get; set; }
    }
}