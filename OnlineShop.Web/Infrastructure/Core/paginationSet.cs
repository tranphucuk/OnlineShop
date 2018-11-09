using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Web.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }

        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}