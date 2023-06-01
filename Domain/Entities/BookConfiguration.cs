using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasIndex(x => x.Title).IsUnique();
            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Status)
                .HasDefaultValue(BookStatus.Available);
        }
    }
}
