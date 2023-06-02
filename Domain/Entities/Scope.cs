using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Scopes")]
    public class Scope : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
