using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordFormatAttribute : ValidationAttribute
{
    public PasswordFormatAttribute()
    {
        ErrorMessage = "The format should be an uppercase letter, a number, and six lowercase letters (e.g., A1bcdefg).";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            var regex = new Regex(@"^[A-Z]\d[a-z]{6}$");

            if (!regex.IsMatch(input))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
