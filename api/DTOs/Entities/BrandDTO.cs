using System.Text.Json.Serialization;

namespace api.DTOs
{
    public class BrandDTO
    {
        [JsonIgnore]
        public int BrandID { get; set; }

        [Required("Name")]
        [StringValue("Name")]
        [NoSpecialCharacters("Name")]
        [NoNumbers("Name")]
        [MaxLengthCharacters("Name", 40)]
        public string Name { get; set; }= string.Empty;

        [Required("Logo")]
        [StringValue("Logo")]
        [HttpsUrl(ErrorMessage = "The 'Logo' URL must start with 'https://'.")]
        [MaxLengthCharacters("Logo", 300)]
        public string Logo { get; set; }= string.Empty;

        [Required("IsAvailable")]
        [BooleanValue]
        public bool IsAvailable { get; set; }
    }
}
