namespace AgriculturalSupplyChain.Models
{
    public class QualityTest
    {
        public int QualityTestID { get; set; }
        public int BatchID { get; set; }
        public DateTime TestDate { get; set; }
        public string TestResults { get; set; }
    }
}
