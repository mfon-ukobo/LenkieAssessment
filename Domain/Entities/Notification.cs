using Domain.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Notifications")]
    public class Notification : BaseEntity<long>
    {
        public Guid CustomerId { get; set; }
        public NotificationEventType EventType { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; internal set; }
    }

    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
