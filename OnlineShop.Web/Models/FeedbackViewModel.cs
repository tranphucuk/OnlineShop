using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [MaxLength(50,ErrorMessage ="Your name seem be too long")]
        [Required(ErrorMessage ="Please enter your name")]
        public string Name { get; set; }

        [MaxLength(50,ErrorMessage ="Your email seem be inccorect")]
        [Required(ErrorMessage ="Please enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter your message")]
        public string Content { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public ContactDetailViewModel ContactDetailVm { get; set; }

        public bool Status { get; set; }
    }
}