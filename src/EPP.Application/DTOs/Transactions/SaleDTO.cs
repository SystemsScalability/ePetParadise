using System.Text.Json.Serialization;
using api.Models;

namespace api.DTOs.Entities
{
    public class SaleDTO
    {
        [JsonIgnore]
        public int SaleID { get; set; }

        [Required("ZipCode")]
        [StringValue("ZipCode")]
        [MaxLengthCharacters("ZipCode", 5)]
        [OnlyNumbers]
        public string ZipCode { get; set; }= string.Empty;

        [Required("Cvv")]
        [IntegerValue("Cvv")]
        [PositiveNumber("Cvv")]
        [RangeValueInt(100, 999)]
        public int Cvv { get; set; }

        [Required("CardNumber")]
        [StringValue("CardNumber")]
        [MaxLengthCharacters("CardNumber", 19)]
        [CreditCardFormat]
        public string CardNumber { get; set; }= string.Empty;

        [Required("Date")]
        [StringValue("Date")]
        [DateFormat]
        [MaxLengthCharacters("Date", 7)]
        public string Date { get; set; }= string.Empty;

        [Required("FinalPrice")]
        [DecimalValue(ErrorMessage = "The 'FinalPrice' property must be a decimal number.")]
        [DecimalPrecision(2, ErrorMessage = "The 'FinalPrice' property must have up to 2 decimal places.")]
        [RangeValueDecimal(0.01, 999999.99)]
        public decimal FinalPrice { get; set; }  

        [Required("UserID")]
        [IntegerValue("UserID")]
        [PositiveNumber("UserID")]
        public int UserID { get; set; } 

        [Required("IsAvailable")]
        [BooleanValue]
        public string IsAvailable { get; set; }= string.Empty; 
    }
}
