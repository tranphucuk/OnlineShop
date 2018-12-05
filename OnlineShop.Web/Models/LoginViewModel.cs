using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*Please enter your username account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}