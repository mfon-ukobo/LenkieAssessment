using Domain.Enums;

namespace Infrastructure.Features.Books.CreateBookNotification
{
    public class CreateBookNotificationRequest
    {
        public long BookId { get; set; }
        public NotificationEventType EventType { get; set; }
    }
}
