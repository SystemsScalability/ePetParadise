using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class CustomEmailFormatAttribute : ValidationAttribute
{
    public CustomEmailFormatAttribute()
    {
        ErrorMessage = "The email format is incorrect. It should be in the format user@gmail.com.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string email = value.ToString();
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@gmail\.com$");
            if (!regex.IsMatch(email))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
