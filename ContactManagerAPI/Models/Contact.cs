using System.ComponentModel.DataAnnotations;

namespace ContactManagerAPI.Models;

public class Contact
{
    public Guid Id {get; set;}

    [Required]
    [StringLength(128)]
    public string? Firstname {get; set;}

    [StringLength(128)]
    public string? LastName {get; set;}

    [Required]
    [StringLength(128)]
    [EmailAddress]
    public string? Email {get; set;}

    [Required]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth {get; set;}

    [Required]
    [StringLength(20)]
    public string? Phone {get; set;}

    [Required]
    public virtual User? User {get; set;}
}
