using Application.Mediator;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Books.CheckOutBook
{
    public class CheckOutBookCommand : ICommand<Result>
    {
        public CheckOutBookCommand(CheckOutBookRequest payload)
        {
            Payload = payload;
        }

        public CheckOutBookRequest Payload { get; set; }
    }

    internal sealed class CheckOutBookCommandHandler : ICommandHandler<CheckOutBookCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly UnitOfWork _unitOfWork;

        public CheckOutBookCommandHandler(UserManager<User> userManager, UnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CheckOutBookCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Payload.CustomerId.ToString());
            var book = await _unitOfWork.Book.GetFirstAsync(book => book.Id == request.Payload.BookId);

            if (book.Status == BookStatus.CheckedOut)
            {
                return new Error("This book has been checked out. You can request to be notified when this book becomes available");
            }

            if (book.Status == BookStatus.Reserved)
            {
                return new Error("A reservation already exists for this book. You can request to be notified when this book becomes available");
            }

            book.Checkout();

            var checkout = new CheckOut
            {
                BookId = book.Id,
                CheckOutDate = DateTime.Now,
                ExpectedCheckInDate = request.Payload.CheckInDate,
                CustomerId = user.Id
            };

            _unitOfWork.CheckOut.Add(checkout);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.SUCCESS;
        }
    }
}
