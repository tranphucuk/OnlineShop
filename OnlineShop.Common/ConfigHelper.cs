using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common
{
    public class ConfigHelper
    {
        public static string GetValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}
