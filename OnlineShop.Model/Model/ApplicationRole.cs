using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Model
{
    [Table("ApplicationRoles")]
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {

        }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
