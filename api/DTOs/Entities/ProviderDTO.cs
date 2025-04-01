using System.Text.Json.Serialization;

namespace api.DTOs
{
    public class ProviderDTO
    { 
        [JsonIgnore]
        public int ProviderID { get; set; }

        [Required("Name")]
        [StringValue("Name")]
        [NoSpecialCharacters("Name")]
        [NoNumbers("Name")]
        [MaxLengthCharacters("Name", 200)]
        public string Name { get; set; }= string.Empty;

        [Required("Nationality")]
        [StringValue("Nationality")]
        [NoSpecialCharacters("Nationality")]
        [NoNumbers("Nationality")]
        [MaxLengthCharacters("Nationality", 200)]
        public string Nationality { get; set; }= string.Empty;

        [Required("IsAvailable")]
        [BooleanValue]
        public bool IsAvailable { get; set; }
    }
}
