namespace api.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string ReviewMessage { get; set; }= string.Empty;
    }
}
