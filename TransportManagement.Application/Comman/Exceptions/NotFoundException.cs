using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Comman.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Name;
        public object Key;
        public NotFoundException(string name, object key)
            : base($"{name} with Id '{key}' was not found.")
        {
            Name = name;
            Key = key;
        }
    }
}
