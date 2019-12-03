using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Shipper : DomainEntity
    {
        public int ShipperNameId { get; set; }
        public int ShipperAddressId { get; set; }
        public int ShipperPhoneNumberId { get; set; }
        
        [ForeignKey(nameof(ShipperNameId))]
        public MultiLangString ShipperName { get; set; }
        
        [ForeignKey(nameof(ShipperAddressId))]
        public MultiLangString ShipperAddress { get; set; }
        
        [ForeignKey(nameof(ShipperPhoneNumberId))]
        public MultiLangString PhoneNumber { get; set; }
        
        public ICollection<Order> Orders { get; set; }
    }
}