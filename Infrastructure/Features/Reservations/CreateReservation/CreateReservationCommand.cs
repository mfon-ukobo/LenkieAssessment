using Application.Mediator;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Features.Reservations.CreateReservation
{
    public class CreateReservationCommand : ICommand<Result<Reservation>>
    {
        public CreateReservationCommand(string userName, CreateReservationRequest payload)
        {
            UserName = userName;
            Payload = payload;
        }

        public string UserName { get; set; }
        public CreateReservationRequest Payload { get; set; }
    }

    internal sealed class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand, Result<Reservation>>
    {
        private readonly UnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Reservation>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetFirstAsync(x => x.Id == request.Payload.BookId);
            if (book.Status == Domain.Enums.BookStatus.CheckedOut)
            {
                return new Error("This book has been checked out. You can request to be notified when this book becomes available");
            }

            var reservation = await CheckExistingReservation(Guid.NewGuid(), request.Payload.BookId);
            if (reservation is not null)
            {
                return new Error("A reservation already exists for this book. You can request to be notified when this book becomes available");
            }

            reservation = new Reservation
            {
                BookId = request.Payload.BookId,
            };

            _unitOfWork.Reservation.Add(reservation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reservation;
        }

        private async Task<Reservation> CheckExistingReservation(Guid customerId, long bookId)
        {
            return await _unitOfWork.Reservation
                .GetFirstAsync(x => x.CustomerId == customerId 
                    && x.BookId == bookId);
        }
    }
}
