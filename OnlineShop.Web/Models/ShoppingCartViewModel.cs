using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int CartID { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}