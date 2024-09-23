namespace AgriculturalSupplyChain.Models
{
    public class Storage
    {
        public int StorageID { get; set; }
        public int BatchID { get; set; }
        public DateTime StorageDate { get; set; }
        public string StorageLocation { get; set; }
    }
}
