using System.ComponentModel.DataAnnotations;
using api.Utilities;

public class PositiveNumberAttribute : ValidationAttribute
{
    public PositiveNumberAttribute(string value)
    {
        ErrorMessage = ErrorUtilities.PositiveNumber(value);
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success; // Puedes decidir si un valor nulo es válido o no
        }

        if (value is IComparable comparableValue && comparableValue.CompareTo(0) < 1)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
