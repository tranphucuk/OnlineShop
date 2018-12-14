using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class ApplicationUserViewModel
    {
        [MaxLength(128)]
        public string Id { get; set; }

        [MaxLength(256), Required]
        public string Fullname { get; set; }

        [MaxLength(50), Required]
        public string UserName { get; set; }

        public string Bio { set; get; }

        [MaxLength(256)]
        public string Password { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public IEnumerable<ApplicationGroupViewModel> Groups { get; set; }
    }
}