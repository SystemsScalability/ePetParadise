namespace api.Models
{   
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Logo { get; set; }= string.Empty;
        public string IsAvailable { get; set; }= string.Empty;
    }
}
