using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Books")]
    public class Book : BaseEntity<long>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public BookStatus Status { get; set; }

        public void Reserve()
        {
            Status = BookStatus.Reserved;
        }

        public void Checkout()
        {
            Status = BookStatus.CheckedOut;
        }

        public void Makeavailable()
        {
            Status = BookStatus.Available;
        }
    }
}
