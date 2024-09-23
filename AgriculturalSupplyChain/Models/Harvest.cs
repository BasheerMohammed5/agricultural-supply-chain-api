namespace AgriculturalSupplyChain.Models
{
    public class Harvest
    {
        public int HarvestID { get; set; }
        public int BatchID { get; set; }
        public DateTime HarvestDate { get; set; }
        public int Quantity { get; set; }
    }
}
