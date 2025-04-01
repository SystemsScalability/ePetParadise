namespace api.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        public string Name { get; set; }= string.Empty;
        public decimal MunicipalTax { get; set; }
        public decimal StatalTax { get; set; }
    }
}
