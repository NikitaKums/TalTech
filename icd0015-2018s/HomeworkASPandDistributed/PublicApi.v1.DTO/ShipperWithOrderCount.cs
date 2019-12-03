namespace PublicApi.v1.DTO
{
    public class ShipperWithOrderCount : Shipper
    {
        public int OrdersCount { get; set; }

    }
}