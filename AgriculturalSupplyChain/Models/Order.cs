namespace AgriculturalSupplyChain.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int RetID { get; set; }
        public int BatchID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
