using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public IEnumerable<ApplicationRoleViewModel> Roles { get; set; }
    }
}