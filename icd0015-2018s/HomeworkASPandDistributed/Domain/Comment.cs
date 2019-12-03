using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Comment : DomainEntity
    {
        public int CommentTitleId { get; set; }
        public int CommentBodyId { get; set; }

        [ForeignKey(nameof(CommentTitleId))]
        public MultiLangString CommentTitle { get; set; }
        
        [ForeignKey(nameof(CommentBodyId))]
        public MultiLangString CommentBody { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}