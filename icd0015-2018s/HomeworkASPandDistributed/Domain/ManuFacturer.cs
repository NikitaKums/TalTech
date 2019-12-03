using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ManuFacturer : DomainEntity
    {        
        public int ManuFacturerNameId { get; set; }
        public int ManuFacturerAadressId { get; set; }
        public int ManuFacturerPhoneNumberId { get; set; }

        [ForeignKey(nameof(ManuFacturerNameId))]
        public MultiLangString ManuFacturerName { get; set; }
        
        [ForeignKey(nameof(ManuFacturerAadressId))]
        public MultiLangString Aadress { get; set; }
        
        [ForeignKey(nameof(ManuFacturerPhoneNumberId))]
        public MultiLangString PhoneNumber { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}