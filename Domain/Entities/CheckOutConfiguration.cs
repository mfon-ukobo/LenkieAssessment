using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class CheckOutConfiguration : IEntityTypeConfiguration<CheckOut>
    {
        public void Configure(EntityTypeBuilder<CheckOut> builder)
        {
            builder.HasIndex(x => x.BookId)
                .IsUnique()
                .HasFilter($"[{nameof(CheckOut.CheckInDate)}] IS NULL");

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId);

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
