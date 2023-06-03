using Application.Mediator;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Customers.GetCustomers
{
    public class GetCustomersQuery : QueryFilter, IQuery<PagedList<User>>
    {
    }

    public class GetCustomerQueryHandler : IQueryHandler<GetCustomersQuery, PagedList<User>>
    {
        private readonly UserManager<User> _userManager;

        public GetCustomerQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<User>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _userManager.GetUsersInRoleAsync(Roles.Customer);
            return customers.ToPagedList(request);
        }
    }
}
