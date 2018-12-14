using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Model
{
    [Table("ApplicationUserRoles")]
    public class ApplicationUserRole
    {
        [MaxLength(128)]
        public string UserId { get; set; }

        [MaxLength(128)]
        public string RoleId { get; set; }

    }
}
