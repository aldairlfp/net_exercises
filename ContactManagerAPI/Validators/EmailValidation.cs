using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
        if(IsValid((string)value))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("Invalid Email");
    }

    private static bool IsValid(string email)
        { 
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
   }
