using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerAddress { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public string CustomerMobile { get; set; }

        public string CustomerMessage { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public bool Status { get; set; }

        public virtual IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}