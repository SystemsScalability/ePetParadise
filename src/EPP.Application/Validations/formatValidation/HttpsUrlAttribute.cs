using System.ComponentModel.DataAnnotations;

public class HttpsUrlAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            string url = value.ToString();

            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "The URL must start with 'https:'.");
            }
        }
        return ValidationResult.Success!;
    }
}
