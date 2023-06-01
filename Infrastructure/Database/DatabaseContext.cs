using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    internal class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<NotificationRequest> NotificationRequests => Set<NotificationRequest>();
        public DbSet<CheckOut> CheckOuts => Set<CheckOut>();
    }
}
