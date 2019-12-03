using System.ComponentModel.DataAnnotations;
using PublicApi.v1.DTO.IdAndNameDTO;

namespace PublicApi.v1.DTO
{
    public class Comment : CommentIdTitleBody
    {
        public string ProductName { get; set; }
        
        public int ProductId { get; set; }
        public int ShopId { get; set; }
    }
}