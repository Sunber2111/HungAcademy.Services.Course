using System;
using System.Net;

namespace Application.Errors
{
    public class NotFoundException: Exception
    {
        public NotFoundException(object errors = null)
        {
            Errors = errors;
        }
        public object Errors { get; }
    }
}
