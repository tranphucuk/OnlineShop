using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class LogoViewModel
    {
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}