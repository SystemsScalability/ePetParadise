using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class OnlyNumbersAttribute : ValidationAttribute
{
    public OnlyNumbersAttribute()
    {
        ErrorMessage = "Only numbers are allowed in this field String.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            var regex = new Regex("^[0-9]*$");
            if (!regex.IsMatch(input))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
