# Some Explanations about the implementation

## Project structure
### Introduction
- The software is split into 4 parts
	- Domain
	- Application
	- Infrastructure
	- WebAPI
	- OAuth
	
#### Domain
The Domain project contains all domain level considerations, entities, enums, exceptions*, domain constants like roles. It does not depend on any other project

#### Application
The Application project depends on the Domain project. Contracts are defined in this layer with no implementation whatsoever

#### Infrastructure
The Infrastructure layer contains all the business logic and implementations of the contracts defined in the Application layer, it also serves as the data access layer and *YEP!!!* you guessed it... it depends on the Application project

#### WebAPI
The WebApi project is the presentation layer containing endpoints and authorization logic

#### OAuth
The OAuth project is where the IdentityServer is located

### Some extra information
I decided to use the Mediator pattern for separation of concerns at the feature level. The features i concluded for this project were Book, Customer, Reservation. Check Out and Check In are part of the Book feature.
The package used is MediatR. I chose it because of the ease of use, clean controller potential and the notifications publisher built into the package.

## Features, Use Cases and Domain
### Customers
- A customer must have an identity similar to the function of a library card
- A customer is a user with the role `Customer`
- A customer can read all active books
- A customer can reserve active books with no restriction to how many book can be reserved
- A customer can check out an indefinite number of active books

### Admin
- From the scope we are expecting only one admin user (Martha) so endpoints to manage non-customer users were not added
- There is no delete options for books in this API though there is a flag for the book status which can be set to `Deleted`
- The admin can modify customer details especially for support scenarios
- The admin can create customers to aid business development. Meaning that a customer can call and then an account can be created for the customer during the call

### Book
- Access to all book endpoints requires a `read:books` permission claim while create and update operations required a `write:books` permission claim

### Reservation
- Creating and getting a reservations required the `read:books` permission. The assumption is that anyone who can view a book should be able to reserve active ones

### Check In & Check Out
- The Check In and Check Out endpoints also require `read:books` permission

## Running the software
1. Execute the SQL script named Database.sql. It will create a database called `Lenkie`
2. Run both the `WebApi` and `OAuth` projects
3. An admin user has already been seeded

### Athorization
To authorize a request make a post request to the oauth application url at `https://localhost:7169/connect/token`. Send the parameters
	
```json
{
	"client_id": "{clientID}"
	"client_secret": "{clientSECRET}"
	"grant_type": "password"
	"username": "{username}"
	"password": "{password}"
}
```


and you will get a success resopnse like:


```json
{
	"access_token": "",
	"expires_in": "",
	"token_type": "",
	"scope": ""
}
```

An example postman export has been added.