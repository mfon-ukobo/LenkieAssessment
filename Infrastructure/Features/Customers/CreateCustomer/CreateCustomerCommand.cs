using Application.Mediator;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Customers.CreateCustomer
{
    public class CreateCustomerCommand : ICommand<Result<User>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Password { get; set; }
    }

    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result<User>>
    {
        private readonly UserManager<User> _userManager;

        public CreateCustomerCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<User>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            var createUserResult = string.IsNullOrWhiteSpace(request.Password) 
                ? await _userManager.CreateAsync(user)
                : await _userManager.CreateAsync(user, request.Password);

            if (!createUserResult.Succeeded)
            {
                return new Error(createUserResult.Errors.Select(x => x.Description));
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, Roles.Customer);
            if (!addToRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new Error(addToRoleResult.Errors.Select(x => x.Description));
            }

            return user;
        }
    }
}
