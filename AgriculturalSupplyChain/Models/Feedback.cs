namespace AgriculturalSupplyChain.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int ConsID { get; set; }
        public int BatchID { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
    }
}
