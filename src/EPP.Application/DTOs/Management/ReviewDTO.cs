using System.Text.Json.Serialization;

namespace api.DTOs
{
    public class ReviewDTO
    {
        [JsonIgnore]
        public int ReviewID { get; set; }

        [Required("CustomerID")]
        [IntegerValue("CustomerID")]
        [PositiveNumber("CustomerID")]
        public int CustomerID { get; set; }

        [Required("ProductID")]
        [IntegerValue("ProductID")]
        [PositiveNumber("ProductID")]
        public int ProductID { get; set; }

        [Required("ReviewMessage")]
        [StringValue("ReviewMessage")]
        [NoSpecialCharacters("ReviewMessage")]
        [NoNumbers("ReviewMessage")]
        [MaxLengthCharacters("ReviewMessage", 150)]
        public string ReviewMessage { get; set; }= string.Empty;
    }
}
