using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common.Exceptions
{
    public class CurrentUserDeleteException
    {
        public static string message = "This action can't be accepted because this user is in use";
    }
}
