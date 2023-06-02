using Application.Mediator;
using Domain.Entities;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Reservations.GetReservations
{
    public class GetReservationsQuery : QueryFilter, IQuery<PagedList<Reservation>>
    {
    }

    internal sealed class GetReservationsQueryHandler : IQueryHandler<GetReservationsQuery, PagedList<Reservation>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetReservationsQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Reservation>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = _unitOfWork.Reservation.GetAll();
            return await reservations.ToPagedListAsync(request);
        }
    }
}
