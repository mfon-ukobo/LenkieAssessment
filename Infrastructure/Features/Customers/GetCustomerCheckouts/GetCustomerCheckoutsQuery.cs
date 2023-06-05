using Application.Mediator;
using Domain.Entities;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Customers.GetCustomerCheckouts
{
    public class GetCustomerCheckoutsQuery : QueryFilter, IQuery<PagedList<CheckOut>>
    {
        public GetCustomerCheckoutsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }

    internal sealed class GetCustomerCheckoutsQueryHandler : IQueryHandler<GetCustomerCheckoutsQuery, PagedList<CheckOut>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetCustomerCheckoutsQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedList<CheckOut>> Handle(GetCustomerCheckoutsQuery request, CancellationToken cancellationToken)
        {
            var checkouts = _unitOfWork.CheckOut.Where(x => x.CustomerId == request.CustomerId);
            return checkouts.ToPagedListAsync(request);
        }
    }
}
