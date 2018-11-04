using System.Collections.Generic;

namespace OnlineShop.Web.Models
{
    public class TagViewModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<PostTagViewModel> PostTags { get; set; }

        public virtual IEnumerable<PostTagViewModel> ProductTags { get; set; }
    }
}