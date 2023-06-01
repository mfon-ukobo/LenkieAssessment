using Application.Mediator;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Books.GetBooks
{
    public class GetBooksQuery : QueryFilter, IQuery<PagedList<Book>>
    {
        public string? Search { get; set; }
        public BookStatus? Status { get; set; }
    }

    internal sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, PagedList<Book>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetBooksQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = _unitOfWork.Book
                .Where(BookHasStatus(request.Status)) 
                .Where(BookTitleContains(request.Search));

            return await books.ToPagedListAsync(request);
        }

        private Expression<Func<Book, bool>> BookHasStatus(BookStatus? status)
        {
            if (status is null) return book => true;

            return book => book.Status == status;
        }

        private Expression<Func<Book, bool>> BookTitleContains(string? title)
        {
            if (string.IsNullOrWhiteSpace(title)) return book => true;

            return book => book.Title.Contains(title);
        }
    }
}
