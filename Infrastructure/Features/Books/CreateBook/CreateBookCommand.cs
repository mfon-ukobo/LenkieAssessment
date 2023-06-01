using Application.Mediator;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Features.Books.CreateBook
{
    public class CreateBookCommand : ICommand<Result<Book>>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; }
    }

    internal sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Result<Book>>
    {
        private readonly UnitOfWork _unitOfWork;

        public CreateBookCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Status = BookStatus.Available
            };

            _unitOfWork.Book.Add(book);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return book;
            }
            catch (DbUpdateException ex)
            {
                // handle exception for duplicate
                // log exception
                throw;
            }
        }
    }
}
