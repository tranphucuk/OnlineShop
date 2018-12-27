using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class FooterViewModel
    {
        public string ID { get; set; }
        public string Content { get; set; }

        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }
        public IEnumerable<ProductViewModel> LatestProducts { get; set; }
        public IEnumerable<PageViewModel> Pages { get; set; }
    }
}