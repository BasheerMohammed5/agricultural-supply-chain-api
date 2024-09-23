namespace AgriculturalSupplyChain.Models
{
    public class Batch
    {
        public int BatchID { get; set; }
        public int ProdID { get; set; }
        public int FarmerID { get; set; }
        public DateTime HarvestDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string QualityCertification { get; set; }
        public string Location { get; set; }
    }
}
