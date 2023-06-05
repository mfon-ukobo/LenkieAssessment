using Application.Mediator;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private const int RESERVATION_DURATION_DAYS = 2;

        public CreateReservationCommandHandler(UnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result<Reservation>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var book = await _unitOfWork.Book.GetFirstAsync(x => x.Id == request.Payload.BookId);
            if (book.Status == BookStatus.CheckedOut)
            {
                return new Error("This book has been checked out. You can request to be notified when this book becomes available");
            }

            if (book.Status == BookStatus.Reserved)
            {
                return new Error("A reservation already exists for this book. You can request to be notified when this book becomes available");
            }

            var reservation = new Reservation
            {
                BookId = request.Payload.BookId,
                CustomerId = user.Id,
                ReservationDate = DateTime.UtcNow,
                ReservationEndDate = DateTime.UtcNow.AddDays(RESERVATION_DURATION_DAYS)
            };
            _unitOfWork.Reservation.Add(reservation);

            book.Reserve(); // change the book status

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reservation;
        }
    }
}
