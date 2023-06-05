using Application.Mediator;
using Domain.Entities;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Authors.GetAuthors
{
    public class GetAuthorsQuery : QueryFilter, IQuery<PagedList<Author>>
    {
    }

    internal sealed class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, PagedList<Author>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetAuthorsQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Author.GetAll().ToPagedListAsync(request);
        }
    }
}
