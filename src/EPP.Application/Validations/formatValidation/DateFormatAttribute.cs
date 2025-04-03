using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class DateFormatAttribute : ValidationAttribute
{
    public DateFormatAttribute()
    {
        ErrorMessage = "The date format should be MM/YYYY.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string dateStr = value.ToString();
            var regex = new Regex(@"^(0[1-9]|1[0-2])/\d{4}$");

            if (!regex.IsMatch(dateStr))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
