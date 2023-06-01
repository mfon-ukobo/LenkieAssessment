using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Books.UpdateBook
{
    public class UpdateBookRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public BookStatus Status { get; set; }
    }
}
