using System.ComponentModel.DataAnnotations;

public class BooleanValueAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            string stringValue = value.ToString();

            if (bool.TryParse(stringValue, out bool result))
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "The value must be a valid boolean.");
            }
        }

        return ValidationResult.Success!;
    }
}
