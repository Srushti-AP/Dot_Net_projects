using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace ProductApi.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message,  (int)HttpStatusCode.NotFound)
        {
        }
    }
}