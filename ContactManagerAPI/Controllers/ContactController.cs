using ContactManagerAPI.Data;
using ContactManagerAPI.Identity;
using ContactManagerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerAPI;

[ApiController]
[Route("api/contacts")]
[Authorize]
[UsernameClaim]
public class ContactController : Controller
{
    private readonly ContactAPIDbContext _context;


    public ContactController(ContactAPIDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await _context.Contacts.ToListAsync();

        return base.Ok(contacts.Select(contact => new ContactDTO
        {
            Id = contact.Id,
            Firstname = contact.Firstname,
            LastName = contact.LastName,
            Email = contact.Email,
            DateOfBirth = contact.DateOfBirth,
            Phone = contact.Phone,
            Owner = contact.Owner,
            Age = GetAgeFromDate(contact)
        }));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetContact([FromRoute] Guid id)
    {
        var contacts = await _context.Contacts.ToListAsync();

        try
        {
            var contact = contacts.Single(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new ContactDTO
            {
                Id = contact.Id,
                Firstname = contact.Firstname,
                LastName = contact.LastName,
                Email = contact.Email,
                DateOfBirth = contact.DateOfBirth,
                Phone = contact.Phone,
                Owner = contact.Owner,
                Age = GetAgeFromDate(contact)
            });
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] ContactDTO contactDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newContact = new Models.Contact
        {
            Firstname = contactDTO.Firstname,
            LastName = contactDTO.LastName,
            Email = contactDTO.Email,
            DateOfBirth = contactDTO.DateOfBirth,
            Phone = contactDTO.Phone,
            Owner = contactDTO.Owner
        };

        _context.Contacts.AddAsync(newContact);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContact), new { id = newContact.Id }, newContact);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody] ContactDTO contactDTO)
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

        existingContact.Firstname = contactDTO.Firstname;
        existingContact.LastName = contactDTO.LastName;
        existingContact.Email = contactDTO.Email;
        existingContact.DateOfBirth = contactDTO.DateOfBirth;
        existingContact.Phone = contactDTO.Phone;
        existingContact.Owner = contactDTO.Owner;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [CubanAdministrator]
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

    private static int GetAgeFromDate(Contact c)
    {
        return c.DateOfBirth > DateTime.Today.AddYears(
                        c.DateOfBirth.Value.Year - DateTime.Today.Year
                            ) ? DateTime.Today.Year - c.DateOfBirth.Value.Year - 1 :
                                DateTime.Today.Year - c.DateOfBirth.Value.Year;
    }
}
