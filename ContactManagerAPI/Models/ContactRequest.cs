using System.ComponentModel.DataAnnotations;
using ContactManagerAPI.Validators;

namespace ContactManagerAPI.Models;

public class ContactRequest
{
    [Required]
    [StringLength(128)]
    public string? Firstname {get; set;}

    [StringLength(128)]
    public string? LastName {get; set;}

    [Required]
    [StringLength(128)]
    [EmailValidation]
    public string? Email {get; set;}

    [Required]
    [DataType(DataType.Date)]
    [AgeValidator]
    public DateTime? DateOfBirth {get; set;}

    [Required]
    [StringLength(20)]
    public string? Phone {get; set;}

    [Required]
    public virtual Guid Owner {get; set;}
}
