using Microsoft.EntityFrameworkCore;

namespace ContactManagerAPI.Data;

public class ContactAPIDbContext : DbContext
{
    public ContactAPIDbContext()
    {
        
    }

    public ContactAPIDbContext(DbContextOptions options): base(options)
    {
        
    }

    public virtual DbSet<Models.Contact>? Contacts {get; set;}
    public DbSet<Models.User>? Users {get; set;}
}
