namespace api.Models
{
    public class Customer
    { 
        public int CustomerID { get; set; }
        public string Email { get; set; }= string.Empty;
        public string Name { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public int RegionID { get; set; }
        public int Nit { get; set; }
        public string IsAvailable { get; set; }= string.Empty;
    }
}
