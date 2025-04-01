using System.ComponentModel.DataAnnotations;
using api.Utilities;

public class RangeValueInt : ValidationAttribute
{
    private readonly int _range1;
    private readonly int _range2;

    public RangeValueInt(int range1, int range2)
    {
        _range1 = range1;
        _range2 = range2;
        ErrorMessage = ErrorUtilities.RangeValueErrorMessageInt(range1, range2);
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null && value is int intValue)
        {
            if (intValue < _range1 || intValue > _range2)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success!;
    }
}
