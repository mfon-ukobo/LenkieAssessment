using Application.Mediator;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Books.GetBook
{
    public class GetBookQuery : IQuery<Book>
    {
        public GetBookQuery(long bookId)
        {
            BookId = bookId;
        }

        public long BookId { get; set; }
    }

    internal sealed class GetBookQueryHandler : IQueryHandler<GetBookQuery, Book>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetBookQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetFirstAsync(x => x.Id == request.BookId);

            return book;
        }
    }
}
