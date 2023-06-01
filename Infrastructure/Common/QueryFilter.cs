using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public abstract class QueryFilter
    {
        public QueryFilter()
        {
            Page = 1;
            Size = 20;
        }

        public int Page { get; set; }
        public int Size { get; set; }
    }
}
