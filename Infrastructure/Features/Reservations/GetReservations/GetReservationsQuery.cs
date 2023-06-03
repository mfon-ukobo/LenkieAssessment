using Application.Mediator;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Reservations.GetReservations
{
    public class GetReservationsQuery : IQuery<PagedList<Reservation>>
    {
        public GetReservationsQuery(GetReservationsRequest payload, string? userName = null)
        {
            Payload = payload;
            UserName = userName;
        }

        public string? UserName { get; set; }
        public GetReservationsRequest Payload { get; set; }
    }

    internal sealed class GetReservationsQueryHandler : IQueryHandler<GetReservationsQuery, PagedList<Reservation>>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public GetReservationsQueryHandler(UnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<PagedList<Reservation>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = _unitOfWork.Reservation.GetAll();

            if (request.UserName is not null)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                reservations = reservations.Where(x => x.CustomerId == user.Id);
            }

            return await reservations.ToPagedListAsync(request.Payload);
        }
    }
}
