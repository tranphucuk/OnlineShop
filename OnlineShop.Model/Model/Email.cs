using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Model
{
    [Table("Emails")]
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Your email seems too long.")]
        [Required(ErrorMessage = "Email address is empty.")]
        [EmailAddress(ErrorMessage = "Invalid email address, please check."), Column(TypeName = "varchar")]
        public string EmailAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}
