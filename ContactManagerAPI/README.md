# Contact Manager API

This is a simple API for managing contacts. It is built using Asp.Net Core and Entity Framework Core. It is a simple API that allows you to create, read, update and delete contacts. It also allows you to search for contacts by id. The endpoints are as follows:

- GET /api/contacts
- GET /api/contacts/{id}
- POST /api/contacts
- PUT /api/contacts/{id}
- DELETE /api/contacts/{id}

The body of the request for POST and PUT should be in JSON format and should contain the following fields:

```json
{
    "firstname": "string",
    "lastname": "string",
    "email": "string",
    "dateofBirth": "string",
    "phone": "string",
    "owner": "string"
}
```

The date of birth should be in the format "yyyy-MM-dd". The lasname is optional. For delete contact, the user must be administrator and must be cuban. It use JWT bearer token as authorization, so you must include the token in the header of the request.
