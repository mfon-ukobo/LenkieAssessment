using Application.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    internal class UnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;

            Book = new Repository<Book>(context);
            Reservation = new Repository<Reservation>(context);
            NotificationRequest = new Repository<NotificationRequest>(context);
            CheckOut = new Repository<CheckOut>(context);
            Notification = new Repository<Notification>(context);
            Author = new Repository<Author>(context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<Book> Book;
        public IRepository<Reservation> Reservation;
        public IRepository<NotificationRequest> NotificationRequest;
        public IRepository<CheckOut> CheckOut;
        public IRepository<Notification> Notification;
        public IRepository<Author> Author;
    }
}
