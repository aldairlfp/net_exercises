using System.ComponentModel.DataAnnotations;
using ContactManagerAPI.Validators;

namespace ContactManagerAPI.Models;

public class ContactDTO
{
    public Guid Id { get; set;}

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

    public int Age { get; set;}

    [Required]
    [StringLength(20)]
    public string? Phone {get; set;}

    [Required]
    public virtual Guid Owner {get; set;}
}
