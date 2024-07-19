using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Sharing
{
    public class ProductParams
    {
        public int MaxPageSize { get; set; } = 5;
        private int pageSize;
        public int PageSize { get => pageSize; set => pageSize = value > MaxPageSize ? MaxPageSize : value; }
        public int PageNumber { get; set; } = 1;

        public int? CategoryId { get; set; }
        public string Sorting { get; set; }

        private string _search;
        public string Search { get => _search; set => _search = value.ToLower(); }
    }
}
