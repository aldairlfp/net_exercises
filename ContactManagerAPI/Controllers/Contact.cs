using ContactManagerAPI.Data;
using ContactManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerAPI;

[ApiController]
[Route("api/[controller]")]
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
        var contacts = await _context.Contacts.ToListAsync();
        return Ok(contacts.Select(c => new
        {
            c.Id,
            c.Firstname,
            c.LastName,
            c.Email,
            c.DateOfBirth,
            c.Phone,
            c.User,
            Age = DateTime.Now.Year - c.DateOfBirth.Value.Year
        }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetContact([FromRoute] Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);

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
            Age = DateTime.Now.Year - contact.DateOfBirth.Value.Year
        });
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

        _context.Contacts.Add(newContact);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContact), new { id = newContact.Id }, newContact);
    }

    [HttpPut("{id:guid}")]
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

    [HttpDelete("{id:guid}")]
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
