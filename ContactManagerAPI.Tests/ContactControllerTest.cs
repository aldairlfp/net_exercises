using ContactManagerAPI.Data;
using ContactManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ContactManagerAPI.Tests;

public class ContactControllerTest
{
    private static ContactController GetController(List<Contact> data)
    {
        var mockSet = new Mock<DbSet<Contact>>();
        mockSet.As<IAsyncEnumerable<Contact>>()
               .Setup(m => m.GetAsyncEnumerator(default))
               .Returns(new TestDbAsyncEnumerator<Contact>(data.GetEnumerator()));

        mockSet.As<IQueryable<Contact>>()
               .Setup(m => m.Provider)
               .Returns(new TestDbAsyncQueryProvider<Contact>(data.AsQueryable().Provider));

        mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).Returns((object[] r) =>
        {
            return new ValueTask<Contact>(data.FirstOrDefault(b => b.Id == (Guid)r[0]));
        });

        mockSet.Setup(m => m.AddAsync(It.IsAny<Contact>(), default)).Callback<Contact, CancellationToken>((s, token) =>
        {
            data.Add(s);
        });

        mockSet.Setup(m => m.Remove(It.IsAny<Contact>())).Callback<Contact>(s =>
        {
            data.Remove(data.Find(t => t.Id == s.Id));
        });

        mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
        mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
        mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

        var mockContext = new Mock<ContactAPIDbContext>();
        mockContext.Setup(c => c.Contacts).Returns(mockSet.Object);

        var controller = new ContactController(mockContext.Object);
        return controller;
    }

    [Fact]
    public async void GetAllContacts()
    {
        var data = new List<Contact>()
        {
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                LastName = "Test",
                Email = "Test@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-18),
                Phone = "Testphone",
                Owner = Guid.NewGuid()
            },
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test2",
                LastName = "Test2",
                Email = "test2@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-18),
                Phone = "Testphone2",
                Owner = Guid.NewGuid()
            }
        };

        var controller = GetController(data);
        var actionResult = await controller.GetContacts();

        var result = actionResult as OkObjectResult;

        var returnContacts = result.Value as IEnumerable<ContactDTO>;

        Assert.Equal(2, returnContacts.Count());
    }

    [Fact]
    public async void GetContactById()
    {
        var data = new List<Contact>()
        {
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                LastName = "Test",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-18),
                Phone = "Testphone",
                Owner = Guid.NewGuid()
},
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test2",
                LastName = "Test2",
                Email = "test2@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-18),
                Phone = "Testphone2",
                Owner = Guid.NewGuid()
                            }
        };

        var controller = GetController(data);
        var actionResult = await controller.GetContact(data[0].Id);
        Assert.NotNull(actionResult);

        var result = actionResult as OkObjectResult;
        Assert.NotNull(result);

        var returnContact = result.Value as ContactDTO;
        Assert.NotNull(returnContact);
        Assert.Equal(data[0].Id, returnContact.Id);

        actionResult = await controller.GetContact(Guid.NewGuid());
        Assert.NotNull(actionResult);
    }

    [Fact]
    public async void AddContact()
    {
        var data = new List<Contact>()
        {
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                LastName = "Test",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Phone = "Testphone",
                Owner = Guid.NewGuid()
            },
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test2",
                LastName = "Test2",
                Email = "test2@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Phone = "Testphone2",
                Owner = Guid.NewGuid()
            }
        };

        var controller = GetController(data);

        var newContact = new ContactDTO
        {
            Firstname = "Test3",
            LastName = "Test3",
            Email = "email3@gmail.com",
            DateOfBirth = DateTime.Now.AddYears(-19),
            Phone = "Testphone3",
            Owner = Guid.NewGuid()
        };

        var actionResult = await controller.CreateContact(newContact);
        Assert.NotNull(actionResult);

        var result = actionResult as CreatedAtActionResult;
        Assert.NotNull(result);

        var returnContact = result.Value as Contact;
        Assert.NotNull(returnContact);

        Assert.Equal(newContact.Firstname, returnContact.Firstname);
    }

    [Fact]
    public async void UpdateContact()
    {
        var data = new List<Contact>()
        {
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                LastName = "Test",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Phone = "Testphone",
                Owner = Guid.NewGuid()
            },
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test2",
                LastName = "Test2",
                Email = "test2@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Phone = "Testphone2",
                Owner = Guid.NewGuid()
            }
        };

        ContactController controller = GetController(data);

        var newContact = new ContactDTO
        {
            Firstname = "Test3",
            LastName = "Test3",
            Email = "email3@gmail.com",
            DateOfBirth = DateTime.Now.AddYears(-19),
            Phone = "Testphone3",
            Owner = Guid.NewGuid()
        };

        var actionResult = await controller.UpdateContact(data[0].Id, newContact);
        Assert.NotNull(actionResult);

        var result = actionResult as NoContentResult;
        Assert.NotNull(result);
    }    

    [Fact]
    public async void DeleteContact()
    {
        var data = new List<Contact>()
        {
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                LastName = "Test",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Phone = "Testphone",
                Owner = Guid.NewGuid()
            },
            new Contact
            {
                Id = Guid.NewGuid(),
                Firstname = "Test2",
                LastName = "Test2",
                Email = "test2@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Phone = "Testphone2",
                Owner = Guid.NewGuid()
            }
        };

        var controller = GetController(data);

        var actionResult = await controller.DeleteContact(data[0].Id);
        Assert.NotNull(actionResult);

        var result = actionResult as NoContentResult;
        Assert.NotNull(result);
    }
}