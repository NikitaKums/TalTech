using BLL.App.DTO.IdAndNameDTO;

namespace BLL.App.DTO
{
    public class CategoryWithProductCount : CategoryIdName
    {           
        public int CategoryProductCount { get; set; }
    }
}