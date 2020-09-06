using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreApp.Models
{
    public class Enum
    {
    }
    public enum ServiceResponse
    {
        SuccessResponse = 1,
        FailedResponse = 0,
        ExeptionResponse = -1,
        InvalidSessionResponse = -2,
        NoRecord = 2
    }
}