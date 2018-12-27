using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class EmailManagerViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "invalid username")]
        [MaxLength(50)]
        public string EmailUser { get; set; }

        [Required(ErrorMessage = "invalid password")]
        [MaxLength(50)]
        public string EmailPassword { get; set; }

        [Required(ErrorMessage = "Please enter email title")]
        [MaxLength(500)]
        public string MailTitle { get; set; }

        [Required(ErrorMessage = "Please enter email content")]
        public string MailContent { get; set; }

        public DateTime SendDate { get; set; }

        public int RecipientCount { get; set; }

        public IEnumerable<EmailViewModel> RecipientEmails { get; set; }
    }
}