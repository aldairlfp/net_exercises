using System.ComponentModel.DataAnnotations;
using ContactManagerAPI.Data;

namespace ContactManagerAPI.Validators;

public class EmailValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var context = (ContactAPIDbContext)validationContext.GetService(typeof(ContactAPIDbContext));
        if (context.Contacts.Any(c => c.Email == (string)value))
        {
            return new ValidationResult("Email already exists");

        }
        if(new EmailAddressAttribute().IsValid(value))
        {
            return new ValidationResult("Invalid Email");
        }
        return ValidationResult.Success;
    }
}
