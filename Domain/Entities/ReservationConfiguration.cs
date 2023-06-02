using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasIndex(x => new { x.BookId, x.CustomerId });

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId);

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.ReservationDate)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
