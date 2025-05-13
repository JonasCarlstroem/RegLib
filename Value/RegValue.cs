using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Value
{
    public class RegValue
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public RegValue() { }

        public RegValue(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
