using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.DTOs
{
    public class CustomerDTO
    {
        [JsonIgnore]
        public int CustomerID { get; set; }

        [Required("Email")]
        [StringValue("Email")]
        [EmailAddress(ErrorMessage = "The email format is incorrect.")]
        [CustomEmailFormat]
        [MaxLengthCharacters("Email", 40)]
        public string Email { get; set; }= string.Empty;

        [Required("Name")]
        [StringValue("Name")]
        [NoSpecialCharacters("Name")]
        [NoNumbers("Name")]
        [MaxLengthCharacters("Name", 40)]
        public string Name { get; set; }= string.Empty;

        [Required("Password")]
        [StringValue("Password")]
        [MaxLengthCharacters("Password", 20)]
        [PasswordFormat]
        public string Password { get; set; }= string.Empty;

        [Required("RegionID")]
        [IntegerValue("RegionID")]
        [PositiveNumber("RegionID")]
        public int RegionID { get; set; }

        [Required("Nit")]
        [IntegerValue("Nit")]
        [PositiveNumber("Nit")]
        [RangeValueInt(6000000, 9999999)]
        public int Nit { get; set; }

        [Required("IsAvailable")]
        [BooleanValue]
        public bool IsAvailable { get; set; }
    }
}
