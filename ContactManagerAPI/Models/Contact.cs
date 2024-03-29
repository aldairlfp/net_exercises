using System.ComponentModel.DataAnnotations;
using ContactManagerAPI.Validators;

namespace ContactManagerAPI.Models;

public class Contact
{
    public Guid Id {get; set;}

    [Required]
    [StringLength(128)]
    public string? Firstname {get; set;}

    [StringLength(128)]
    public string? Lastname {get; set;}

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
