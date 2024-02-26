using System.ComponentModel.DataAnnotations;

namespace ContactManagerAPI.Models;

public class User
{
    public Guid Id {get; set;}

    [Required]
    [StringLength(128)]
    public string? Firstname {get; set;}

    [Required]
    [StringLength(128)]
    public string? LastName {get; set;}

    [Required]
    [StringLength(60)]
    public string? Username {get; set;}

    [Required]
    [StringLength(256)]
    public string? Password {get; set;}
}
