using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Other { get; set; }

        public bool Status { get; set; }
    }
}