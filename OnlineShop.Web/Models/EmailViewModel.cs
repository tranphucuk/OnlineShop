using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class EmailViewModel
    {
        public int ID { get; set; }

        [MaxLength(50,ErrorMessage ="Your email seems too long.")]
        [Required(ErrorMessage ="Email address is empty.")]
        [EmailAddress(ErrorMessage ="Invalid email address, please check."), Column(TypeName = "varchar")]
        public string EmailAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}