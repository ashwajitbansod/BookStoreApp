using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreApp.Models
{
    public class ServiceResponseModel<T>
    {
        public string Message { get; set; }
        public long Response { get; set; }
        public T Data { get; set; }


    }
}