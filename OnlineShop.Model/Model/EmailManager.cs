using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Model
{
    [Table("EmailManagers")]
    public class EmailManager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName ="varchar")]
        public string EmailUser { get; set; }

        [Required(ErrorMessage = "Please enter email title")]
        [MaxLength(500)]
        public string MailTitle { get; set; }

        [Required(ErrorMessage ="Please enter email content")]
        public string MailContent { get; set; }

        public DateTime SendDate { get; set; }

        public int RecipientCount { get; set; }

        public IEnumerable<Email> RecipientEmails { get; set; }
    }
}
