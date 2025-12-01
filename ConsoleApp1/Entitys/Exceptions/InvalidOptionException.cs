
using System;

namespace Models.Entitys.Exceptions
{
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException() { }

        public InvalidOptionException(string message) : base(message) { }
    }
}
