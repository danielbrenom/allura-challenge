using System;

namespace Allura.Challenge.Domain.Exceptions
{
    public class GenericException : Exception
    {
        public int StatusCode { get; set; }

        public GenericException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}