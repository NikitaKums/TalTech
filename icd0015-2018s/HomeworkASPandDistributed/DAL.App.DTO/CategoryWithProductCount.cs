using DAL.App.DTO.IdAndNameDTO;

namespace DAL.App.DTO
{
    public class CategoryWithProductCount : CategoryIdName
    {           
        public int CategoryProductCount { get; set; }
    }
}