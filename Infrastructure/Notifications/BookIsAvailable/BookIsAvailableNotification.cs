using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Notifications.BookIsAvailable
{
    public class BookIsAvailableNotification : INotification
    {
        public BookIsAvailableNotification(Book book)
        {
            
        }

        public Book Book { get; set; }
    }

    internal sealed class BookIsAvailableNotificationHandler : INotificationHandler<BookIsAvailableNotification>
    {
        private readonly UnitOfWork _unitOfWork;
        private const string MESSAGE_TEMPLATE = "The book {title} is now available";

        public BookIsAvailableNotificationHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(BookIsAvailableNotification notification, CancellationToken cancellationToken)
        {
            var notificationRequests = _unitOfWork.NotificationRequest
                .Where(x => x.NotificationEventType == NotificationEventType.BookIsAvailable
                    && x.BookId == notification.Book.Id);

            var notifications = notificationRequests
                .Select(request => new Notification
                {
                    EventType = NotificationEventType.BookIsAvailable,
                    CustomerId = request.CustomerId,
                    Body = string.Format(MESSAGE_TEMPLATE, notification.Book.Title)
                });

            _unitOfWork.Notification.AddRange(notifications);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
