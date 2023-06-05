using Application.Mediator;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Books.UpdateBook
{
    public class UpdateBookCommand : ICommand<Result>
    {
        public UpdateBookCommand(long bookId, UpdateBookRequest payload)
        {
            BookId = bookId;
            Payload = payload;
        }

        public long BookId { get; set; }
        public UpdateBookRequest Payload { get; set;}
    }

    internal sealed class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, Result>
    {
        private readonly UnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetFirstAsync(x => x.Id == request.BookId);

            if (book is null)
            {
                return new Error($"Book with ID: {request.BookId} could not be found");
            }

            book.Title = request.Payload.Title;
            book.Description = request.Payload.Description;
            book.ImageUrl = request.Payload.ImageUrl;
            book.AuthorId = request.Payload.AuthorId;

            _unitOfWork.Book.Update(book);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.SUCCESS;
            }
            catch (Exception ex)
            {
                // log error
                throw;
            }
        }
    }
}
