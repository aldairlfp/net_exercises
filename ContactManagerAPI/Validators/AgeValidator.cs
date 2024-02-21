using System.ComponentModel.DataAnnotations;

namespace ContactManagerAPI.Validators;

public class AgeValidator: ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date.AddYears(18) > DateTime.Now)
            {
                return new ValidationResult("You must be 18 years or older to register");
            }
        }
        return ValidationResult.Success;
    }
}
