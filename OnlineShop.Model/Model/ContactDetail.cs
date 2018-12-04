using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Model
{
    [Table("ContactDetails")]
    public class ContactDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(250, ErrorMessage = "Name seem be too long ")]
        [Required]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Phone seem be too long ")]
        [Required] public string Phone { get; set; }

        [StringLength(250, ErrorMessage = "Email seem be too long ")]
        [Required] public string Email { get; set; }

        [StringLength(250, ErrorMessage = "Address seem be too long ")]
        [Required] public string Address { get; set; }

        public string Other { get; set; }

        public bool Status { get; set; }
    }
}
