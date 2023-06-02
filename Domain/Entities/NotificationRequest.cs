using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("NotificationRequests")]
    public class NotificationRequest : BaseEntity<long>
    {
        public NotificationEventType NotificationEventType { get; set; }
        public long BookId { get; set; }
        public Guid CustomerId { get; set; }

        public User Customer { get; set; }
    }
}
