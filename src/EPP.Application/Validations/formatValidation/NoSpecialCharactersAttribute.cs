using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using api.Utilities;

public class NoSpecialCharactersAttribute : ValidationAttribute
{
    public NoSpecialCharactersAttribute(string value)
    {
        ErrorMessage = ErrorUtilities.NoSpecialCharacters(value);
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string stringValue = value.ToString();
            var regex = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regex.IsMatch(stringValue))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
