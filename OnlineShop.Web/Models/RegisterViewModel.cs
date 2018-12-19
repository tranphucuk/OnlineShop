using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class RegisterViewModel
    {
        [MaxLength(50, ErrorMessage = "Your Name seem too long")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Fullname { get; set; }

        [MaxLength(50, ErrorMessage = "Your Username seem too long")]
        [MinLength(6, ErrorMessage = "Your Username seem too short")]
        [Required(ErrorMessage = "Please enter your Username")]
        public string Username { get; set; }

        [MaxLength(50, ErrorMessage = "Your Password seem too long")]
        [MinLength(6, ErrorMessage = "Your password must longer than 6 characters")]
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

        [MaxLength(50, ErrorMessage = "Your Password seem too long")]
        [MinLength(6, ErrorMessage = "Your password must longer than 6 characters")]
        [Required(ErrorMessage = "Please enter your Password")]
        [Compare("Password", ErrorMessage = "Confirm passsword doesn't match. Try again")]
        public string ConfirmPassword { get; set; }

        [MaxLength(250, ErrorMessage = "Your address seem too long")]
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [MaxLength(50, ErrorMessage = "Your email seem too long")]
        [EmailAddress(ErrorMessage = "Your email is invalid")]
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "Your phone number is invalid")]
        [MinLength(9, ErrorMessage = "Your phone number is invalid")]
        [Required(ErrorMessage = "Please enter your phone")]
        public string Phone { get; set; }
    }
}