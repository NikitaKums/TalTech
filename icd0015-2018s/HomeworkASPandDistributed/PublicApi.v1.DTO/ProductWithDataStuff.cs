using System.Collections.Generic;
using PublicApi.v1.DTO.IdAndNameDTO;

namespace PublicApi.v1.DTO
{
    public class ProductWithDataStuff : Product
    {
        public List<CategoryIdName> CategoryDTOs { get; set; } // multiple
        public List<CommentIdTitleBody> CommentDTOs { get; set; } // multiple
        
        public int ProductsInOrdersCount { get; set; }
        public int ProductsSoldCount { get; set; }
        public int ProductReturnsCount { get; set; }
        public int ProductsWithDefectCount { get; set; }
    }
}