namespace AgriculturalSupplyChain.Models
{
    public class Shipment
    {
        public int ShipmentID { get; set; }
        public int SupID { get; set; }
        public int BatchID { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Status { get; set; }
        public string GPSLocation { get; set; }
    }
}
