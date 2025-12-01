using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entitys.Exceptions
{
    public class NewFileException : Exception
    {

        public NewFileException() { }

        public NewFileException(string message) : base(message) { }
    }
}
