namespace api.Models
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReportDate { get; set; }= string.Empty;
        public decimal ObtainedTaxes { get; set; }
        public decimal ApplicationTax { get; set; }
        public decimal DeliveryTime { get; set; }
        public int LocalQuantity { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string IsAvailable { get; set; }= string.Empty;
    }
}
