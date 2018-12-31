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
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [MaxLength(50)]
        public string CustomerEmail { get; set; }

        public int ProductQuantity { get; set; }

        [MaxLength(50)]
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