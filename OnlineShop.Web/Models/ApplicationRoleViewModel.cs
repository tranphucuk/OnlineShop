using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class ApplicationRoleViewModel
    {
        [MaxLength(128)]
        public string Id { get; set; }

        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}