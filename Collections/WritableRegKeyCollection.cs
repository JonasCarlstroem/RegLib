using RegLib.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public class WritableRegKeyCollection : RegKeyCollection<WritableRegKey>
    {
        public WritableRegKeyCollection() 
            : base() { }

        public WritableRegKeyCollection(IEnumerable<WritableRegKey> keys)
            : base(keys) { }
    }
}
