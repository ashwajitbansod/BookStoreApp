using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreApp.Services
{
    public static class CommonMessage
    {
        public static string WrongParameterMessage()
        {
            return "Parameter is wrong.";
        }
        public static string Success()
        {
            return "Success.";
        }
    }
}