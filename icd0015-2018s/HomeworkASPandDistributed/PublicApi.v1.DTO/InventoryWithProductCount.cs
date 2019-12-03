using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class InventoryWithProductCount : Inventory
    {
        public string ShopName { get; set; }
        public int ProductCount { get; set; }
    }
}