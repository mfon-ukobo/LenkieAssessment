using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class NotificationRequestConfiguration : IEntityTypeConfiguration<NotificationRequest>
    {
        public void Configure(EntityTypeBuilder<NotificationRequest> builder)
        {
            builder.HasIndex(x => new { x.BookId, x.CustomerId, x.NotificationEventType })
                .IsUnique();

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId);

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
