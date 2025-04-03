using System.ComponentModel.DataAnnotations;
using api.Utilities;

public class IntegerValue : ValidationAttribute
{
    public IntegerValue(string value)
    {
        ErrorMessage = ErrorUtilities.ValidateString(value);
    }
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            if (int.TryParse(value.ToString(), out _))
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success!;
    }
}
