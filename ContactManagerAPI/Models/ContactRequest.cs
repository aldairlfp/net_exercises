namespace ContactManagerAPI.Models;

public class ContactRequest
{
    public string? Firstname {get; set;}

    public string? LastName {get; set;}

    public string? Email {get; set;}

    public DateTime? DateOfBirth {get; set;}

    public string? Phone {get; set;}

    public virtual Guid User {get; set;}
}
