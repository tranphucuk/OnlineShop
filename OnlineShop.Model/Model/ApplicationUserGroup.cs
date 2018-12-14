using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Model
{
    [Table("ApplicationUserGroups")]
    public class ApplicationUserGroup
    {
        [StringLength(128)]
        [Key]
        [Column(Order =1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GroupId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}
