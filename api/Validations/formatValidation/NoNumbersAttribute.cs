using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using api.Utilities;

public class NoNumbersAttribute : ValidationAttribute
{
    public NoNumbersAttribute(string value)
    {
        ErrorMessage = ErrorUtilities.NoSpecialNumbers(value);
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string stringValue = value.ToString();
            var regex = new Regex("^[a-zA-Z ]*$");

            if (!regex.IsMatch(stringValue))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
