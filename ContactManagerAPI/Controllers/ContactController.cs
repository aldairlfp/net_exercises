using ContactManagerAPI.Data;
using ContactManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerAPI;

[ApiController]
[Route("api/contacts")]
public class Contact : Controller
{
    private readonly ContactAPIDbContext _context;


    public Contact(ContactAPIDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await _context.Contacts.Include(c => c.User).ToListAsync();

        return Ok(contacts.Select(c => new
        {
            c.Id,
            c.Firstname,
            c.LastName,
            c.Email,
            c.DateOfBirth,
            c.Phone,
            c.User,
            Age = c.DateOfBirth > DateTime.Today.AddYears(
                c.DateOfBirth.Value.Year - DateTime.Today.Year
                    ) ? DateTime.Today.Year - c.DateOfBirth.Value.Year - 1 :
                        DateTime.Today.Year - c.DateOfBirth.Value.Year
        }));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetContact([FromRoute] Guid id)
    {
        var contacts = await _context.Contacts.Include(c => c.User).ToListAsync();

        try
        {
            var contact = contacts.Single(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                contact.Id,
                contact.Firstname,
                contact.LastName,
                contact.Email,
                contact.DateOfBirth,
                contact.Phone,
                contact.User,
                Age = contact.DateOfBirth > DateTime.Today.AddYears(
                    contact.DateOfBirth.Value.Year - DateTime.Today.Year
                        ) ? DateTime.Today.Year - contact.DateOfBirth.Value.Year - 1 :
                            DateTime.Today.Year - contact.DateOfBirth.Value.Year
            });
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] ContactRequest contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newContact = new Models.Contact
        {
            Firstname = contact.Firstname,
            LastName = contact.LastName,
            Email = contact.Email,
            DateOfBirth = contact.DateOfBirth,
            Phone = contact.Phone,
            User = await _context.Users.FindAsync(contact.User)
        };

        if (newContact.User == null)
        {
            return BadRequest(contact.User);
        }

        _context.Contacts.Add(newContact);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContact), new { id = newContact.Id }, newContact);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody] ContactRequest contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingContact = await _context.Contacts.FindAsync(id);

        if (existingContact == null)
        {
            return NotFound();
        }

        existingContact.Firstname = contact.Firstname;
        existingContact.LastName = contact.LastName;
        existingContact.Email = contact.Email;
        existingContact.DateOfBirth = contact.DateOfBirth;
        existingContact.Phone = contact.Phone;
        existingContact.User = await _context.Users.FindAsync(contact.User);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
