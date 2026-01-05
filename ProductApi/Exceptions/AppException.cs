using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace ProductApi.Exceptions
{
    public  abstract class AppException : Exception
    {
        public int StatusCode { get; }
        protected AppException(string message,int statuscode) : base(message)
        {
            StatusCode = statuscode;
        }
    }
}