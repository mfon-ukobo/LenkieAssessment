using Application.Mediator;
using Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Customers.UpdateCustomer
{
    public class UpdateCustomerCommand : ICommand<Result>
    {
        public UpdateCustomerCommand(Guid customerId, UpdateCustomerRequest payload)
        {
            Payload = payload;
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
        public UpdateCustomerRequest Payload { get; set; }
    }

    public class UpdateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    internal sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, Result>
    {
        private readonly UserManager<User> _userManager;

        public UpdateCustomerCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.CustomerId.ToString());
            if (user is null)
            {
                return new Error($"No User found with the Id: {request.CustomerId}");
            }

            user.FirstName = request.Payload.FirstName;
            user.LastName = request.Payload.LastName;
            user.Email = request.Payload.Email;

            var updateuserResult = await _userManager.UpdateAsync(user);
            if (!updateuserResult.Succeeded)
            {
                return new Error(updateuserResult.Errors.Select(x => x.Description));
            }

            return Result.SUCCESS;
        }
    }
}
