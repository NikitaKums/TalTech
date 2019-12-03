using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Translation  : DomainEntity
    {
        [MaxLength(5)]
        public string Culture { get; set; }
        
        [MaxLength(10240)]
        public string Value { get; set; }
        
        public int MultiLangStringId { get; set; }
        public MultiLangString MultiLangString { get; set; }
    }
}