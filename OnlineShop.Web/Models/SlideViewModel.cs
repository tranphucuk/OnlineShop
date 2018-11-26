using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class SlideViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int? DisplayOrder { get; set; }

        public bool Status { get; set; }

        public string Content { get; set; }
    }
}