using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common
{
    public class CommonConstants
    {
        public const string productTag = "product";
        public const string postTag = "post";
        public const string footerId = "default";
        public static string GetCurrency(decimal price)
        {
            return price.ToString("C0", new CultureInfo("en-US"));
        }
        public const string sessionCart = "sessionCart";
    }
}
