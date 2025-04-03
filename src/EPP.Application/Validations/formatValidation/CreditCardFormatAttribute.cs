using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class CreditCardFormatAttribute : ValidationAttribute
{
    public CreditCardFormatAttribute()
    {
        ErrorMessage = "The credit card format should be ####-####-####-####.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string creditCardNumber = value.ToString();
            var regex = new Regex(@"^\d{4}-\d{4}-\d{4}-\d{4}$");

            if (!regex.IsMatch(creditCardNumber))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
