using System.Text.Json.Serialization;
using api.Models;

namespace api.DTOs
{
    public class PurchaseDTO
    {
        [JsonIgnore]
        public int PurchaseID { get; set; }

        [Required("TotalPrice")]
        [DecimalPrecision(2, ErrorMessage = "The 'TotalPrice' property must have up to 2 decimal places.")]
        [DecimalValue(ErrorMessage = "The 'TotalPrice' property must be a decimal number.")]
        [RangeValueDecimal(0.01, 500000)]
        public decimal TotalPrice { get; set; }
        
        [Required("ObtainedTaxes")]
        [DecimalPrecision(2, ErrorMessage = "The 'ObtainedTaxes' property must have up to 2 decimal places.")]
        [DecimalValue(ErrorMessage = "The 'ObtainedTaxes' property must be a decimal number.")]
        [RangeValueDecimal(0.01, 100)]
        public decimal ObtainedTaxes { get; set; }

        [Required("DeliveryTime")]
        [DecimalPrecision(2, ErrorMessage = "The 'DeliveryTime' property must have up to 2 decimal places.")]
        [DecimalValue(ErrorMessage = "The 'DeliveryTime' property must be a decimal number.")]
        [RangeValueDecimal(0.01, 20)]
        public decimal DeliveryTime { get; set; }

        [Required("LocalQuantity")]
        [IntegerValue("LocalQuantity")]
        [PositiveNumber("LocalQuantity")]
        public int LocalQuantity { get; set; }

        [Required("ProductID")]
        [IntegerValue("ProductID")]
        [PositiveNumber("ProductID")]
        public int ProductID { get; set; }

        [Required("UserID")]
        [IntegerValue("UserID")]
        [PositiveNumber("UserID")]
        public int UserID { get; set; }

        [Required("IsAvailable")]
        [BooleanValue]
        public bool IsAvailable { get; set; }
    }
}
