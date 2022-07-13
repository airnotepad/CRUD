using System;

namespace Application.Common.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message) { }
    }
}