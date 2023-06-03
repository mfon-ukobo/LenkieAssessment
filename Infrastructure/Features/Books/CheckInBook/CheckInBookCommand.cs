using Application.Mediator;
using Infrastructure.Notifications.BookIsAvailable;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Books.CheckInBook
{
    public class CheckInBookCommand : ICommand<Result>
    {
        public long BookId { get; set; }
    }

    internal sealed class CheckInBookCommandHandler : ICommandHandler<CheckInBookCommand, Result>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CheckInBookCommandHandler(UnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result> Handle(CheckInBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetFirstAsync(book => book.Id == request.BookId);
            book.MakeAvailable();

            var checkOut = await _unitOfWork.CheckOut
                .Where(x => x.CheckInDate == null && x.BookId == book.Id)
                .SingleAsync();

            if (checkOut is not null)
            {
                checkOut.CheckInDate = DateTime.UtcNow;
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new BookIsAvailableNotification(book)); // publish the event
                return Result.SUCCESS;
            }
            catch (Exception ex)
            {
                // log error
                throw;
            }
        }
    }
}
