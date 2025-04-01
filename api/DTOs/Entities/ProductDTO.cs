using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using api.Models;

namespace api.DTOs
{
    public class ProductDTO
    {
        [JsonIgnore]
        public int ProductID { get; set; }

        [Required("Name")]
        [StringValue("Name")]
        [NoSpecialCharacters("Name")]
        [NoNumbers("Name")]
        [MaxLengthCharacters("Name", 40)]
        public string Name { get; set; }= string.Empty;

        [Required("Price")]
        [DecimalPrecision(2, ErrorMessage = "The 'Price' property must have up to 2 decimal places.")]
        [DecimalValue(ErrorMessage = "The 'Price' property must be a decimal number.")]
        [RangeValueDecimal(0.0001, 500000)]
        public decimal Price { get; set; }

        [Required("Quantity")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "El campo debe ser un número entero.")]
        [IntegerValue("Quantity")]
        [RangeValueInt(1, 250000)]
        public int Quantity { get; set; }

        [Required("Discount")]
        [IntegerValue("Discount")]
        [RangeValueInt(0, 60)]
        public int Discount { get; set; }

        [Required("AnimalCategory")]
        [StringValue("AnimalCategory")]
        [NoSpecialCharacters("AnimalCategory")]
        [NoNumbers("AnimalCategory")]
        [MaxLengthCharacters("AnimalCategory", 40)]
        public string AnimalCategory { get; set; }= string.Empty;

        [Required("Image")]
        [StringValue("Image")]
        [HttpsUrl(ErrorMessage = "The 'Image' URL must start with 'https:'.")]
        [MaxLengthCharacters("Image", 300)]
        public string Image { get; set; }= string.Empty;

        [Required("Description")]
        [StringValue("Description")]
        [NoSpecialCharacters("Description")]
        [NoNumbers("Description")]
        [MaxLengthCharacters("Description", 1000)]
        public string Description { get; set; }= string.Empty;

        [Required("ProductType")]
        [StringValue("ProductType")]
        [NoSpecialCharacters("ProductType")]
        [NoNumbers("ProductType")]
        [MaxLengthCharacters("ProductType", 40)]
        public string ProductType { get; set; }= string.Empty;

        [Required("BrandID")]
        [IntegerValue("BrandID")]
        [PositiveNumber("BrandID")]
        public int BrandID { get; set; }

        [Required("ProviderID")]
        [IntegerValue("ProviderID")]
        [PositiveNumber("ProviderID")]
        public int ProviderID { get; set; }

        [Required("IsAvailable")]
        [BooleanValue]
        public bool IsAvailable { get; set; }

        [Required("HasTax")]
        [BooleanValue]
        public bool HasTax { get; set; }
    }
}
