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
    public class Book : BaseEntitiy<long>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public BookStatus Status { get; set; }
    }
}
